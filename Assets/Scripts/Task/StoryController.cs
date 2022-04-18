using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryController : MonoBehaviour
{
    [SerializeField] GameObject CloseButton;
    [SerializeField] GameObject[] StoryIcons;
    [SerializeField] GameObject OpenButton;

    [SerializeField] GameObject StoryText;

    [SerializeField] GameObject StoryPanel;
    [SerializeField] Sprite[] SpriteForStory;

    Text StoryTextContent;
    List<Image> ImageForStory = new List<Image>();

    string[] StoryLists = {
        "新型核反应二期试验已经接近尾声. 研发部的同事告诉我家用核反应堆已经完成beta测并且即将投入市场。虽说如此，探索科学的过程是无止境的，而且我也不会就此止步。",
        "一个同事告诉我她住的街区遭到了爆炸的波及，她今天起不能来上班了。又一个实验因为人手不足被推迟了。该死。我不能理解为什么人们宁愿把时间花在毫无意义的争斗上，也不愿意来搞点科学。",
        "10/26 钓鱼\n10/27 钓鱼\n10/30 钓鱼\n11/6 无意义地钓鱼\n11/21 还是钓鱼，但我感觉越来越痛苦和迷茫\n12/25 依然是钓鱼，要是能搞点科学实验就更好了",
        "3/12 钓鱼，不过今天我发现了一处实验室的遗迹\n3/17 我找了些实验室里还能用的设备，这下又可以开始做实验了。不知道这样的日子还能持续多久，但也许这样的状态对我来说已经是一种奢侈。",
        "我的实验...是世界毁灭的元凶？对这件事我无法再视而不见......如果时间重新来过...如果我从未发布过那些研究成果...如果..."
    };



    private bool status = false;


    void Awake()
    {
        CloseButton.GetComponent<Button>().onClick.AddListener(_CloseButtonOnclick);
        OpenButton.GetComponent<Button>().onClick.AddListener(_OpenButtonOnclick);
        for (int i = 0; i < StoryIcons.Length; i++)
        {
            int present = i;
            StoryIcons[i].GetComponent<Button>().onClick.AddListener(() => _StoryOnclick(present));
            ImageForStory.Add(StoryIcons[i].GetComponent<Image>());
        }

        StoryTextContent = StoryText.GetComponent<Text>();
    }

    void LoadStory()
    {
        // Set the status of the icon, 
        // Change the sprite of each image
        for (int i = 0; i < ImageForStory.Count; i++)
        {
            if (i <= StaticData.story_progress)
            {
                ImageForStory[i].sprite = SpriteForStory[1];
            }
            else
            {
                ImageForStory[i].sprite = SpriteForStory[0];
            }
        }
    }

    void RefreshPanel()
    {
        if (status == true)
        {
            LoadStory();
        }
    }

    void CloseOriginalPanel()
    {
        if (status == true)
        {
            StoryPanel.SetActive(false);
        }
    }

    void _StoryOnclick(int i)
    {
        Debug.Log("Story" + i);
        //Show the corresponding story in TaskList
        if (i <= StaticData.story_progress)
        {
            StoryTextContent.text = $"Story: {i + 1}\n";
            StoryTextContent.text += StoryLists[i];
        }
    }

    void _CloseButtonOnclick()
    {
        //change status to 0;
        //Set the status of PanelController to false
        CloseOriginalPanel();
        status = false;
        StaticData.has_open_panel = false;
    }

    void _OpenButtonOnclick()
    {
        //change status to 1
        //activate the todo panel;
        //refresh todo panel;
        //set the status of PanelController to true;
        StaticData.has_open_panel = true;
        status = true;
        StoryPanel.SetActive(true);
        RefreshPanel();
        _StoryOnclick(0);
    }
}