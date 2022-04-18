using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialItemController : MonoBehaviour
{
    public GameObject item_panel;
    SpecialItem[] special_items;

    private void Start() {
        special_items = GetComponentsInChildren<SpecialItem>(true);

        foreach (SpecialItem item in special_items)
        {
            item.gameObject.SetActive(StaticData.special_item_unlock_status[item.fishID]);
        }
    }

    public void OpenPanel(int fishID)
    {
        StaticData.has_open_panel = true;
        item_panel.SetActive(true);
        item_panel.GetComponent<ItemInfoDisplayer>().DisplayItemInfo(fishID);

    }

    public void ClosePanel()
    {
        item_panel.SetActive(false);
        StaticData.has_open_panel = false;
    }
}