using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Line : MonoBehaviour
{
    public AudioClip taskLineAudio;
    Image line;
    Subscription<ToastRequest> toast_sub;
    Subscription<ToastCrossOut> toast_cross_sub;
    Subscription<RemoveLine> line_remove_sub;
    float draw_time = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        line = gameObject.GetComponent<Image>();
        toast_cross_sub = EventBus.Subscribe<ToastCrossOut>(DrawLine);
        toast_sub = EventBus.Subscribe<ToastRequest>(RemoveLine);
        line_remove_sub = EventBus.Subscribe<RemoveLine>(removeLine);

        line.fillAmount = 0;
    }

    void DrawLine(ToastCrossOut e) 
    {
        line.fillAmount = 0;
        StartCoroutine(Draw());
    }

    IEnumerator Draw() 
    {
        yield return new WaitForSeconds(1f);
        AudioSource.PlayClipAtPoint(taskLineAudio, Camera.main.transform.position);
        float t = 0;
        while (t < draw_time)
        {
            line.fillAmount = t / draw_time;
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    void RemoveLine(ToastRequest e) 
    {
        line.fillAmount = 0;
    }

    void removeLine(RemoveLine e)
    {
        line.fillAmount = 0;
    }
}
