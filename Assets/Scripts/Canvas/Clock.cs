using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    int hour;
    int minute;

    // the start time of fishing, represents in minutes
    // default: 8:00 am
    int ini_time = 480;
    float present_time = 0.0f;
    bool is_day_finish = false;

    // the scale factor of game time, comparing to real time
    public static int speed_up_scale = 10;
    Text text_content;

    // ensure player will not be forced to go home in the middle of a minigame
    Subscription<fish_hooked_event> fishHook_event_subscription;
    Subscription<close_caught_fish_panel_event> caught_fish_event_subscription;
    Subscription<fish_escape_event> fishEscape_event_subscription;
    private bool isMinigameFinished = true;

    // Start is called before the first frame update
    void Awake()
    {
        text_content = GetComponent<Text>();

        // start time may vary
        ini_time = StaticData.ini_time;

        fishHook_event_subscription = EventBus.Subscribe<fish_hooked_event>(handleFishHooked);
        caught_fish_event_subscription = EventBus.Subscribe<close_caught_fish_panel_event>(closeCaughtFishPanelHandler);
        fishEscape_event_subscription = EventBus.Subscribe<fish_escape_event>(handleFishEscape);
    }

    void handleFishHooked(fish_hooked_event e)
    {
        isMinigameFinished = false;
    }

    void closeCaughtFishPanelHandler(close_caught_fish_panel_event e)
    {
        isMinigameFinished = true;
    }
    void handleFishEscape(fish_escape_event e)
    {
        isMinigameFinished = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (is_day_finish && isMinigameFinished)
        {
            EventBus.Publish<refresh_the_day>(new refresh_the_day(1));
            isMinigameFinished = false;
        }
        if(is_day_finish || StaticData.has_open_panel){
            return;
        }
        present_time +=Time.deltaTime * speed_up_scale;
        hour = ((int)present_time + ini_time) / 60;
        minute = (int)present_time + ini_time - 60 * hour;

        // add zeros
        string hour_string = hour.ToString();
        string minute_string = minute.ToString();
        if (hour < 10) hour_string = "0" + hour_string;
        if (minute < 10) minute_string = "0" + minute_string;

        // if time changing frequently is too annoying, add the below line
        if (minute%5 == 0) 
        {
            text_content.text = $"{hour_string}:{minute_string}";
        }
        
        if (hour == 18 && minute == 0)
        {
            EventBus.Publish<time_to_nignt>(new time_to_nignt());
        }
        
        if(hour >= 24)
        {
            //EventBus.Publish<refresh_the_day>(new refresh_the_day(1));
            is_day_finish = true;
        }
    }

    public int GetTime()
    {
        return (int)present_time + ini_time;
    }

}
