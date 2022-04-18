using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CaughtFishButton : MonoBehaviour
{
    [SerializeField] GameObject CaughtFishPanel;
     public AudioClip fishDropAudio;

    public static bool isFishAnimation = false;

    public void WhenOnClick()
    {
        StartCoroutine(CloseCaughtFishPanel());
    }

    public static void SetFishOrItemAnimation(bool isFish)
    {
        isFishAnimation = isFish;
    }

    IEnumerator CloseCaughtFishPanel()
    {

        while (StaticData.has_open_panel)
        {

            // find position of caught fish image
            GameObject caughtFishImg = GameObject.FindGameObjectWithTag("FishCaughtImg");
            Vector3 original_pos = caughtFishImg.transform.position;

            Vector3 final_position = GameObject.FindGameObjectWithTag("InventoryButton").transform.position;
            if (!isFishAnimation)
            {
                final_position = GameObject.FindGameObjectWithTag("HomeButton").transform.position;
            }
            float middle_x = original_pos.x + Mathf.Abs(final_position.x - original_pos.x) / 2;
            float middle_y = original_pos.y + 300f;
            Vector3 midlle_position = new Vector3(middle_x, middle_y, 1);

            //Debug.Log("original pos is " + original_pos);
            //Debug.Log("final pos is " + final_position);


            // move caught fish image to bag
            yield return StartCoroutine(
                MoveObjectOverTime(caughtFishImg.transform, original_pos, midlle_position, 0.3f, true)
            );
            yield return StartCoroutine(
                MoveObjectOverTime(caughtFishImg.transform, midlle_position, final_position, 0.3f, false)
            );


            AudioSource.PlayClipAtPoint(fishDropAudio, Camera.main.transform.position);

            // set panels to inactive
            CaughtFishPanel.SetActive(false);
            StaticData.has_open_panel = false;

            EventBus.Publish<close_caught_fish_panel_event>(new close_caught_fish_panel_event("fish caught panel closed"));


            // move caught fish img back to original pos for future fish
            caughtFishImg.transform.position = original_pos;
            caughtFishImg.transform.localScale = new Vector3(1f, 1f, 1);

            yield return null;
        }
    }


    IEnumerator MoveObjectOverTime(Transform target, Vector3 initial_pos, Vector3 dest_pos, float duration_sec, bool isFirstHalf)
    {
        float initial_time = Time.time;

        float progress = (Time.time - initial_time) / duration_sec;

        while (progress < 1.0f)
        {

            progress = (Time.time - initial_time) / duration_sec;
            Vector3 new_position = Vector3.Lerp(initial_pos, dest_pos, progress);
            target.position = new_position;

            if (isFirstHalf)
            {
                Vector3 new_scale = Vector3.Lerp(target.transform.localScale, new Vector3(0.7f, 0.7f, 1), progress);
                target.localScale = new_scale;
            }
            else
            {
                Vector3 new_scale = Vector3.Lerp(new Vector3(0.7f, 0.7f, 1), new Vector3(0.2f, 0.2f, 1), progress);
                target.localScale = new_scale;
            }

            // yield until the end of the frame, allowing other code / coroutines to run
            // and allowing time to pass.
            yield return null;
        }

        target.position = dest_pos;
    }

}
