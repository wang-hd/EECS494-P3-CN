using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabInstantiater : MonoBehaviour
{
    public GameObject fishing_mini_game_prefab;

    public GameObject fishing_mini_game_tutorial_prefab;
    
    public void MiniGameInstantiate(Transform trans, int[] fish_ids = null)
    {
        GameObject fishing_mini_game_object = Instantiate(fishing_mini_game_prefab, trans);
        fishing_mini_game_object.transform.position = trans.position + new Vector3(-2.5f, 0.5f, 0);
        fishing_mini_game_object.transform.localScale = new Vector3(1, 1, 1);
        FishingMiniGameControler fishing_mini_game_controler = fishing_mini_game_object.GetComponent<FishingMiniGameControler>();
        //fishing_mini_game_controler.SetIntervalSize(interval_length);
        //fishing_mini_game_controler.SetFishSpeed(fish_speed);
        //fishing_mini_game_controler.SetUpForce(up_force);
        fishing_mini_game_controler.setFishIDs(fish_ids);
    }

    public void MiniGameTutorialInstantiate(Transform trans)
    {
        GameObject fishing_mini_game_object = Instantiate(fishing_mini_game_tutorial_prefab, trans);
        fishing_mini_game_object.transform.position = trans.position + new Vector3(0, 0, 0);
        fishing_mini_game_object.transform.localScale = new Vector3(1, 1, 1);
        FishingMiniGameControler fishing_mini_game_controler = fishing_mini_game_object.GetComponent<FishingMiniGameControler>();
        //fishing_mini_game_controler.SetIntervalSize(interval_length);
        //fishing_mini_game_controler.SetFishSpeed(fish_speed);
        //fishing_mini_game_controler.SetUpForce(up_force);
        int[] fish_ids = new int[5] { -1, -1, -1, -1, -1 };
        fishing_mini_game_controler.setFishIDs(fish_ids);
    }
}
