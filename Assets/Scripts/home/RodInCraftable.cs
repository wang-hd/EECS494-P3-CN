using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RodInCraftable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Toggle toggle;
    Image image;
    Image border_image;
    Image select_image;
    public int rod_index;

    Subscription<change_rod_event> change_rod_subs;

    // Start is called before the first frame update
    void Start()
    {
        toggle = GetComponent<Toggle>();
        image = GetComponent<Image>();

        border_image = gameObject.transform.Find("Border").GetComponent<Image>();
        select_image = gameObject.transform.Find("Select").GetComponent<Image>();
        change_rod_subs = EventBus.Subscribe<change_rod_event>(ChangeGreen);

        toggle.onValueChanged.AddListener( SwitchRod ) ;
        toggle.onValueChanged.AddListener( OnToggleValueChanged ) ;

        image.color = Color.gray;
        if (rod_index == StaticData.current_rod_index) 
        {
            image.color = Color.white;
            select_image.color = new Color32(255, 255, 255, 255);
        }
        
    }

    // following code changes display of yellow border
    void SwitchRod(bool isOn)
    {
        if (isOn)
        {
            Craftable.selected_rod_index = rod_index;
            EventBus.Publish<switch_rod_event>(new switch_rod_event());
        }
    }

    private void OnToggleValueChanged(bool isOn)
    {
        Color c = image.color;
        if (isOn)
        {
            border_image.color = new Color32(255, 255, 255, 255);
            image.color = Color.white;
        }
        else
        {
            border_image.color = new Color32(255, 255, 255, 0);
            image.color = Color.gray;
        }
    }

    public void OnPointerEnter(PointerEventData pointer) 
    {
        if (!toggle.isOn) border_image.color = new Color32(255, 255, 255, 100);
    }
    
    public void OnPointerExit(PointerEventData pointer) {
        if (!toggle.isOn) border_image.color = new Color32(255, 255, 255, 0);
    }

    // following code changes display of green border
    void ChangeGreen(change_rod_event e) 
    {
        if (rod_index == StaticData.current_rod_index) select_image.color = new Color32(255, 255, 255, 255);
        else select_image.color = new Color32(255, 255, 255, 0);
    }
}
