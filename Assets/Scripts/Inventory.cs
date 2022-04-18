using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    Subscription<fish_hooked_event> fishHooked_event_subscription;
    Subscription<get_fish_event> getFish_event_subscription;
    Subscription<use_fish_event> useFish_event_subscription;

    [SerializeField] GameObject[] prefabs;
    [SerializeField] GameObject UI_Inventory;
    [SerializeField] GameObject fish_number;
    [SerializeField] GameObject fish_weight;
    [SerializeField] AudioClip fish_use_audio;


    private bool inventoryDisplay = false;
    private Text fish_number_text;
    private Text fish_weight_text;

    //private int fishID;


    // Start is called before the first frame update
    void Start()
    {
        fish_number_text = fish_number.GetComponent<Text>();
        fish_weight_text = fish_weight.GetComponent<Text>();
        getFish_event_subscription = EventBus.Subscribe<get_fish_event>(_AddFish);
        useFish_event_subscription = EventBus.Subscribe<use_fish_event>(_UseFish);
        //fishHooked_event_subscription = EventBus.Subscribe<fish_hooked_event>(_SetFish);

        // restore data from static data
        foreach (int fishID in StaticData.inventory)
        {
            GameObject go = Instantiate(prefabs[fishID], prefabs[fishID].transform.position, Quaternion.identity);
            go.transform.SetParent(UI_Inventory.transform.GetChild(0).transform, false);
        }
        //fish_number_text.text = StaticData.fish_list_num.ToString();
        fish_number_text.text = StaticData.bones.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.Escape)) && inventoryDisplay)
        {
            UI_Inventory.SetActive(false);
            inventoryDisplay = false;
        }
        else if (Input.GetKeyDown(KeyCode.B) && !inventoryDisplay)
        {
            UI_Inventory.SetActive(true);
            inventoryDisplay = true;
        }
    }

    //public void _SetFish(fish_hooked_event e)
    //{
    //    fishID = e.fishHookedID;
    //}

    public void _AddFish(get_fish_event e)
    {
        StaticData.fish_list_num = StaticData.fish_list_num + 1;
        StaticData.fish_weight_sum = StaticData.fish_weight_sum + FishList.GetFishWithFishID(e.fishHookedID).getWeight();
        GameObject go = Instantiate(prefabs[e.fishHookedID], prefabs[e.fishHookedID].transform.position, Quaternion.identity);
        go.transform.SetParent(UI_Inventory.transform.GetChild(0).transform, false);
        //fish_number_text.text = StaticData.fish_list_num.ToString();
        fish_weight_text.text = StaticData.fish_weight_sum.ToString("0.0") + " lb";
    }

    public void _UseFish(use_fish_event e)
    {
        AudioSource.PlayClipAtPoint(fish_use_audio, Camera.main.transform.position);
        StaticData.fish_list_num -= 1;
        StaticData.fish_weight_sum -= e.weight;
        //fish_number_text.text = StaticData.fish_list_num.ToString();
        fish_weight_text.text = StaticData.fish_weight_sum.ToString("0.0") + " lb";
    }
    
        private void OnDestroy()
    {
        EventBus.Unsubscribe<get_fish_event>(getFish_event_subscription);
        EventBus.Unsubscribe<use_fish_event>(useFish_event_subscription);
    }
}
