using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectToggleStory : MonoBehaviour
{
    [SerializeField] Text story_text;
    [SerializeField] GameObject fish_and_item_display;
    public void toggle_info()
    {
        if (!story_text.enabled)
        {
            story_text.enabled = true;
            fish_and_item_display.SetActive(false);
        }
        else
        {
            story_text.enabled = false;
            fish_and_item_display.SetActive(true);
        }
    }
}
