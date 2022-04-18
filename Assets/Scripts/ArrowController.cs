using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    static ArrowController instance;

    private void Awake() {
        // Typical singleton initialization code.
        if (instance != null && instance != this)
        {
            // If there already exists a ToastManager, we need to go away.
            Destroy(gameObject);
            return;
        }
        else
        {
            // If we are the first ToastManager, we claim the "instance" variable so others go away.
            instance = this;
            DontDestroyOnLoad(gameObject); // Survive scene changes
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
