using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameIntervalHorizontal : MiniGameInterval
{
    void Start()
    {
        GetComponent<ConstantForce2D>().force = new Vector2(-9.8f, 0);

        reel = GameObject.FindGameObjectWithTag("reel");
        reel.transform.Rotate(0, 0, -90);
        reel.transform.position = new Vector3(-4.3f, 2.16f, 0);
        reel.transform.SetParent(gameObject.transform);
    }

    protected override void _update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(up_force, 0));
        }
    }
}
