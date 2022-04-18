using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameIntervalReverse : MiniGameInterval
{

    protected override void _update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(GetComponent<Rigidbody2D>().gravityScale == 1.0f)
            {
                GetComponent<Rigidbody2D>().gravityScale = -1.0f;
            }
            else
            {
                GetComponent<Rigidbody2D>().gravityScale = 1.0f;
            }
            
        }
    }
}
