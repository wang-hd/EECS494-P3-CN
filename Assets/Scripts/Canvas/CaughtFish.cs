using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaughtFish : MonoBehaviour
{
    [SerializeField] GameObject FishName;
    [SerializeField] GameObject Image;
    [SerializeField] GameObject Attack;
    [SerializeField] GameObject Weight;
    [SerializeField] GameObject Hungry;
    [SerializeField] GameObject Health;

    Text fishname;
    Text attack;
    Text weight;
    Text hungry;
    Text health;
    Image image;

    void Awake()
    {
        fishname = FishName.GetComponent<Text>();
        attack = Attack.GetComponent<Text>();
        weight = Weight.GetComponent<Text>();
        hungry = Hungry.GetComponent<Text>();
        health = Health.GetComponent<Text>();
        image = Image.GetComponent<Image>();
    }

    public void DisplayFish(string _name, int _attack, float _weight, int _hungry, int _health, Sprite _image, bool _isFish)
    {
        fishname.text = _name;
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
            CaughtFishButton.SetFishOrItemAnimation(true);
        }
        else
        {
            Attack.SetActive(false);
            Weight.SetActive(false);
            Hungry.SetActive(false);
            Health.SetActive(false);
            CaughtFishButton.SetFishOrItemAnimation(false);
        }

    }


}
