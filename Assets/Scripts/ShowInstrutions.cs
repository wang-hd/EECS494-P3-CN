using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ShowInstrutions : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string Instruction;
    [SerializeField] bool index_panel = false;
    public void OnPointerEnter(PointerEventData eventData)
    {
        EventBus.Publish<show_instruction>(new show_instruction(Instruction, true));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EventBus.Publish<show_instruction>(new show_instruction("", false));
    }

    void OnMouseOver()
    {
        if (!StaticData.has_open_panel || index_panel)
        {
            EventBus.Publish<show_instruction>(new show_instruction(Instruction, true));
        }
    }

    public void OnMouseExit()
    {
        EventBus.Publish<show_instruction>(new show_instruction("", false));
    }
}
