using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour
{
    public LevelSelectPlayer player;
    public LevelSelectPoint start_point;
    public bool moveable = true;
    private int current_level_idx = 0;
    List<GameObject> fish_pool;
    [SerializeField] GameObject levels;
    [SerializeField] GameObject confirm_panel;
    [SerializeField] Text level_text;
    [SerializeField] Text level_story;
    [SerializeField] GameObject [] indexPrefab;
    [SerializeField] GameObject panel_background;
    [SerializeField] GameObject Special_object;

    Subscription<reach_level_event> reach_level_event_subscription;
    Subscription<leave_level_event> leave_level_event_subscription;
    // Start is called before the first frame update
    void Start()
    {
        // Should come from some static file to remember which level is the player in.
        player.Initialize(start_point);
        reach_level_event_subscription = EventBus.Subscribe<reach_level_event>(HandleReachLevelEvent);
        leave_level_event_subscription = EventBus.Subscribe<leave_level_event>(HandleLeaveLevelEvent);
        fish_pool = new List<GameObject> { };

    }

    // Update is called once per frame
    void Update()
    {
        if (player.is_moving || !moveable) return;

        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            player.TrySetDirection(Direction.up);
        }else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            player.TrySetDirection(Direction.down);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            player.TrySetDirection(Direction.left);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            player.TrySetDirection(Direction.right);
        }

        // Testcode
        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                UnlockLevel(StaticData.getLevelNameByLevelIdx(1));
                EventBus.Publish<ToastRequest>(new ToastRequest("未来道具研究所 已解锁", 3.0f, false, false, false));
                StaticData.day = 3;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                UnlockLevel(StaticData.getLevelNameByLevelIdx(2));
                EventBus.Publish<ToastRequest>(new ToastRequest("黑龙港 已解锁", 3.0f, false, false, false));
                StaticData.day = 5;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                UnlockLevel(StaticData.getLevelNameByLevelIdx(3));
                EventBus.Publish<ToastRequest>(new ToastRequest("州湖 已解锁", 3.0f, false, false, false));
                StaticData.day = 7;
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                UnlockLevel(StaticData.getLevelNameByLevelIdx(4));
                EventBus.Publish<ToastRequest>(new ToastRequest("前海 已解锁", 3.0f, false, false, false));
                StaticData.day = 9;
            }
        }

        
    }

    public void UnlockLevel(string level_name)
    {
        LevelSelectPoint level = GetChildWithName(levels, level_name).GetComponent<LevelSelectPoint>();
        level.is_locked = false;
        foreach(LevelSelectPoint autopoints in level.AttachedAutoPoints)
        {
            autopoints.is_locked = false;
        }
        if (level.StopSignForThisLevel) {
            level.StopSignForThisLevel.SetActive(false);
        }
    }

    public GameObject GetChildWithName(GameObject obj, string name)
    {
        Debug.Log(name);
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }

    void HandleReachLevelEvent(reach_level_event e)
    {
        confirm_panel.SetActive(true);
        current_level_idx = e.level_idx;
        if(e.level_idx == -2)
        {// it is home
            panel_background.SetActive(false);
            level_text.text = "Home";
            level_story.text = StaticData.getLevelStoryByLevelIdx(e.level_idx);
        }
        else if(e.level_idx >=0)
        {   
            level_text.text = StaticData.getLevelNameByLevelIdx(e.level_idx);
            level_story.text = StaticData.getLevelStoryByLevelIdx(e.level_idx);
            panel_background.SetActive(true);
            List<int> current_level_fish = StaticData.fishInEachLevel[e.level_idx];
            current_level_fish.Sort((f1,f2)=>
            {
                if (!FishList.GetFishWithFishID(f1).isFish)
                {
                    return 1;
                }
                if (!FishList.GetFishWithFishID(f2).isFish)
                {
                    return -1;
                }
                int res = FishList.GetFishWithFishID(f1).fish_rarity - FishList.GetFishWithFishID(f2).fish_rarity;
                if (res == 0)
                {
                    return f1 - f2;
                }
                return res;
            });
            for (int i = 0; i < current_level_fish.Count; i++){
                GameObject tmp;
                if (FishList.GetFishWithFishID(current_level_fish[i]).isFish)
                {
                    tmp = Instantiate(indexPrefab[current_level_fish[i]]);
                    if (StaticData.checkIndex(current_level_fish[i]))
                    {
                        tmp.transform.GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255);
                    }
                }
                else
                {
                    tmp = Instantiate(Special_object);     
                }
                tmp.transform.SetParent(panel_background.transform);
                fish_pool.Add(tmp);
            }

        }   
    }

    void HandleLeaveLevelEvent(leave_level_event e)
    {
        for(int i = 0; i < fish_pool.Count; i++){
            if(fish_pool[i]!=null){
                Destroy(fish_pool[i]);
            }
        }
        confirm_panel.SetActive(false);
    }

    public void LoadLevel()
    {
        if (level_text.IsActive())
        {
            if (current_level_idx == -2)
            {
                SceneManager.LoadScene("home");
            }
            else if(current_level_idx != -1)
            {
                StaticData.currentLevelIdx = current_level_idx;
                SceneManager.LoadScene("Main Scene");
            }

        }
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe<reach_level_event>(reach_level_event_subscription);
        EventBus.Unsubscribe<leave_level_event>(leave_level_event_subscription);
    }
}
