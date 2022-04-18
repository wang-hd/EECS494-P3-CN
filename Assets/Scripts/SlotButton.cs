using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Button use;
    [SerializeField] Button info;
    [SerializeField] GameObject prefab;

    private Fish fish;
    // Start is called before the first frame update
    void Awake()
    {
        use.onClick.AddListener(UseOnClick);
        info.onClick.AddListener(InfoOnClick);
        fish = prefab.GetComponent<Fish>();
    }

    public void OnPointerEnter(PointerEventData pointer) 
    {
        use.gameObject.SetActive(true);
        info.gameObject.SetActive(true);
    }
    
    public void OnPointerExit(PointerEventData pointer) {
        use.gameObject.SetActive(false);
        info.gameObject.SetActive(false);
    }

    void UseOnClick()
    {
        EventBus.Publish<alter_hunger_event>(new alter_hunger_event(fish.getHungerValue()));
        EventBus.Publish<alter_health_event>(new alter_health_event(fish.getHealthValue()));
        EventBus.Publish<use_fish_event>(new use_fish_event(fish.getWeight()));
        //TODO sheid
        Destroy(this.gameObject);
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
