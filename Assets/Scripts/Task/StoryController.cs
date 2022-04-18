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
        "Research on the new type of nuclear reaction will soon come to an end. Colleagues from Engineering Department told me that home nuclear reactors is out of beta and will be released on time. Even though, exploring the truths of this universe is endless, and I will never stop.",
        "I just found out that one of colleagues can no longer come because her neighborhood was bombed. Another test is postponed due to lack of labor. Damn. I can’t understand why people would rather spend time on meaningless conflicts than doing science.",
        "10/26 Fishing\n10/27 Fishing\n10/30 Fishing\n11/6 Fishing aimlessly\n11/21 Fishing with increasing confusion and pain\n12/25 Fishing, but I still prefer science",
        "3/12 Fishing, but I found the ruins of some science labs.\n3/17 I reconstructed some parts of the labs. Science prerequisites met, Test can be restarted. I don’t know how much longer I’ll have to be like this, but maybe my current state is already the best I can ask for.",
        "I can’t see this once I realized that it is my research that ruins the world. If I could go back in time…If I have never published that research…Wait…I'm a scientist…I could really…"
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