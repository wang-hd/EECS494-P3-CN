using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayUpdator : MonoBehaviour
{
    Text text_content;
    Subscription<refresh_the_day> refresh_the_day_subscription;
    int day;

    // Start is called before the first frame update
    void Awake()
    {
        text_content = GetComponent<Text>();
        refresh_the_day_subscription = EventBus.Subscribe<refresh_the_day>(_refresh);

    }

    void Start()
    {
        day = StaticData.day;
        text_content.text = "Day " + day.ToString();
    }
    // Update is called once per frame
    public void _refresh(refresh_the_day e)
    {
        day ++;
        text_content.text = "Day "+ day;
        EventBus.Publish<alter_hunger_event>(new alter_hunger_event(-30));
        StaticData.day = day;
        StaticData.ini_time = 480;
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe<refresh_the_day>(refresh_the_day_subscription);
    }
}
