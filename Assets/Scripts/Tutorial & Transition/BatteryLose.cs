using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BatteryLose : MonoBehaviour
{
    //RectTransform battery_lose;
    [SerializeField] GameObject battery;
    [SerializeField] AudioClip batteryAudio;
    Transform[] batteries;
    Text day_counter_text;
    int day;
    private AudioSource Audio;
    // Start is called before the first frame update
    void Start()
    {
        Audio = GetComponent<AudioSource>();
        //battery_lose = GameObject.Find("BatteryLoseImage").GetComponent<RectTransform>();
        batteries = battery.gameObject.GetComponentsInChildren<Transform>();
        day_counter_text = GameObject.Find("DayCounter").GetComponent<Text>();
        day = StaticData.day;
        int len = batteries.Length;

        //if (day == 1) battery_lose.sizeDelta = new Vector2 (58 * (day - 1), battery_lose.sizeDelta.y);
        //else battery_lose.sizeDelta = new Vector2 (58 * (day - 2), battery_lose.sizeDelta.y);
        for (int i=1; i<day-1; i++)
        {
            batteries[len-i].gameObject.SetActive(false);
        }
        day_counter_text.text = "Day " + (day-1).ToString();

        StartCoroutine(BatteryLoseEffect());
        StartCoroutine(LoadNewScene());
        
    }

    IEnumerator BatteryLoseEffect()
    {
        yield return new WaitForSeconds(0.5f);
        Audio.clip = batteryAudio;
        Audio.Play();
        //AudioSource.PlayClipAtPoint(batteryAudio, Camera.main.transform.position);

        //battery_lose.sizeDelta = new Vector2 (58 * (day - 1), battery_lose.sizeDelta.y);
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        if (StaticData.day == 1 )
        {
            yield return new WaitForSeconds(1f);
        }
        else{
            int len = batteries.Length;
            batteries[len-day+1].gameObject.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            batteries[len-day+1].gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);

            batteries[len-day+1].gameObject.SetActive(false);
            day_counter_text.text = "Day " + day.ToString();
        }
        
    }

    IEnumerator LoadNewScene()
    {
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("Home");
    }

    
}
