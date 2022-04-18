using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelSelectLevelForcast : MonoBehaviour
{
    Text forecast_text;
    static List<string> dummy_words = new List<string>
    {
        "超超稀有的鱼比超稀有的鱼更稀有。",
        "危险的鱼给你的收获也越多。",
        "把所有地区都探索一遍之后会发生什么？谁知道呢。",
    };

    void Start()
    {
        forecast_text = GetComponent<Text>();
        // remember to follow TaskControllerForMap.cs
        switch (StaticData.day)
        {
            case 1:
                forecast_text.text = "未来道具研究所 将在2天后解锁。";
                break;
            case 2:
                forecast_text.text = "未来道具研究所 将在1天后解锁。";
                break;
            case 3:
                forecast_text.text = "黑龙港 将在2天后解锁。";
                break;
            case 4:
                forecast_text.text = "黑龙港 将在1天后解锁。";
                break;
            case 5:
                forecast_text.text = "州湖 将在2天后解锁。";
                break;
            case 6:
                forecast_text.text = "州湖 将在1天后解锁。";
                break;
            case 7:
                forecast_text.text = "前海 将在2天后解锁。";
                break;
            case 8:
                forecast_text.text = "前海 将在1天后解锁。";
                break;
            default:
                forecast_text.text = dummy_words[Random.Range(0,dummy_words.Count-1)];
                break;
        }
    }
}
