using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Craftable : MonoBehaviour
{
    public GameObject panel;
    public GameObject rod_panel;
    public GameObject outline;
    public AudioClip rodChangeAudio;
    public AudioClip rodChangeErrorAudio;
    public Text in_use_rod;
    public Text rod_description;
    public Text rod_unlock_requirement;
    public Text rod_unlock_value;
    public static int selected_rod_index;
    [SerializeField] AudioClip craft_audio;
    Subscription<switch_rod_event> switch_rod_subscription;

    static bool is_first_upgrade = true;

    private void Start() {
        switch_rod_subscription = EventBus.Subscribe<switch_rod_event>(ShowDescription);
        switch_rod_subscription = EventBus.Subscribe<switch_rod_event>(ShowRequirement);

        selected_rod_index = StaticData.current_rod_index;
        rod_panel.transform.GetChild(selected_rod_index).GetComponent<Toggle>().isOn = true;
        rod_description.text = StaticData.rod_descriptions[selected_rod_index];
        in_use_rod.text = StaticData.rods[selected_rod_index];
        SetRequirement();
    }
    
    // outline on hover
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
            AudioSource.PlayClipAtPoint(craft_audio, Camera.main.transform.position);
            OpenPanel();
            rod_panel.transform.GetChild(StaticData.current_rod_index).GetComponent<Toggle>().isOn = true;
            rod_description.text = StaticData.rod_descriptions[StaticData.current_rod_index];
        }
        
    }

    // open & close UI panel
    public void OpenPanel()
    {
        StaticData.has_open_panel = true;
        panel.SetActive(true);
        if (StaticData.tutorial_step == 3) EventBus.Publish<TutorialProcessEvent>(new TutorialProcessEvent());
    }

    public void ClosePanel()
    {
        StaticData.has_open_panel = false;
        panel.SetActive(false);
    }

    // Craftable feature
    void ShowDescription(switch_rod_event e)
    {
        rod_description.text = StaticData.rod_descriptions[selected_rod_index];
    }

    void ShowRequirement(switch_rod_event e) 
    {
        SetRequirement();
        rod_unlock_value.color = new Color32(204, 207, 151, 255);
    }

    void SetRequirement()
    {
        if (StaticData.rod_unlock_status[selected_rod_index]) 
        {
            rod_unlock_requirement.text = "Already unlocked";
            rod_unlock_value.text = "";
        }
        else 
        {
            rod_unlock_requirement.text = "Unlock Require: ";
            rod_unlock_value.text = "x " + StaticData.rod_unlock_values[selected_rod_index] + " (" +
                StaticData.bones + ")";
        }
    }

    bool CheckRodSwitch()
    {
        if (StaticData.rod_unlock_status[selected_rod_index]) return true;
        else
        {
            if (StaticData.bones >= StaticData.rod_unlock_values[selected_rod_index])
            {
                StaticData.bones -= StaticData.rod_unlock_values[selected_rod_index];
                StaticData.rod_unlock_status[selected_rod_index] = true;
                rod_unlock_requirement.text = "Already unlocked";
                rod_unlock_value.text = "";
                return true;
            }
            return false;
        }
    }

    public void ConfirmRodSwitch()
    { 
        if (CheckRodSwitch())  
        {
            AudioSource.PlayClipAtPoint(rodChangeAudio, Camera.main.transform.position);
            if(is_first_upgrade&&StaticData.current_rod_index!=selected_rod_index)
            {
                EventBus.Publish<update_task_event>(new update_task_event(2,1));
                is_first_upgrade = false;
            }
            StaticData.current_rod_index = selected_rod_index;
            in_use_rod.text = "In use: " + StaticData.rods[selected_rod_index];
            
            EventBus.Publish<change_rod_event>(new change_rod_event());
        }
        else 
        {
            AudioSource.PlayClipAtPoint(rodChangeErrorAudio, Camera.main.transform.position);
            rod_unlock_value.color = new Color32(184, 85, 60, 255);
        }
    }

    public void CancelRodSwitch()
    {
        selected_rod_index = StaticData.current_rod_index;
        rod_unlock_value.color = new Color32(204, 207, 151, 255);
    }

}
