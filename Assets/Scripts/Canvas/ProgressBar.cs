using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ProgressBar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Image image;
    public float decrease_speed = 0.001f;
    float CurrentAmount;
    Text value;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        value = GetComponentInChildren<Text>();

        CurrentAmount = 1f;
    }
    void Update()
    {
        if(image.fillAmount >= CurrentAmount + decrease_speed)
        {
            image.fillAmount -= decrease_speed;
        }
        else if (image.fillAmount <= CurrentAmount - decrease_speed)
        {
            image.fillAmount += decrease_speed;
        }
    }
    public void UpdateProgress(int progress)
    {
        CurrentAmount = (float)progress/100;
    }

    public void OnPointerEnter(PointerEventData pointer) 
    {
        if (value)
        {
            value.text = (Mathf.Round(image.fillAmount * 100)).ToString();
            value.enabled = true;
        }
        
    }
    
    public void OnPointerExit(PointerEventData pointer) {
        if (value)
        {
            value.enabled = false;
        }
    }

}
