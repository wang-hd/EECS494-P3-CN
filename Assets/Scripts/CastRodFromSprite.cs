using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastRodFromSprite : MonoBehaviour
{
    [SerializeField] GameObject player;
    PlayerController player_controller;
    [SerializeField] GameObject outline;

    private void Start() {
        player_controller = player.GetComponent<PlayerController>();
    }

    private void OnMouseDown()
    {
        player_controller.handleCastClick();
        //gameObject.SetActive(false);
        if (StaticData.tutorial_step == 1) EventBus.Publish<TutorialCastEvent>(new TutorialCastEvent());
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

}
