using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskControllerForKitchen : MonoBehaviour
{
    [SerializeField] GameObject BigPanel;
    public GameObject closeButton;
    // Start is called before the first frame update
    public void onButtonClickOpen()
    {
        if(StaticData.has_open_panel)
        {
            Debug.Log("panel is opened");
            return;
        }else{
            StaticData.has_open_panel = true;
            BigPanel.SetActive(true);
            closeButton.SetActive(true);
        }
    }

    public void onButtonClickClose()
    {
        StaticData.has_open_panel = false;
    }
}
