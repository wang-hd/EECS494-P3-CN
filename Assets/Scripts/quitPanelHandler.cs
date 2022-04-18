using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quitPanelHandler : MonoBehaviour
{
    public GameObject quitPanel;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quitPanel.SetActive(true);
        }
    }

    public void closeQuitPanel()
    {
        quitPanel.SetActive(false);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
