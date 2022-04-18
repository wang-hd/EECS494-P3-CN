using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoFishing : MonoBehaviour
{
    Button go_fishing;
    private void Start() {
        go_fishing = GetComponent<Button>();
        go_fishing.onClick.AddListener(LoadFishingScene);
    }

    void LoadFishingScene()
    {
        StaticData.usage = TransitionScreenUsage.to_customize;
        StaticData.nextScene = ("LevelSelection");
        StaticData.message = ("Opening the door");
        SceneManager.LoadScene("transition");
    }
}
