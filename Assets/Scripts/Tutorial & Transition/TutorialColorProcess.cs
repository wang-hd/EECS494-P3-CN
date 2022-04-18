using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TutorialColorProcess : MonoBehaviour
{
    PostProcessVolume volume;
    ColorGrading color_grading;
    float color_change_time = 3f;
    float fade_time = 1f;

    Subscription<time_to_nignt> night_subscription;
    Subscription<refresh_the_day> refresh_subscription;

    // Start is called before the first frame update
    void Start()
    {
        volume = gameObject.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out color_grading);

        night_subscription = EventBus.Subscribe<time_to_nignt>(ChangeNightColor);
        
        color_grading.postExposure.value = 10f;
        StartCoroutine(FadeFromWHite());
        //StartCoroutine(changeColor());
    }

    IEnumerator FadeFromWHite()
    {
        float t = 0;
        while (t <= color_change_time)
        {
            t += Time.deltaTime;
            color_grading.postExposure.value = Mathf.Lerp(10f, -0.1f, t / color_change_time);
            yield return null;

        }
    }

    void ChangeNightColor(time_to_nignt e) 
    {
        StartCoroutine(changeColor());
    }

    IEnumerator changeColor()
    {
        float t = 0;
        while (t <= color_change_time)
        {
            t += Time.deltaTime;
            color_grading.hueShift.value = Mathf.Lerp(0, 70, t / color_change_time);
            color_grading.tint.value = Mathf.Lerp(0, -70, t / color_change_time);
            color_grading.colorFilter.Interp(Color.white, Color.gray, t / color_change_time);
            yield return null;

        }

    }

}
