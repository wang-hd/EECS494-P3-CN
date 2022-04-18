using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelSelectLevelForcast : MonoBehaviour
{
    Text forecast_text;
    static List<string> dummy_words = new List<string>
    {
        "Super super tare fish is rarer than super rare fish.",
        "You will get rewards for hunting down dangerous fishes.",
        "Who knows what will happen after you explore the whole map.",
    };

    void Start()
    {
        forecast_text = GetComponent<Text>();
        // remember to follow TaskControllerForMap.cs
        switch (StaticData.day)
        {
            case 1:
                forecast_text.text = "Future Gadget Laboratory will be unlocked in two days.";
                break;
            case 2:
                forecast_text.text = "Future Gadget Laboratory will be unlocked in one day.";
                break;
            case 3:
                forecast_text.text = "Black Dragon Harbor will be unlocked in two days.";
                break;
            case 4:
                forecast_text.text = "Black Dragon Harbor will be unlocked in one day.";
                break;
            case 5:
                forecast_text.text = "State Lake will be unlocked in two days.";
                break;
            case 6:
                forecast_text.text = "State Lake will be unlocked in one day.";
                break;
            case 7:
                forecast_text.text = "Upper Sea will be unlocked in two days.";
                break;
            case 8:
                forecast_text.text = "Upper Sea will be unlocked in one day.";
                break;
            default:
                forecast_text.text = dummy_words[Random.Range(0,dummy_words.Count-1)];
                break;
        }
    }
}
