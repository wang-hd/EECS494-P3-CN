using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskControllerForMap : MonoBehaviour
{
    [SerializeField] GameObject MapController;

    LevelSelectManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = MapController.GetComponent<LevelSelectManager>();
        if (StaticData.day == 3 || StaticData.day == 5 || StaticData.day == 7 || StaticData.day == 9)
        {
            // remember to change LevelSelectLevelForcast.cs
            switch (StaticData.day)
            {
                case 3:
                    StaticData.map_progress = 1;
                    break;
                case 5:
                    StaticData.map_progress = 2;
                    break;
                case 7:
                    StaticData.map_progress = 3;
                    break;
                case 9:
                    StaticData.map_progress = 4;
                    break;
                default:
                    break;
            }
            EventBus.Publish<ToastRequest>(new ToastRequest($"New scene {StaticData.getLevelNameByLevelIdx(StaticData.map_progress)} unlocked!", 3.0f, false,false,false));
        }
        for (int i = 0; i <= StaticData.map_progress && i < StaticData.levels.Count; i++)
        {
            Debug.Log(StaticData.map_progress);
            manager.UnlockLevel(StaticData.getLevelNameByLevelIdx(i));
        }
    }
}
