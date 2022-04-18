using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameInterval : MonoBehaviour
{
    private Rigidbody2D interval_rb2d;
    private FishingMiniGameControler fishing_mini_game_controler;
    public float up_force = 150f;
    [Range(0.5f, 2)] public float mass = 1f;
    protected GameObject reel;
    // Start is called before the first frame update
    void Start()
    {
        interval_rb2d = gameObject.GetComponent<Rigidbody2D>();
        fishing_mini_game_controler = transform.GetComponentInParent<FishingMiniGameControler>();
        interval_rb2d.mass = mass;

        reel = GameObject.FindGameObjectWithTag("reel");
        reel.transform.SetParent(gameObject.transform);
    }

    private void Update()
    {
        _update();
    }

    protected virtual void _update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            interval_rb2d.velocity = Vector2.zero;
            interval_rb2d.AddForce(new Vector2(0, up_force));
        }
        interval_rb2d.mass = mass;
    }
}
