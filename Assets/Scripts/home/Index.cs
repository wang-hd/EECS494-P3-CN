using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Index : MonoBehaviour
{
    public GameObject panel;
    public GameObject outline;
    [SerializeField] GameObject[] prefabs;
    [SerializeField] GameObject fish_panel;
    [SerializeField] AudioClip index_audio;

    private void OnMouseEnter()
    {
        if (!StaticData.has_open_panel)
        {
            outline.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        if (!StaticData.has_open_panel)
        {
            outline.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        outline.SetActive(false);
        if (!StaticData.has_open_panel)
        {
            AudioSource.PlayClipAtPoint(index_audio, Camera.main.transform.position);
            OpenPanel();
        }

    }

    public void OpenPanel()
    {
        StaticData.has_open_panel = true;
        clearFishPanel();
        List<int> fishIDs = new List<int> { };
        for (int i = 0; i < StaticData.getIndexLength(); ++i)
        {
            if (FishList.GetFishWithFishID(i).isFish)
            {
                fishIDs.Add(i);
            }
        }
        fishIDs.Sort((f1, f2) =>
        {
            if (!FishList.GetFishWithFishID(f1).isFish)
            {
                return 1;
            }
            if (!FishList.GetFishWithFishID(f2).isFish)
            {
                return -1;
            }
            int res = FishList.GetFishWithFishID(f1).fish_rarity - FishList.GetFishWithFishID(f2).fish_rarity;
            if (res == 0)
            {
                return f1 - f2;
            }
            return res;
        });

        for (int i = 0; i < StaticData.getIndexLength(); ++i)
        {

            GameObject go = Instantiate(prefabs[fishIDs[i]], prefabs[fishIDs[i]].transform.position, Quaternion.identity);
            if (StaticData.checkIndex(fishIDs[i]))
            {
                go.transform.GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255);
                go.transform.GetChild(1).gameObject.SetActive(true);
            }
            go.transform.SetParent(fish_panel.transform, false);
        }
        panel.SetActive(true);
    }

    public void ClosePanel()
    {
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
}
