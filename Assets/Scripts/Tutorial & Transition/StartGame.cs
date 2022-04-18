using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] GameObject BlackCanvas;
    [SerializeField] Sprite[] backgrounds;
    [SerializeField] GameObject background;
    [SerializeField] GameObject start_button;
    [SerializeField] GameObject quit_button;
    [SerializeField] GameObject cast_rod_button;
    [SerializeField] GameObject world_end_image;
    [SerializeField] GameObject rod_image;
    [SerializeField] Text story_text;
    [SerializeField] GameObject[] guide_texts;
    [SerializeField] AudioClip typing_audio;
    [SerializeField] GameObject skip;


    float show_image_time = 3f;
    

    Subscription<tutorial_get_fish_event> fish_get_event;
    Subscription<tutorial_fish_escape_event> fish_escape_event;

    bool is_showing_guide = false;
    int current_guide = 0;

    private AudioSource Audio;

    private void Start() {
        Audio = GetComponent<AudioSource>();
        Audio.clip = typing_audio;
        fish_escape_event = EventBus.Subscribe<tutorial_fish_escape_event>(MiniGameOnceAgain);
        fish_get_event = EventBus.Subscribe<tutorial_get_fish_event>(MiniGameSuccess);

        System.Random rand = new System.Random();
        int randint = rand.Next(0, backgrounds.Length);
        background.GetComponent<SpriteRenderer>().sprite = backgrounds[randint];
    }

    private void Update() {
        if (is_showing_guide) 
        {
            if (Input.GetMouseButtonDown(0) && current_guide <= 2)
            {
                guide_texts[current_guide].SetActive(true);
                current_guide ++;
            }
        }
    }


    public void StartTheGame()
    {
        BlackCanvas.SetActive(true);
        StartCoroutine(LoadNewScene());
    }
    IEnumerator LoadNewScene()
    {
        skip.SetActive(true);
        yield return new WaitForSeconds(2f);
        BlackCanvas.SetActive(false);
        background.SetActive(false);
        start_button.SetActive(false);
        quit_button.SetActive(false);
        StaticData.usage = TransitionScreenUsage.to_customize;
        StaticData.nextScene = "Main Scene";

        Audio.Play();
        story_text.text = "About 8 years ago, a nuclear war ruined the world.";
        yield return new WaitForSeconds(2f);
        Audio.Stop();
        story_text.text = "";

        // show world end image
        float t = 0;
        world_end_image.SetActive(true);
        Vector3 init_pos = world_end_image.transform.position;
        Vector3 end_pos = init_pos;
        end_pos.x -= 8;
        while (t < show_image_time)
        {
            world_end_image.transform.position = Vector3.Lerp(init_pos, end_pos, t / show_image_time);
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        world_end_image.SetActive(false);

        Audio.Play();
        story_text.text = "My name is Stein. Ran to this island to avoid the war.";
        yield return new WaitForSeconds(2f);
        Audio.Stop();

        Audio.Play();
        story_text.text = "";
        yield return new WaitForSeconds(1f);
        Audio.Stop();

        Audio.Play();
        story_text.text = "And found there's not much resources except fish.";
        yield return new WaitForSeconds(1f);
        Audio.Stop();


        cast_rod_button.SetActive(true);
        rod_image.SetActive(true);

    }

    public void CastRod()
    {
        cast_rod_button.SetActive(false);
        rod_image.SetActive(false);
        StartCoroutine(StartFishing());
    }

    IEnumerator StartFishing()
    {
        yield return new WaitForSeconds(0.2f);
        story_text.text = "";
        is_showing_guide = true;
        guide_texts[0].SetActive(true);
             

        yield return new WaitForSeconds(1f);
        current_guide = 1;
        GetComponent<PrefabInstantiater>().MiniGameTutorialInstantiate(transform);
    }

    void MiniGameOnceAgain(tutorial_fish_escape_event e) 
    {
        StartCoroutine(MiniGameReset());
    }

    IEnumerator MiniGameReset()
    {
        yield return new WaitForSeconds(0.5f);
        is_showing_guide = false;
        foreach (GameObject text in guide_texts) text.SetActive(false);

        Audio.Play();
        story_text.text = "...Once Again";

        yield return new WaitForSeconds(1f);
        Audio.Stop();
        cast_rod_button.SetActive(true);
        rod_image.SetActive(true);
    }


    void MiniGameSuccess(tutorial_get_fish_event e)
    {
        StartCoroutine(MiniGameDone());
    }

    IEnumerator MiniGameDone()
    {
        yield return new WaitForSeconds(0.5f);
        is_showing_guide = false;
        foreach (GameObject text in guide_texts) text.SetActive(false);
        Audio.Play();
        story_text.text = "Just like that.";
        yield return new WaitForSeconds(1f);

        story_text.text = "";
        yield return new WaitForSeconds(0.5f);
        
        story_text.text = "I don't know if the war has ended.";
        yield return new WaitForSeconds(2f);

        story_text.text = "Let me just persist a little bit longer,";
        yield return new WaitForSeconds(2f);

        story_text.text = "Let's say,";
        yield return new WaitForSeconds(1f);

        story_text.text = "Let's say,\nTen days.";
        yield return new WaitForSeconds(2f);

        story_text.text = "";
        yield return new WaitForSeconds(1f);
        
        Audio.Stop();
        SceneManager.LoadScene("Main Scene");

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Skip()
    {
        StaticData.usage = TransitionScreenUsage.to_home;
        SceneManager.LoadScene("Main Scene");
    }


}    
