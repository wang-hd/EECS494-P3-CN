using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemInfoDisplayer : MonoBehaviour
{
    [SerializeField] GameObject image;
    [SerializeField] GameObject itemName;
    [SerializeField] GameObject itemStory;


    public void DisplayItemInfo(int fishID)
    {
        FishData fish = FishList.GetFishWithFishID(fishID);

        itemName.GetComponent<Text>().text = fish.getFishName();
        itemStory.GetComponent<Text>().text = fish.getFishStory();
        image.GetComponent<Image>().sprite = fish.getSprite();
    }
}
