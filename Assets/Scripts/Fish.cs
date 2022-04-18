using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] bool isFish = true;
    [SerializeField] int fishID = -1;
    [SerializeField] int attack = 0;
    [SerializeField] int weight = 1;
    [SerializeField] int restore_hungry_value = 0;
    [SerializeField] int restore_health_value = 0;
    [SerializeField] int sheid_value = 0;

    [SerializeField] float mini_game_stay_time = 2f;
    [SerializeField] float mini_game_move_speed = 1f;
    [SerializeField] float mini_game_progress_bar_increase_speed = 0.2f;
    [SerializeField] float mini_game_progress_bar_drop_speed = 0.1f;

    [SerializeField] Color major_color = new Color(106/255, 21/255, 129/255);

    [SerializeField] FishRarity fish_rarity = FishRarity.R;
    [SerializeField] FishSize fish_size = FishSize.Small;
    [SerializeField] string fish_story = "A fish that has many siblings with different colors.";

    public bool getIsFish()
    {
        return isFish;
    }

    public int getFishID()
    {
        return fishID;
    }

    public int getAttack()
    {
        return attack;
    }

    public int getWeight()
    {
        return weight;
    }

    public int getHungerValue()
    {
        return restore_hungry_value;
    }
    
    public int getHealthValue()
    {
        return restore_health_value;
    }

    public int getSheidValue()
    {
        return sheid_value;
    }

    public float getMiniGameStayTime()
    {
        return mini_game_stay_time;
    }

    public float getMiniGameMoveSpeed()
    {
        return mini_game_move_speed;
    }

    public float getMiniGameProgressBarIncreaseSpeed()
    {
        return mini_game_progress_bar_increase_speed;
    }

    public float getMiniGameProgressBarDropSpeed()
    {
        return mini_game_progress_bar_drop_speed;
    }

    public Color getMajorColor()
    {
        return major_color;
    }

    public FishRarity getFishRarity()
    {
        return fish_rarity;
    }

    public FishSize getFishSize()
    {
        return fish_size;
    }

    public string getFishName()
    {
        return gameObject.name;
    }

    public string getFishStory()
    {
        return fish_story;
    }
}
