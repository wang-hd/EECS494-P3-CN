using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    public GameObject panel;
    public GameObject outline;
    [SerializeField] GameObject[] prefabs;
    [SerializeField] GameObject fish_panel;
    [SerializeField] AudioClip fish_use_audio;
    [SerializeField] AudioClip bag_audio;
    Subscription<use_fish_event> useFish_event_subscription;

    private void Awake()
    {
        useFish_event_subscription = EventBus.Subscribe<use_fish_event>(handleFishUseEvent);
    }

    private void OnMouseEnter() {
        if (!StaticData.has_open_panel) 
        {
            outline.SetActive(true);
        }
    }

    private void OnMouseExit() {
        if (!StaticData.has_open_panel) 
        {
            outline.SetActive(false);
        }
    }

    private void OnMouseDown() {
        outline.SetActive(false);
        if (!StaticData.has_open_panel) 
        {
            AudioSource.PlayClipAtPoint(bag_audio, Camera.main.transform.position);
            OpenPanel();
        }
        
    }

    public void OpenPanel()
    {
        if (StaticData.has_open_panel) { return; }
        StaticData.has_open_panel = true;
        clearFishPanel();
        foreach (int fishID in StaticData.inventory)
        {
            GameObject go = Instantiate(prefabs[fishID], prefabs[fishID].transform.position, Quaternion.identity);
            go.transform.SetParent(fish_panel.transform, false);
        }
        panel.SetActive(true);
    }

    public void ClosePanel()
    {
        StaticData.clearInventory();
        foreach (Transform fish in fish_panel.transform)
        {
            if (fish != null)
            {
                StaticData.inventory.Add(fish.gameObject.GetComponent<SlotButton>().getFishID());
            }

        }
        panel.SetActive(false);
        StaticData.has_open_panel = false;
    }


    private void clearFishPanel()
    {
        foreach (Transform fish in fish_panel.transform)
        {
            if (fish != null)
            {
                Destroy(fish.gameObject);
            }

        }
    }

    private void handleFishUseEvent(use_fish_event e)
    {
        AudioSource.PlayClipAtPoint(fish_use_audio, Camera.main.transform.position);
    }
}