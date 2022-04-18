using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishInfoBackButtonHandler : MonoBehaviour
{
    [SerializeField] GameObject FishInfoPanel;

    public void WhenOnClick()
    {
        FishInfoPanel.SetActive(false);
    }
}
