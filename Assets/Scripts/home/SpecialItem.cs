using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialItem : MonoBehaviour
{
    public int fishID;
    public GameObject controller;


    public GameObject outline;
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
            controller.GetComponent<SpecialItemController>().OpenPanel(fishID);
        }
        
    }


}
