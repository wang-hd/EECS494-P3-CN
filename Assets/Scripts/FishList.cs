using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FishSize
{
    Small,
    Medium,
    Large
}

// Numbers in FishRarity represent weights when calculating possiblities
public enum FishRarity
{
    R=0,
    SR=1,
    SSR=2
}
public class FishData
{
    public bool isFish { get; private set; } = true;
    private int fishID = -1;
    private int attack = 0;
    private float weight = 1.0f;
    private int restore_hungry_value = 0;
    private int restore_health_value = 0;
    private int sheid_value = 0;
    private Sprite sprite;

    //public int restore_hungry_value_after_cooked { get; private set; } = 0;
    //public int restore_health_value_after_cooked { get; private set; } = 0;

    public float mini_game_stay_time { get; private set; } = 2f;
    public float mini_game_move_speed { get; private set; } = 1f;
    public float mini_game_progress_bar_increase_speed { get; private set; } = 0.2f;
    public float mini_game_progress_bar_drop_speed { get; private set; } = 0.1f;

    public Color major_color { get; private set; } = new Color(106 / 255, 21 / 255, 129 / 255);

    public FishRarity fish_rarity { get; private set; } = FishRarity.R;

    public FishSize fish_size { get; private set; } = FishSize.Small;

    public string fish_name { get; private set; } = "Untitled Fish";

    public string fish_story { get; private set; } = "A fish that has many siblings with different colors.";


    public FishData(bool _isFish, int _fishID, int _attack, float _weight, int _restore_hungry_value, int _restore_health_value, int _sheid_value, Sprite _sprite, float _mini_game_stay_time, float _mini_game_move_speed, float _mini_game_progress_bar_increase_speed, float _mini_game_progress_bar_drop_speed, Color _major_color, FishRarity _fish_rarity, FishSize _fish_size, string _fish_name, string _fish_story)
    {
        isFish = _isFish;
        fishID = _fishID;
        attack = _attack;
        weight = _weight;
        restore_hungry_value = _restore_hungry_value;
        restore_health_value = _restore_health_value;
        sheid_value = _sheid_value;
        sprite = _sprite;
        mini_game_stay_time = _mini_game_stay_time;
        mini_game_move_speed = _mini_game_move_speed;
        mini_game_progress_bar_increase_speed = _mini_game_progress_bar_increase_speed;
        mini_game_progress_bar_drop_speed = _mini_game_progress_bar_drop_speed;
        major_color = _major_color;
        fish_rarity = _fish_rarity;
        fish_size = _fish_size;
        fish_name = _fish_name;
        fish_story = _fish_story;
    }

    public int getID()
    {
        return fishID;
    }

    public int getAttack()
    {
        return attack;
    }

    public float getWeight()
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

    public FishRarity getRarity()
    {
        return fish_rarity;
    }
    public Sprite getSprite()
    {
        return sprite;
    }

    public string getFishName()
    {
        return fish_name;
    }

    public string getFishStory()
    {
        return fish_story;
    }

    public bool getIsFish()
    {
        return isFish;
    }

}

public class FishList : MonoBehaviour
{
    public static List<FishData> fishList = new List<FishData>();
    [SerializeField] GameObject[] fishes;
    private static bool start = true;

    Subscription<unlock_item_event> unlock_item_subscription;

    // Start is called before the first frame update
    void Start()
    {
        if (start)
        {
            fishList.Clear();
            int fishCount = 0;
            foreach (GameObject fish in fishes)
            {
                Fish fishComponent = fish.GetComponent<Fish>();
                if (fishComponent.getIsFish())
                {
                    fishCount += 1;
                }
                FishData fishdata = new FishData(fishComponent.getIsFish(),fishComponent.getFishID(), fishComponent.getAttack(), fishComponent.getWeight(),
                                                 fishComponent.getHungerValue(), fishComponent.getHealthValue(), fishComponent.getSheidValue(),
                                                 fishComponent.GetComponent<SpriteRenderer>().sprite, fishComponent.getMiniGameStayTime(),
                                                 fishComponent.getMiniGameMoveSpeed(), fishComponent.getMiniGameProgressBarIncreaseSpeed(),
                                                 fishComponent.getMiniGameProgressBarDropSpeed(), fishComponent.getMajorColor(), fishComponent.getFishRarity(),
                                                 fishComponent.getFishSize(), fishComponent.getFishName(), fishComponent.getFishStory());
                fishList.Insert(fish.GetComponent<Fish>().getFishID(), fishdata);
            }

            StaticData.setIndexSize(fishCount);
            start = false;
        }

        unlock_item_subscription = EventBus.Subscribe<unlock_item_event>(unlockItem);

    }

    public static FishData GetFishWithFishID(int fish_id)
    {
        // This way is better if we want the fish_id to be something other than index, we can just change this place;
        return fishList[fish_id];
    }

    void unlockItem(unlock_item_event e)
    {
        StaticData.unlockItem(e.itemID);
    }
}