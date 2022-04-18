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

        subtitle.text = "好吧，这种生活倒也不坏。";
        second_subtitle.text = "";
        yield return new WaitForSeconds(2f);

        subtitle.text = "";
        yield return new WaitForSeconds(1f);

        subtitle.text = "我最后还是决定余生都在那座小岛上度过，";
        yield return new WaitForSeconds(2f);

        subtitle.text = "";
        yield return new WaitForSeconds(1f);

        subtitle.text = "并且再也不去想什么核武器，什么核战争了。";
        yield return new WaitForSeconds(3f);

        subtitle.text = "";
        yield return new WaitForSeconds(2f);

        subtitle.text = "Normal Ending:\n\n\n";
        second_subtitle.text = "没有尽头的钓鱼物语";
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

        subtitle.text = "2034 A.D.\n科学家们宣布地球上所有的能源仅能最后维持200年左右的人类活动。能源危机由此开始。";
        second_subtitle.text = "";
        item.SetActive(true);
        item.GetComponent<Image>().sprite = items[0];
        yield return new WaitForSeconds(4.5f);

        subtitle.text = "";
        item.SetActive(false);
        yield return new WaitForSeconds(1f);

        subtitle.text = "2042 A.D.\nStein博士提出新的核反应公式，大幅降低了核能的利用成本，廉价的核能让人类看到了生存下去的希望，同年Stein博士获贝诺尔物理学奖。";
        item.SetActive(true);
        item.GetComponent<Image>().sprite = items[1];
        yield return new WaitForSeconds(10f);

        subtitle.text = "";
        item.SetActive(false);
        yield return new WaitForSeconds(1f);

        subtitle.text = "2048 A.D.\n便携、易用、高效的核能已全面普及，每个家庭都拥有了自己的核反应堆能源站";
        item.SetActive(true);
        item.GetComponent<Image>().sprite = items[2];
        yield return new WaitForSeconds(8f);

        subtitle.text = "";
        item.SetActive(false);
        yield return new WaitForSeconds(1f);

        subtitle.text = "2050 A.D.\n随着能源危机的结束，解决了危机的人类把注意转向了抢夺领土。便宜危险的核武器在战争中大放异彩，消灭了世界80%的人口";
        item.SetActive(true);
        item.GetComponent<Image>().sprite = items[3];
        yield return new WaitForSeconds(10f);

        subtitle.text = "";
        item.SetActive(false);
        yield return new WaitForSeconds(1f);

        subtitle.text = "2052 A.D.\nSteins博士从公众视野中消失了。只有他自己知道，他在一个有很多鱼的小岛上开始了一段新生活。";
        item.SetActive(true);
        item.GetComponent<Image>().sprite = items[4];
        yield return new WaitForSeconds(6f);

        subtitle.text = "";
        item.SetActive(false);
        yield return new WaitForSeconds(2.5f);

        subtitle.text = "True Ending:\n\n\n";
        second_subtitle.text = "这是我的核战争";
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

        second_subtitle.text = "Stein博士，\n世界末日和鱼\n\n";
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
