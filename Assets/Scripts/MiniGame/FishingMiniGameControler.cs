using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingMiniGameControler : MonoBehaviour
{
    MiniGameInterval[] intervals;
    MiniGameFish[] mini_game_fishes;
    public int[] fishIDs { get; private set; }

    private List<int> got_fish_ID;

    public SpriteRenderer leading_fish_image;

    public float[] mini_game_progress_bar_progress { get; private set; } = new float[] { 0.2f, 0.2f, 0.2f, 0.2f, 0.2f };
    public float[] mini_game_progress_bar_increase_speeds { get; private set; } = new float[] { 0.2f, 0.2f, 0.2f, 0.2f, 0.2f };
    public float[] mini_game_progress_bar_drop_speeds { get; private set; } = new float[] { 0.1f, 0.1f, 0.1f, 0.1f, 0.1f };

    public float[] mini_game_fish_attack { get; private set; } = new float[] { 0f, 0f, 0f, 0f, 0f };

    bool isMuliple = false;

    void Awake()
    {

        intervals = gameObject.GetComponentsInChildren<MiniGameInterval>(true);
        fishIDs = new int[5] {0,1,2,3,4};
    }

    private void Start()
    {
        // Choose the using rods

        foreach (MiniGameInterval mini_game_interval in intervals)
        {
            if (fishIDs[0] != -1)// not in tutorial mode
            {
                if (mini_game_interval.name == StaticData.rods[StaticData.current_rod_index])
                {
                    mini_game_interval.gameObject.SetActive(true);
                    mini_game_interval.enabled = true;
                }
                else
                {
                    mini_game_interval.gameObject.SetActive(false);
                    mini_game_interval.enabled = false;
                }
            }
        }

        for (int i = 0; i < 5; i++)
        {
            if(fishIDs[i] != -1)// not in tutorial mode
            {
                mini_game_progress_bar_increase_speeds[i] = FishList.GetFishWithFishID(fishIDs[i]).mini_game_progress_bar_increase_speed;
                mini_game_progress_bar_drop_speeds[i] = FishList.GetFishWithFishID(fishIDs[i]).mini_game_progress_bar_drop_speed;
                mini_game_fish_attack[i] = FishList.GetFishWithFishID(fishIDs[i]).getAttack();
            }
            else 
            {
                if (i == 0) mini_game_progress_bar_progress[i] = 0.5f;
                if (i != 0) mini_game_progress_bar_progress[i] = 0;
            }
            
        }
        got_fish_ID = new List<int> { };

        if(StaticData.current_rod_index == 6) 
        {// currently only rod 6 supports getting multiple fish at a time.
            isMuliple = true;
        }
    }
    void Update()
    {
        float max_progress = 0;
        int max_progress_idx = 0;
        for (int i = 0; i < mini_game_progress_bar_progress.Length; i++)
        {
            if(mini_game_progress_bar_progress[i] >= max_progress)
            {
                max_progress = mini_game_progress_bar_progress[i];
                max_progress_idx = i;
            }
        }

        // Display the leading fish

        if(fishIDs[0]!= -1)// not in tutorial mode
        {
            leading_fish_image.sprite = FishList.GetFishWithFishID(fishIDs[max_progress_idx]).getSprite();
        }
       
        
        if (max_progress == 0)
        {
            if (fishIDs[0] != -1)
            {// not in tutorial mode
                EventBus.Publish<fish_escape_event>(new fish_escape_event("Fish escaped"));
            }
            else
            {
                EventBus.Publish<tutorial_fish_escape_event>(new tutorial_fish_escape_event("Fish escaped"));
            }
            DestroyMiniGame();
        }


        if(got_fish_ID.Count > 0)
        {
            if (isMuliple)
            {// use the rod that can get multiple fishes;
                bool isEndGame = true;
                for (int i = 0; i < mini_game_progress_bar_progress.Length; i++)
                {
                    if (mini_game_progress_bar_progress[i] > 0 && mini_game_progress_bar_progress[i] < 1)
                    {
                        isEndGame = false;
                    }
                }
                if (isEndGame) {
                    foreach(int id in got_fish_ID)
                    {
                        handleGotFishOrItemEvent(id);
                    }
                    DestroyMiniGame(); 
                }
            }
            else
            {
                int fishID = got_fish_ID[0];
                handleGotFishOrItemEvent(fishID);
                DestroyMiniGame();
            }
        }
       
    }

    public void DestroyMiniGame()
    {
        Destroy(gameObject);
    }

    public void setFishIDs(int[] fish_ids)
    {
        fishIDs = fish_ids;
    }

    public void SetFishProgressBarIncreaseSpeed(Func<int[], float[]> callback)
    {
        mini_game_progress_bar_increase_speeds = callback(fishIDs);
    }

    public void SetFishProgressBarDropSpeed(Func<int[], float[]> callback)
    {
        mini_game_progress_bar_drop_speeds = callback(fishIDs);
    }

    public void SetProgressValue(int idx, float val)
    {
        mini_game_progress_bar_progress[idx] = val;
    }

    public void GotFishFromProgressBar(int fishID)
    {
        got_fish_ID.Add(fishID);
    }


    private void handleGotFishOrItemEvent(int fishID)
    {
        if (fishID != -1)
        {// n
            if (FishList.GetFishWithFishID(fishID).isFish)
            {
                EventBus.Publish<unlock_item_event>(new unlock_item_event(fishID)); // although the name is item, it is the index of fish
                EventBus.Publish<get_fish_event>(new get_fish_event("Fish caught", fishID));
            }
            else
            {
                EventBus.Publish<get_item_event>(new get_item_event(fishID));
            }
        }
        else
        {
            EventBus.Publish<tutorial_get_fish_event>(new tutorial_get_fish_event("Fish cauget"));
        }
        
    }
}
