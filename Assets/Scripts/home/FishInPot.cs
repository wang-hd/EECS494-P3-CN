using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishInPot : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    private Fish fish;

    Toggle toggle;
    Image image;

    void Awake() {
        toggle = GetComponent<Toggle>();
        image = GetComponent<Image>();
        toggle.onValueChanged.AddListener( MoveFishInPot );
        toggle.onValueChanged.AddListener( OnToggleValueChanged ) ;

        fish = prefab.GetComponent<Fish>();
    }

    void MoveFishInPot(bool isOn)
    {
        if (isOn)
        {
            Kitchen.pot.Add(this.gameObject);
            Kitchen.bones_in_pot += fish.getWeight();
        }
        else 
        {
            Kitchen.pot.Remove(this.gameObject);
            Kitchen.bones_in_pot -= fish.getWeight();
        }
        EventBus.Publish<switch_fish_event>(new switch_fish_event());
    }

    private void OnToggleValueChanged(bool isOn)
    {
        Color c = image.color;
        if (isOn)
        {
            image.color = Color.gray;
        }
        else
        {
            image.color = Color.white;
        }
    }

    public int getFishID()
    {
        return fish.getFishID();
    }

}
