using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Button comeHomeButton;
    [SerializeField] GameObject comeHomeConfirmPanel;
    [SerializeField] GameObject BlackPanel;

    // Start is called before the first frame update
    void Awake()
    {
        if (comeHomeButton)
        {
            //comeHomeButton.onClick.AddListener(comeBackHome);
        }
        if(StaticData.day == 11)
        {
            bool flag = true;
            flag = flag && (StaticData.story_progress == StaticData.task_number);
            foreach(var tmp in StaticData.special_item_unlock_status)
            {
                flag = flag && tmp.Value;
            }
            if(flag){
                StartCoroutine(HandlePlayerWin());
            }else{
                StartCoroutine(HandlePlayerLose());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Test Code
        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                EventBus.Publish<ToastRequest>(new ToastRequest("Toast that will not disappear itself!", 0.2f, true));
                EventBus.Publish<ToastRequest>(new ToastRequest("Toast that will disappear itself!", 1f, false));
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                GetComponent<PrefabInstantiater>().MiniGameInstantiate(gameObject.transform);
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                StaticData.current_rod_index = 1 - StaticData.current_rod_index;
            }
        }

        // End of Test Code
        if(StaticData.day == 11)
        {
            bool flag = true;
            flag = flag && (StaticData.story_progress == StaticData.task_number);
            foreach(var tmp in StaticData.special_item_unlock_status)
            {
                flag = flag && tmp.Value;
            }
            if(flag){
                StartCoroutine(HandlePlayerWin());
            }else{
                StartCoroutine(HandlePlayerLose());
            }
        }
    }

    public void comeBackHome()
    {
        StaticData.has_open_panel = true;
        if(StaticData.day == 10)
        {
            bool flag = true;
            flag = flag && (StaticData.story_progress == StaticData.task_number);
            foreach(var tmp in StaticData.special_item_unlock_status)
            {
                flag = flag && tmp.Value;
            }
            if(flag){
                StaticData.ending = 2;
                StartCoroutine(HandlePlayerWin());
            }else{
                StaticData.ending = 1;
                StartCoroutine(HandlePlayerLose());
            }
        }


        //if inventory > some value; Publish
        //else, restart the game;
        EventBus.Publish<refresh_the_day>(new refresh_the_day(StaticData.day));
        //SceneManager.LoadScene(0);//restart the game
        //if have time, want to make some UI
        
    }

    IEnumerator HandlePlayerWin()
    {
        BlackPanel.SetActive(true);
        yield return new WaitForSeconds(3f);
        StaticData.usage = TransitionScreenUsage.to_finish;
        StaticData.message = "Congratulations! You have reached to the happy end of this story, but the team is still working on it...";
        SceneManager.LoadScene("Ending");
    }
    IEnumerator HandlePlayerLose()
    {
        BlackPanel.SetActive(true);
        yield return new WaitForSeconds(3f);
        StaticData.usage = TransitionScreenUsage.to_finish;
        StaticData.message = "Congratulations! You have reached to the bad end of this story, but the tram is still working on it...";
        SceneManager.LoadScene("Ending");
    }

}
