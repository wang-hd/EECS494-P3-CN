using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class fishInfoDisplayer : MonoBehaviour
{
    [SerializeField] GameObject Image;
    [SerializeField] GameObject fishName;
    [SerializeField] GameObject fishStory;
    [SerializeField] GameObject Attack;
    [SerializeField] GameObject Weight;
    [SerializeField] GameObject Hungry;
    [SerializeField] GameObject Health;

    Text fishNameText;
    Text fishStoryText;
    Image image;
    Text attack;
    Text weight;
    Text hungry;
    Text health;

    void Awake()
    {
        fishNameText = fishName.GetComponent<Text>();
        fishStoryText = fishStory.GetComponent<Text>();
        image = Image.GetComponent<Image>();
        attack = Attack.GetComponent<Text>();
        weight = Weight.GetComponent<Text>();
        hungry = Hungry.GetComponent<Text>();
        health = Health.GetComponent<Text>();

    }

    public void DisplayFishInfo(string _fishName, string _fishInfo, Sprite _image,
                                 int _attack, float _weight, int _hungry, int _health, bool _isFish)
    {
        fishNameText.text = _fishName;
        fishStoryText.text = "Fish Story : " + _fishInfo;
        image.sprite = _image;
        if (_isFish)
        {
            Attack.SetActive(true);
            Weight.SetActive(true);
            Hungry.SetActive(true);
            Health.SetActive(true);
            attack.text = _attack.ToString();
            weight.text = _weight.ToString();
            hungry.text = _hungry.ToString();
            health.text = _health.ToString();
        }
        else
        {
            Attack.SetActive(false);
            Weight.SetActive(false);
            Hungry.SetActive(false);
            Health.SetActive(false);
        }


    }

    public void handleFishInfoEvent(fish_info_event e)
    {
        FishData fish = FishList.GetFishWithFishID(e.fishInfoID);
        DisplayFishInfo(fish.getFishName(), fish.getFishStory(), fish.getSprite(), 
                        fish.getAttack(), fish.getWeight(), fish.getHungerValue(), fish.getHealthValue(), fish.getIsFish());

    }


}
