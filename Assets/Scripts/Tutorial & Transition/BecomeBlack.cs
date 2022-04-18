using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BecomeBlack : MonoBehaviour
{
    Image image;
    private float speed = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        image.color = Color.Lerp(image.color, Color.black, speed * Time.deltaTime); 
    }
}
