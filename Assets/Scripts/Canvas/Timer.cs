using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    int hour;
    int minute;
    float present_time = 0.0f;
    bool is_the_day_finish = false;
    Text text_content;
    Subscription<get_fish_event> got_fish_subscription;
    Subscription<refresh_the_day> refresh_the_day_subscription;

    // Start is called before the first frame update
    void Awake()
    {
        text_content = GetComponent<Text>();
        got_fish_subscription = EventBus.Subscribe<get_fish_event>(_reset);
        refresh_the_day_subscription = EventBus.Subscribe<refresh_the_day>(_refresh);
    }

    // Update is called once per frame
    void Update()
    {
        if(is_the_day_finish || StaticData.has_open_panel){
            return;
        }
        present_time +=Time.deltaTime * Clock.speed_up_scale;
        hour = ((int)present_time) / 60;
        minute = (int)present_time - 60 * hour;

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
    }

    public void _reset(get_fish_event e)
    {
        present_time = 0.0f;
    }

    public void _refresh(refresh_the_day e)
    {
        is_the_day_finish = true;
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe<get_fish_event>(got_fish_subscription);
        EventBus.Unsubscribe<refresh_the_day>(refresh_the_day_subscription);
    }
}
