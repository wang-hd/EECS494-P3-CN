using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InLevelLevelManager : MonoBehaviour
{
    public GameObject background_gameobject;
    public Transform player;
    public Sprite[] backgrounds;
    public Vector2[] scale_for_background;
    public Vector2[] player_position_adjustment;

    // Start is called before the first frame update
    void Start()
    {
        background_gameobject.GetComponent<SpriteRenderer>().sprite = backgrounds[StaticData.currentLevelIdx];
        background_gameobject.GetComponent<Transform>().localScale = new Vector3(scale_for_background[StaticData.currentLevelIdx].x, scale_for_background[StaticData.currentLevelIdx].y, 1);
        player.position = new Vector3(player_position_adjustment[StaticData.currentLevelIdx].x, player_position_adjustment[StaticData.currentLevelIdx].y, 1);
    }
}
