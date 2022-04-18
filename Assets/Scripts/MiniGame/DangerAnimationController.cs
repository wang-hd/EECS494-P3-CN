using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerAnimationController : MonoBehaviour
{
    FishingMiniGameControler controler;
    MiniGameFish mini_game_fish;
    Animator shining_animator;
    private int fishID;
    public float animator_speed_coifficient = 0.05f;
    public float danger_warning_threshold = 20f;
    // Start is called before the first frame update
    void Start()
    {
        controler = gameObject.GetComponentInParent<FishingMiniGameControler>();
        mini_game_fish = gameObject.GetComponentInParent<MiniGameFish>();
        shining_animator = gameObject.GetComponent<Animator>();
        fishID = mini_game_fish.fishID;
        if (fishID == -1) // in tutorial
        {
            gameObject.SetActive(false);
        }
        else
        {
            float danger_value = controler.mini_game_fish_attack[mini_game_fish.index];
            if (danger_value > danger_warning_threshold)
            {
                shining_animator.speed = danger_value * animator_speed_coifficient;
            }
            else
            {
                shining_animator.speed = 0;
            }
            
        }
    }
}
