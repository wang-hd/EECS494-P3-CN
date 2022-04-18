using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kitchen : MonoBehaviour
{
    public GameObject panel;
    public GameObject outline;
    [SerializeField] GameObject[] prefabs;
    [SerializeField] GameObject fish_panel_content;
    [SerializeField] AudioClip fishbone_audio;
    [SerializeField] AudioClip kitchen_audio;

    public static List<GameObject> pot = new List<GameObject>();
    public static int bones_in_pot = 0;
    Subscription<switch_fish_event> switch_fish_subscription;
    public Text reward_text;

    static bool is_first_exchange = true;

    private void Start() {
        switch_fish_subscription = EventBus.Subscribe<switch_fish_event>(ShowReward);
    }

    private void OnMouseEnter() {
        if (!StaticData.has_open_panel) 
        {
            outline.SetActive(true);
        }
    }

    private void OnMouseExit() {
        if (!StaticData.has_open_panel) 
        {
            outline.SetActive(false);
        }
    }


    private void OnMouseDown() {
        outline.SetActive(false);
        if (!StaticData.has_open_panel) 
        {
            AudioSource.PlayClipAtPoint(kitchen_audio, Camera.main.transform.position);
            OpenPanel();
        }
        
    }

    public void OpenPanel()
    {
        StaticData.has_open_panel = true;
        clearFishPanel();
        foreach (int fishID in StaticData.inventory)
        {
            GameObject go = Instantiate(prefabs[fishID], prefabs[fishID].transform.position, Quaternion.identity);
            go.transform.SetParent(fish_panel_content.transform, false);
        }
        panel.SetActive(true);
        if (StaticData.tutorial_step == 2) EventBus.Publish<TutorialProcessEvent>(new TutorialProcessEvent());
    }

    public void ClosePanel()
    {
        StaticData.clearInventory();
        foreach (Transform fish in fish_panel_content.transform)
        {
            if (fish != null)
            {
                StaticData.inventory.Add(fish.gameObject.GetComponent<FishInPot>().getFishID());
            }

        }
        panel.SetActive(false);
        StaticData.has_open_panel = false;
    }

    public void Cook() 
    {
        foreach (GameObject fish in pot)
        {
            AudioSource.PlayClipAtPoint(fishbone_audio, Camera.main.transform.position);
            Destroy(fish);
        }
        StaticData.bones += bones_in_pot;
        StaticData.bones_accrued += bones_in_pot;
        if(StaticData.bones_accrued >= 30)
        {
            EventBus.Publish<update_task_event>(new update_task_event(3,1));
        }
        if(is_first_exchange&&bones_in_pot!=0)
        {
            EventBus.Publish<update_task_event>(new update_task_event(1,2));
            is_first_exchange = false;
        }
        ClearPot();
        //ClosePanel();
    }

    public void CancelCook() 
    {
        ClearPot();
    }
    
    void ClearPot()
    {
        foreach (Transform child in fish_panel_content.transform)
        {
            child.gameObject.GetComponent<Toggle>().isOn = false;
        }
        Kitchen.pot = new List<GameObject>();
        bones_in_pot = 0;
    }

    void ShowReward(switch_fish_event e) 
    {
        reward_text.text = "x " + bones_in_pot + " (" + StaticData.bones + ")";
    }

    private void clearFishPanel()
    {
        foreach (Transform fish in fish_panel_content.transform)
        {
            if (fish != null)
            {
                Destroy(fish.gameObject);
            }

        }
    }
}
