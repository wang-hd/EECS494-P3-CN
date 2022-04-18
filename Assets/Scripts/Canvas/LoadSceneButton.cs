using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    [SerializeField] Transform panel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ComeBackHome(){
        StaticData.clearInventory();
        foreach (Transform fish in panel)
        {
            // print(fish.gameObject.GetComponent<SlotButton>().GetInstanceID());
            if (fish != null)
            {
                StaticData.inventory.Add(fish.gameObject.GetComponent<SlotButton>().getFishID());
            }
            
        }
        StaticData.has_open_panel = false;
        StaticData.usage = TransitionScreenUsage.to_home;
        SceneManager.LoadScene("transition");
    }
}
