using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameProgressBar : MonoBehaviour
{
    private int fishID;
    Slider progressbar_game_object;
    SpriteRenderer fish_icon_in_progress;
    FishingMiniGameControler controler;
    [SerializeField] MiniGameFish mini_game_fish;
    public bool isActive = true;
    // Start is called before the first frame update
    void Start()
    {
        controler = gameObject.GetComponentInParent<FishingMiniGameControler>();
        fishID = mini_game_fish.fishID;
        progressbar_game_object = gameObject.GetComponent<Slider>();
        fish_icon_in_progress = gameObject.GetComponentInChildren<SpriteRenderer>();
        progressbar_game_object.value = controler.mini_game_progress_bar_progress[mini_game_fish.index];
        fish_icon_in_progress.color = Color.white;
        if (fishID != -1) // not tutorial
        {
            progressbar_game_object.transform.Find("ProgressBarFill").GetComponent<Image>().color = FishList.GetFishWithFishID(fishID).major_color;
            fish_icon_in_progress.sprite = FishList.GetFishWithFishID(fishID).getSprite();
        }

        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        progressbar_game_object.value = controler.mini_game_progress_bar_progress[mini_game_fish.index];
        if (mini_game_fish.in_region_indicator && controler.mini_game_progress_bar_progress[mini_game_fish.index] < 1 && isActive)
        {
            float progress_value = controler.mini_game_progress_bar_progress[mini_game_fish.index];
            progress_value += controler.mini_game_progress_bar_increase_speeds[mini_game_fish.index] * Time.deltaTime;
            controler.SetProgressValue(mini_game_fish.index, progress_value);
        }
        else if (!mini_game_fish.in_region_indicator && controler.mini_game_progress_bar_progress[mini_game_fish.index] > 0 && isActive)
        {
            float progress_value = controler.mini_game_progress_bar_progress[mini_game_fish.index];
            progress_value -= controler.mini_game_progress_bar_drop_speeds[mini_game_fish.index] * Time.deltaTime;
            controler.SetProgressValue(mini_game_fish.index, progress_value);
        }

        if (controler.mini_game_progress_bar_progress[mini_game_fish.index] <= 0)
        {
            mini_game_fish.DeactivateMiniGameFish();
        }

        if (controler.mini_game_progress_bar_progress[mini_game_fish.index] >= 1 && isActive)
        {
            if (fishID != -1)
            {// not tutorial
                mini_game_fish.DeactivateMiniGameFish();
                isActive = false;
            }
            controler.GotFishFromProgressBar(fishID);
        }
    }
}
