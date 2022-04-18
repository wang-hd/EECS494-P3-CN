using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowFishBones : MonoBehaviour
{
    Text fish_bones;
    // Start is called before the first frame update
    void Start()
    {
        fish_bones = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        fish_bones.text = StaticData.bones.ToString();
    }
}
