using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelController : MonoBehaviour
{
    [SerializeField] GameObject CaughtFish;
    [SerializeField] GameObject DaySummary;
    [SerializeField] GameObject DayAttack;
    [SerializeField] GameObject DayHunger;
    [SerializeField] GameObject DayFish;
    [SerializeField] GameObject Instruction;
    [SerializeField] GameObject InventoryPanel;
    [SerializeField] GameObject TaskPanel;
    [SerializeField] GameObject StoryPanel;
    [SerializeField] GameObject CaughtFishPanel;
    [SerializeField] GameObject FishInfoPanel;
    [SerializeField] GameObject ComeHomeConfirmPanel;
    [SerializeField] GameObject[] indexPrefab;

    Subscription<get_fish_event> get_fish_subcscription;
    Subscription<refresh_the_day> refresh_day_subcscription;
    Subscription<show_instruction> instruction_subscription;
    Subscription<get_item_event> getItem_event_subscription;
    Subscription<fish_info_event> fishInfo_event_subscription;
    CaughtFish fish;
    Text instruction_text;

    public int hunger_lose = 30;
    int health_lose = 0;
    List<FishData> fish_got = new List<FishData> { };


    // Multi fish support
    Queue<get_fish_event> get_fish_event_queue = new Queue<get_fish_event>();
    Queue<get_item_event> get_item_event_queue = new Queue<get_item_event>();

    //private int fishID;
    // Start is called before the first frame update
    void Awake()
    {
        get_fish_subcscription = EventBus.Subscribe<get_fish_event>(_onGetFish);

        refresh_day_subcscription = EventBus.Subscribe<refresh_the_day>(_onRefreshDays);
        instruction_subscription = EventBus.Subscribe<show_instruction>(_onShowInstruction);
        fishInfo_event_subscription = EventBus.Subscribe<fish_info_event>(_onFishInfoRequested);
        getItem_event_subscription = EventBus.Subscribe<get_item_event>(_onGetItem);
        fish = CaughtFish.GetComponent<CaughtFish>();
        instruction_text = Instruction.GetComponentsInChildren<Text>()[0];
    }

    void Start()
    {
        health_lose = 0;
        fish_got = new List<FishData> { };
        // close task panel
        StaticData.has_open_panel = false;
    }

    private void Update()
    {
        if(!StaticData.has_open_panel && (get_fish_event_queue.Count > 0 || get_item_event_queue.Count > 0))
        {
            if(get_fish_event_queue.Count > 0)
            {
                get_fish_event e = get_fish_event_queue.Dequeue();
                _openDisplayFishPanel(e);
            }
            if(get_item_event_queue.Count > 0)
            {
                get_item_event e = get_item_event_queue.Dequeue();
                _openDisplayItemPanel(e);
            }
        }
    }

    void _onGetFish(get_fish_event e)
    {
        get_fish_event_queue.Enqueue(e);
    }


    void _openDisplayFishPanel(get_fish_event e)
    {
        StaticData.has_open_panel = true;
        CaughtFish.SetActive(true);
        FishData this_fish = FishList.GetFishWithFishID(e.fishHookedID);

        fish_got.Add(this_fish);
        health_lose += this_fish.getAttack();

        fish.DisplayFish(this_fish.getFishName(), this_fish.getAttack(), this_fish.getWeight(), this_fish.getHungerValue(),
        this_fish.getHealthValue(), this_fish.getSprite(), this_fish.getIsFish());
    }

    void _onGetItem(get_item_event e)
    {
        get_item_event_queue.Enqueue(e);
    }

    void _openDisplayItemPanel(get_item_event e)
    {
        StaticData.has_open_panel = true;
        CaughtFish.SetActive(true);
        FishData this_item = FishList.GetFishWithFishID(e.itemID);
        fish.DisplayFish(this_item.getFishName(), this_item.getAttack(), this_item.getWeight(), this_item.getHungerValue(),
        this_item.getHealthValue(), this_item.getSprite(), this_item.getIsFish());
    }

    void _onRefreshDays(refresh_the_day e)
    {
        StaticData.has_open_panel = true;
        DayAttack.GetComponent<Text>().text = $"失去的生命: {health_lose}";
        DayHunger.GetComponent<Text>().text = $"失去的能量: {hunger_lose}";
        foreach(FishData fish in fish_got)
        {
            GameObject tmp = Instantiate(indexPrefab[fish.getID()]);
            tmp.GetComponent<Image>().enabled = false;
            tmp.transform.GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255);
            tmp.transform.SetParent(DayFish.transform);
            tmp.transform.Rotate(new Vector3(0, 0, -45));
            tmp.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            
        }
            


        InventoryPanel.SetActive(false);
        TaskPanel.SetActive(false);
        StoryPanel.SetActive(false);
        CaughtFishPanel.SetActive(false);
        FishInfoPanel.SetActive(false);
        ComeHomeConfirmPanel.SetActive(false);
        DaySummary.SetActive(true);
    }

    void _onShowInstruction(show_instruction e)
    {
        if(e.is_begin){
            Instruction.SetActive(true);
            instruction_text.text = e.instruction_name;
        }else{
            Instruction.SetActive(false);
            instruction_text.text = "";
        }
    }

    void _onFishInfoRequested(fish_info_event e)
    {
        FishInfoPanel.SetActive(true);

        FishInfoPanel.GetComponent<fishInfoDisplayer>().handleFishInfoEvent(e);
    }
    public void SetOnClick(GameObject panel)
    {
        if (!StaticData.has_open_panel)
        {
            panel.SetActive(true);
            StaticData.has_open_panel = true;
        }
    }

    public void SetOffClick(GameObject panel)
    {
        if (StaticData.has_open_panel)
        {
            panel.SetActive(false);
            StaticData.has_open_panel = false;
        }
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe<get_fish_event>(get_fish_subcscription);
        EventBus.Unsubscribe<refresh_the_day>(refresh_day_subcscription);
        EventBus.Unsubscribe<show_instruction>(instruction_subscription);
        EventBus.Unsubscribe<get_item_event>(getItem_event_subscription);
    }

}
