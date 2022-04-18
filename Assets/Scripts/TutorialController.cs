using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    [SerializeField] GameObject arrow;
    static TutorialController instance;

    Subscription<TutorialCastEvent> cast_rod_sub;
    Subscription<TutorialProcessEvent> change_scene_sub;

    [Header("Buttons")]
    private GameObject bag_obj;
    private bool bag_obj_enabled = false;
    private GameObject task_obj;
    private bool task_obj_enabled = false;
    private GameObject story_obj;
    private bool story_obj_enabled = false;
    private GameObject fishbone_obj;
    private bool fishbone_obj_enabled = false;
    private GameObject satiety_obj;
    private bool satiety_obj_enabled = false;
    private GameObject health_obj;
    private bool health_obj_enabled = false;
    [SerializeField] GameObject tutorial_panel;
    int tutorial_panel_step = 0;

    Subscription<get_fish_event> get_fish_sub;
    Subscription<update_task_event> update_task_sub;
    Subscription<update_task_event> update_task_sub_fishbone;

    Dictionary<int, Vector3> positions = new Dictionary<int, Vector3> {
        {1, new Vector3(0, -0.6f, 0)}, {2, new Vector3(-4, 2.5f, 0)}, {3, new Vector3(0, 3.2f, 0)},
    };
    Dictionary<string, Vector3> tutorial_panel_position = new Dictionary<string, Vector3> {
        {"satiety", new Vector3(300, -200, 0)}, {"health", new Vector3(300, -200, 0)}, 
        {"time", new Vector3(300, 300, 0)}, {"task", new Vector3(-1200, 100)},
    };
    Dictionary<string, string> tutorial_texts = new Dictionary<string, string> {
        {"satiety", "能量会随时间流逝。\n吃鱼可以恢复能量。"},
        {"health", "如果生命值降到0，游戏结束。\n有些鱼可以恢复生命值。"},
        {"time", "每天24:00结束。\n按右下角的Home键可以随时回家。"},
        {"task", "完成任务会解锁新的故事。\n任务的完成度也会影响游戏的结局。"},
    };
    //Dictionary<int, Quaternion> rotations = new Dictionary<int, Quaternion> {
    //    {1, Quaternion.identity},
    //};
    // Start is called before the first frame update
    void Awake()
    {
        // Typical singleton initialization code.
        if (instance != null && instance != this)
        {
            // If there already exists a ToastManager, we need to go away.
            Destroy(gameObject);
            return;
        }
        else
        {
            // If we are the first ToastManager, we claim the "instance" variable so others go away.
            instance = this;
            DontDestroyOnLoad(gameObject); // Survive scene changes
        }

        cast_rod_sub = EventBus.Subscribe<TutorialCastEvent>(HideOnCast);
        change_scene_sub = EventBus.Subscribe<TutorialProcessEvent>(ProcessTutorial);

        SceneManager.sceneLoaded += OnSceneLoaded;

        if (SceneManager.GetActiveScene().name == "Main Scene" && StaticData.day == 1)
        {
            StartCoroutine(OldTutorial());
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name=="Level Selection")
        {
            arrow.SetActive(false);
        }
        else if (scene.name == "Main Scene")
        {
            StartCoroutine(ShowNextTutorial());
        }

        bag_obj = GameObject.FindWithTag("InventoryButton");
        if (bag_obj_enabled)
        {
            EnableBag(new get_fish_event("dummy", 0));
        }
        task_obj = GameObject.FindWithTag("TaskButton");
        story_obj = GameObject.FindWithTag("StoryButton");
        if (task_obj_enabled || story_obj_enabled)
        {
            EnableTask(new update_task_event(0, 0));
        }
        fishbone_obj = GameObject.FindWithTag("FishBone");
        if (fishbone_obj_enabled)
        {
            EnableFishBones(new update_task_event(1, 2));
        }
        satiety_obj = GameObject.FindWithTag("satiety");
        if (satiety_obj_enabled)
        {
            EnableSatiety();
        }
        health_obj = GameObject.FindWithTag("health");
        if (health_obj_enabled)
        {
            EnableHealth();
        }
        
    }

    private void Start()
    {
        // button
        get_fish_sub = EventBus.Subscribe<get_fish_event>(EnableBag);
        update_task_sub = EventBus.Subscribe<update_task_event>(EnableTask);
        update_task_sub_fishbone = EventBus.Subscribe<update_task_event>(EnableFishBones);

    }

    IEnumerator OldTutorial()
    {
        yield return new WaitForSeconds(4f);
        SetArrowPosition();
    }

    void StartSatietyTutorial()
    {
        EnableSatiety();
        tutorial_panel.SetActive(true);
        RectTransform rt = tutorial_panel.GetComponent<RectTransform>();
        rt.anchorMax = new Vector2(0, 0.5f); rt.anchorMin = new Vector2(0, 0.5f);
        rt.anchoredPosition = tutorial_panel_position["satiety"];
        tutorial_panel.GetComponentInChildren<Text>().text = tutorial_texts["satiety"];
        tutorial_panel_step = 2;
    }

    void StartHealthTutorial()
    {
        EnableHealth();
        tutorial_panel.SetActive(true);
        RectTransform rt = tutorial_panel.GetComponent<RectTransform>();
        rt.anchorMax = new Vector2(0, 0.5f); rt.anchorMin = new Vector2(0, 0.5f);
        rt.anchoredPosition = tutorial_panel_position["health"];
        tutorial_panel.GetComponentInChildren<Text>().text = tutorial_texts["health"];
        tutorial_panel_step = 3;
    }

    void StartTimeTutorial()
    {
        tutorial_panel.SetActive(true);
        RectTransform rt = tutorial_panel.GetComponent<RectTransform>();
        rt.anchorMax = new Vector2(0, 0.5f); rt.anchorMin = new Vector2(0, 0.5f);
        rt.anchoredPosition = tutorial_panel_position["time"];
        tutorial_panel.GetComponentInChildren<Text>().text = tutorial_texts["time"];
        tutorial_panel_step = 4;
    }

    public void CloseTutorialPanel()
    {
        tutorial_panel.SetActive(false);
        StartCoroutine(ShowNextTutorial());
    }

    IEnumerator ShowNextTutorial()
    {
        if (tutorial_panel_step == 4) yield break;
        yield return new WaitForSeconds(0.5f);
        while (StaticData.has_open_panel || GameObject.FindGameObjectWithTag("Minigame"))
        {
            yield return new WaitForEndOfFrame();
        }
        if (!tutorial_panel.activeInHierarchy)
        {
            if (tutorial_panel_step == 1) 
            {
                EnableSatiety();
                if (SceneManager.GetActiveScene().buildIndex == 1) StartSatietyTutorial();
            }
            else if (tutorial_panel_step == 2) 
            {
                EnableHealth();
                if (SceneManager.GetActiveScene().buildIndex == 1) StartHealthTutorial();
            }
            else if (tutorial_panel_step == 3) 
            {
                
                if (SceneManager.GetActiveScene().buildIndex == 1) StartTimeTutorial();
            }
        }
    }



    void HideOnCast(TutorialCastEvent e) 
    {
        if (StaticData.tutorial_step == 1)
        {
            arrow.SetActive(false);
            StaticData.tutorial_step = 2;
        }
    }

    void ProcessTutorial(TutorialProcessEvent e) 
    {
        if (StaticData.tutorial_step == 0 || StaticData.tutorial_step >= 3)
        {
            StaticData.tutorial_step = 0;
            arrow.SetActive(false);
            return;
        }
        
        StaticData.tutorial_step ++;
        SetArrowPosition();
    }

    void SetArrowPosition()
    {
        // scene index: main = 1, home = 3
        if (SceneManager.GetActiveScene().buildIndex == 1 && StaticData.tutorial_step == 1)
        {
            arrow.SetActive(true);
            arrow.transform.position = positions[StaticData.tutorial_step];
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3 && StaticData.tutorial_step >= 2 && StaticData.tutorial_step <= 3)
        {
            arrow.SetActive(true);
            arrow.transform.position = positions[StaticData.tutorial_step];
        }
        else arrow.SetActive(false);

    }



    void EnableBag(get_fish_event e)
    {
        bag_obj_enabled = true;
        if (bag_obj)
        {

            bag_obj.GetComponent<Button>().enabled = true;
            bag_obj.GetComponent<Image>().enabled = true;
        }
        
    }

    void EnableTask(update_task_event e)
    {
        // if appear for the first time
        if (task_obj_enabled == false) 
        {
            StartCoroutine(DelayedEnableTask());
            return;
        }
        task_obj_enabled = true;
        story_obj_enabled = true;
        if (task_obj)
        {
            task_obj.GetComponent<Image>().enabled = true;
            task_obj.GetComponent<Button>().enabled = true;
        }
        if (story_obj)
        {
            story_obj.GetComponent<Image>().enabled = true;
            story_obj.GetComponent<Button>().enabled = true;
        }
    }

    IEnumerator DelayedEnableTask()
    {
        yield return new WaitForSeconds(4f);
        task_obj_enabled = true;
        EnableTask(new update_task_event(1, 1));

        if (tutorial_panel)
        {
            tutorial_panel.SetActive(true);
            RectTransform rt = tutorial_panel.GetComponent<RectTransform>();
            rt.anchorMax = new Vector2(1, 0.5f); rt.anchorMin = new Vector2(1, 0.5f);
            rt.anchoredPosition = tutorial_panel_position["task"];
            tutorial_panel.GetComponentInChildren<Text>().text = tutorial_texts["task"];
            tutorial_panel_step = 1;
        }

    }

    void EnableFishBones(update_task_event e)
    {
        if(e.task==1 & e.step == 2)
        {
            fishbone_obj_enabled = true;
            if (fishbone_obj){
                fishbone_obj.GetComponent<Text>().enabled = true;
                fishbone_obj.GetComponentInChildren<Image>().enabled = true;
            }
        }
    }

    void EnableSatiety()
    {
        if (satiety_obj)
        {
            satiety_obj_enabled = true;
            Image[] images = satiety_obj.GetComponentsInChildren<Image>();
            foreach (Image image in images) image.enabled = true;
        }

    }

    void EnableHealth()
    {
        if (health_obj)
        {
            health_obj_enabled = true;
            Image[] images = health_obj.GetComponentsInChildren<Image>();
            foreach (Image image in images) image.enabled = true;
        }
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe<get_fish_event>(get_fish_sub);
    }
}
