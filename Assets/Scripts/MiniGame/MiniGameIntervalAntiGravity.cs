using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameIntervalAntiGravity : MiniGameInterval
{
    void Start()
    {
        reel = GameObject.FindGameObjectWithTag("reel");
        reel.transform.SetParent(gameObject.transform);
        GetComponent<Rigidbody2D>().gravityScale = -1.0f;
    }
    protected override void _update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -up_force));
        }
    }
}
