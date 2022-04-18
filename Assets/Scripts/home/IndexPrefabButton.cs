using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndexPrefabButton : MonoBehaviour
{
    [SerializeField] Button info;
    [SerializeField] GameObject prefab;

    private Fish fish;
    // Start is called before the first frame update
    void Awake()
    {
        info.onClick.AddListener(InfoOnClick);
        fish = prefab.GetComponent<Fish>();
    }


    void InfoOnClick()
    {
        //FishData fish = FishList.GetFishWithFishID(getFishID());
        //string content = "";
        //if (fish.getAttack() > 0) { content = "This is a fish that will cause " + fish.getAttack().ToString() + " damage."; EventBus.Publish<ToastRequest>(new ToastRequest(content)); }
        //if (fish.getHealthValue() > 0) { content = "This is a fish that will increase your health value by " + fish.getHealthValue().ToString() + "."; EventBus.Publish<ToastRequest>(new ToastRequest(content)); }
        //if (fish.getHungerValue() > 0) { content = "This is a fish that will increase your hunger value by " + fish.getHungerValue().ToString() + "."; EventBus.Publish<ToastRequest>(new ToastRequest(content)); }

        EventBus.Publish<fish_info_event>(new fish_info_event("Fish info requested with ID ", getFishID()));

    }

    public int getFishID()
    {
        return fish.getFishID();
    }
}
