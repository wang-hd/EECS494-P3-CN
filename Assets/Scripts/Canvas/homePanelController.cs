using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class homePanelController : MonoBehaviour
{
    [SerializeField] GameObject FishInfoPanel;
    [SerializeField] GameObject Inventory;
    [SerializeField] GameObject Instruction;
    Text instruction_text;


    Subscription<fish_info_event> fishInfo_event_subscription;
    Subscription<show_instruction> instruction_subscription;


    fishInfoDisplayer fishInfo;
    // Start is called before the first frame update
    void Awake()
    {
        fishInfo = FishInfoPanel.GetComponent<fishInfoDisplayer>();
        fishInfo_event_subscription = EventBus.Subscribe<fish_info_event>(_onFishInfoRequested);
        instruction_subscription = EventBus.Subscribe<show_instruction>(_onShowInstruction);
        instruction_text = Instruction.GetComponentsInChildren<Text>()[0];

    }

    private void Start()
    {
        StaticData.has_open_panel = false;
    }

    void _onFishInfoRequested(fish_info_event e)
    {
        FishInfoPanel.SetActive(true);

        FishData fish = FishList.GetFishWithFishID(e.fishInfoID);
        fishInfo.DisplayFishInfo(fish.getFishName(), fish.getFishStory(), fish.getSprite(),
                                fish.getAttack(), fish.getWeight(), fish.getHungerValue(), fish.getHealthValue(), fish.getIsFish());
    }

    void _onShowInstruction(show_instruction e)
    {
        if (e.is_begin)
        {
            Instruction.SetActive(true);
            instruction_text.text = e.instruction_name;
        }
        else
        {
            Instruction.SetActive(false);
            instruction_text.text = "";
        }
    }
}
