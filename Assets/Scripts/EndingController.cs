using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingController : MonoBehaviour
{
    [SerializeField] GameObject battery_lose_rectangle;
    [SerializeField] GameObject day_counter;
    [SerializeField] GameObject battery_image;
    [SerializeField] GameObject ending_text;
    [SerializeField] GameObject second_text;
    [SerializeField] GameObject scroll_text;
    [SerializeField] GameObject second_scroll_text;
    [SerializeField] GameObject underwater_image;
    [SerializeField] Sprite[] items;
    [SerializeField] GameObject item;
    [SerializeField] AudioClip bgm_true;
    [SerializeField] AudioClip bgm_normal;
    Text subtitle;
    Text second_subtitle;
    Text scroll;
    Text second_scroll;
    float move_time = 5f;
    float credits_time = 8f;
    // Start is called before the first frame update
    void Start()
    {
        subtitle = ending_text.GetComponent<Text>();
        second_subtitle = second_text.GetComponent<Text>();
        scroll = scroll_text.GetComponent<Text>();
        second_scroll = second_scroll_text.GetComponent<Text>();
        // if dead
        if (StaticData.ending == 0) StartCoroutine(Credits());
        else StartCoroutine(BatteryBlink());
        //StartCoroutine(Credits());
    }

    IEnumerator BatteryBlink()
    {

        yield return new WaitForSeconds(1);
        battery_lose_rectangle.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        battery_lose_rectangle.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Blink(false);
        yield return new WaitForSeconds(0.1f);
        Blink(true);
        yield return new WaitForSeconds(0.1f);
        Blink(false);

        yield return new WaitForSeconds(0.5f);
        battery_image.GetComponent<SpriteRenderer>().color = Color.black;
        
        yield return new WaitForSeconds(2f);


        if (StaticData.ending == 1) StartCoroutine(NormalEnding());
        else if (StaticData.ending == 2) StartCoroutine(TrueEnding());

    }

    void Blink(bool state)
    {
        battery_lose_rectangle.SetActive(state);
        day_counter.SetActive(state);
    }

    IEnumerator NormalEnding()
    {
        AudioSource.PlayClipAtPoint(bgm_normal, Camera.main.transform.position);
        ending_text.SetActive(true);
        second_text.SetActive(true);

        subtitle.text = "Well, living such a life isn't bad at all.";
        second_subtitle.text = "";
        yield return new WaitForSeconds(2f);

        subtitle.text = "";
        yield return new WaitForSeconds(1f);

        subtitle.text = "I finally decided to spend rest of my life on that island,";
        yield return new WaitForSeconds(2f);

        subtitle.text = "";
        yield return new WaitForSeconds(1f);

        subtitle.text = "And we don't talk about nuclear or wars any more.";
        yield return new WaitForSeconds(3f);

        subtitle.text = "";
        yield return new WaitForSeconds(2f);

        subtitle.text = "Normal Ending:\n\n\n";
        second_subtitle.text = "Endless Fishing Tale";
        subtitle.color = new Color32(81, 171, 71, 255);
        second_subtitle.color = new Color32(81, 171, 71, 255);
        yield return new WaitForSeconds(3f);

        subtitle.text = "";
        second_subtitle.text = "";
        subtitle.color = Color.white;
        second_subtitle.color = Color.white;
        yield return new WaitForSeconds(2f);

        StartCoroutine(Credits());
        
    }

    IEnumerator TrueEnding()
    {
        ending_text.SetActive(true);
        second_text.SetActive(true);
        AudioSource.PlayClipAtPoint(bgm_true, Camera.main.transform.position);

        subtitle.text = "2034 A.D.\nScientists estimated that all energy sources on earth would be exhausted after 200 years. The Energy Crisis starts.";
        second_subtitle.text = "";
        item.SetActive(true);
        item.GetComponent<Image>().sprite = items[0];
        yield return new WaitForSeconds(4.5f);

        subtitle.text = "";
        item.SetActive(false);
        yield return new WaitForSeconds(1f);

        subtitle.text = "2042 A.D.\nDr. Stein developed a new nuclear reaction, and greatly reduced the cost of nuclear usage. Wide use of nuclear energy gives people hope of surviving the Energy Crisis, and in the same year Dr. Steins is awarded Belno Prize.";
        item.SetActive(true);
        item.GetComponent<Image>().sprite = items[1];
        yield return new WaitForSeconds(10f);

        subtitle.text = "";
        item.SetActive(false);
        yield return new WaitForSeconds(1f);

        subtitle.text = "2050 A.D.\nEasy-to-operate, high-efficiency nuclear reactors have become extremely popular. Now every family has their own family nuclear reactors.";
        item.SetActive(true);
        item.GetComponent<Image>().sprite = items[2];
        yield return new WaitForSeconds(8f);

        subtitle.text = "";
        item.SetActive(false);
        yield return new WaitForSeconds(1f);

        subtitle.text = "2058 A.D.\nWith the end of the Energy Crisis, humans turn to conflicts of territories. Cheap but fatal nuclear weapons bloom and flourish in wars, and wipe out 80% of the world population.";
        item.SetActive(true);
        item.GetComponent<Image>().sprite = items[3];
        yield return new WaitForSeconds(10f);

        subtitle.text = "";
        item.SetActive(false);
        yield return new WaitForSeconds(1f);

        subtitle.text = "2066 A.D.\nDr. Steins disappeared from public view. Only himself know he starts a new life on a island full of fishes.";
        item.SetActive(true);
        item.GetComponent<Image>().sprite = items[4];
        yield return new WaitForSeconds(6f);

        subtitle.text = "";
        item.SetActive(false);
        yield return new WaitForSeconds(2.5f);

        subtitle.text = "True Ending:\n\n\n";
        second_subtitle.text = "This Nuclear War of Mine";
        item.SetActive(false);
        subtitle.color = new Color32(81, 171, 71, 255);
        second_subtitle.color = new Color32(81, 171, 71, 255);
        yield return new WaitForSeconds(3f);

        subtitle.text = "";
        second_subtitle.text = "";
        subtitle.color = Color.white;
        second_subtitle.color = Color.white;
        yield return new WaitForSeconds(2f);

        StartCoroutine(Credits());
        
    }

    IEnumerator Credits()
    {
        scroll_text.SetActive(true);
        second_scroll_text.SetActive(true);
        Vector3 init_pos = scroll_text.transform.position;
        Vector3 end_pos = init_pos;
        end_pos.y += 2300;

        second_subtitle.text = "Stein's Fishing Tale\n\n";
        subtitle.text = "A game by loIs studio";
        yield return new WaitForSeconds(2f);
        Vector3 text_1_init_pos = ending_text.transform.position;
        Vector3 text_2_ini_pos = second_text.transform.position;
        Vector3 text_end_pos = text_2_ini_pos;
        text_end_pos.y += 2300;

        scroll.text = "loIs studio\n\n\n\n\n\n\n\n\nFreelancers\n\n\n\n\n\nThank you for playing!";
        second_scroll.text = "\n\n(lots of International students)\n\n\n\n\nAnna Li\nDoris Li\nHongdan Wang\n"
        + "Zhige Wang\nZhijie Zhao\n\n\n\n\n\nGreat thanks to";

        float t = 0f;
        while (t < credits_time)
        {
            ending_text.transform.position = Vector3.Lerp(text_1_init_pos, text_end_pos, t / credits_time);
            second_text.transform.position = Vector3.Lerp(text_2_ini_pos, text_end_pos, t / credits_time);
            scroll_text.transform.position = Vector3.Lerp(init_pos, end_pos, t / credits_time);
            second_scroll_text.transform.position = Vector3.Lerp(init_pos, end_pos, t / credits_time);
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(5f);
        scroll.text = "";
        second_scroll.text = "";
        subtitle.text = "";
        second_subtitle.text = "";


        if (StaticData.ending == 1)
        {
            ClearStaticData();
            StartCoroutine(Underwater());
        }
        else 
        {
            ClearStaticData();
            SceneManager.LoadScene("Begin");
        }
        
    }

    IEnumerator Underwater()
    {
        underwater_image.SetActive(true);
        float t = 0f;
        Vector3 init_pos = underwater_image.transform.position;
        Vector3 end_pos = init_pos;
        end_pos.y += 18.4f;
        while (t < move_time)
        {
            underwater_image.transform.position = Vector3.Lerp(init_pos, end_pos, t / move_time);
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Begin");
    }

    void ClearStaticData()
    {
        StaticData.Refresh();
        StaticData.clearInventory();
    }
}
