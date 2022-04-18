using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionControll : MonoBehaviour
{
    [SerializeField] GameObject NextDayPanel;
    [SerializeField] GameObject TransitionPanel;
    [SerializeField] AudioClip die_audio;
    [SerializeField] AudioClip door_audio;

    private AudioSource Audio;
    // Start is called before the first frame update
    void Start()
    {
        Audio = GetComponent<AudioSource>();
        if (StaticData.usage == TransitionScreenUsage.to_dead)
        {
            StaticData.Refresh();
            StaticData.message = "You Are Dead. You Keep Something, But You Can Not Keep Everything.\nWhat is lost?";
            Audio.clip = die_audio;
            Audio.Play();
            NextDayPanel.SetActive(false);
        }else if(StaticData.usage == TransitionScreenUsage.to_customize)
        {
            Audio.clip = door_audio;
            Audio.Play();
            NextDayPanel.SetActive(false);
        }
            else if(StaticData.usage == TransitionScreenUsage.to_home)
        {
            TransitionPanel.SetActive(false);
        }else if(StaticData.usage == TransitionScreenUsage.to_finish)
        {
            StaticData.Refresh();
            NextDayPanel.SetActive(false);
        }
    }
}
