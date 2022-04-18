using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishingTutorial : MonoBehaviour
{
    [SerializeField] GameObject cast_rod_button;
    [SerializeField] GameObject cast_rod_panel;
    [SerializeField] GameObject fish_count;
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject inventory_panel;
    [SerializeField] GameObject fishbone;
    [SerializeField] GameObject fishbone_panel;
    [SerializeField] GameObject satiety;
    [SerializeField] GameObject health;
    [SerializeField] GameObject satiety_panel;
    [SerializeField] GameObject health_panel;
    [SerializeField] GameObject time_panel;
    [SerializeField] GameObject home;
    [SerializeField] GameObject home_panel;
    [SerializeField] GameObject task;
    [SerializeField] GameObject task_small;
    [SerializeField] GameObject task_panel;

    bool has_finished_inventory_tutorial = false;
    Subscription<get_fish_event> get_fish_subs;
    [SerializeField] GameObject clock;
    Clock present_time;


    bool is_panel_open = false;


    // Start is called before the first frame update
    void Start()
    {
        get_fish_subs = EventBus.Subscribe<get_fish_event>(StartInventoryTutorial);
        present_time = clock.GetComponent<Clock>();


        StartCastRodTutorial();
    }

    

    void StartCastRodTutorial()
    {
        StartCoroutine(CastRodTutorial());
    }

    IEnumerator CastRodTutorial() 
    {
        yield return new WaitForSeconds(4);

        cast_rod_button.SetActive(true);
        cast_rod_panel.SetActive(true);
    }

    void StartInventoryTutorial(get_fish_event e) 
    {
        StartCoroutine(InventoryTutorial());
    }

    IEnumerator InventoryTutorial()
    {
        yield return new WaitForSeconds(1);

        inventory.SetActive(true);
        inventory_panel.SetActive(true);

    }

    public void StartFishboneTutorial() 
    {
        StartCoroutine(FishboneTutorial());
    }

    IEnumerator FishboneTutorial()
    {
        yield return new WaitForSeconds(1);

        fishbone.SetActive(true);
        fishbone_panel.SetActive(true);
    }

    public void StartSatietyTutorial()
    {
        if (!has_finished_inventory_tutorial) StartCoroutine(SatietyTutorial());
    }

    IEnumerator SatietyTutorial()
    {
        yield return new WaitForSeconds(1);

        satiety.SetActive(true);
        satiety_panel.SetActive(true);
    }

    public void StartHealthTutorial()
    {
        StartCoroutine(HealthTutorial());
    }

    IEnumerator HealthTutorial()
    {
        yield return new WaitForSeconds(0.5f);

        health.SetActive(true);
        health_panel.SetActive(true);
    }

    public void StartTimeTutorial()
    {
        StartCoroutine(TimeTutorial());
    }

    IEnumerator TimeTutorial()
    {
        yield return new WaitForSeconds(1f);

        time_panel.SetActive(true);
    }

    public void StartHomeTutorial()
    {
        StartCoroutine(HomeTutorial());
    }

    IEnumerator HomeTutorial()
    {
        yield return new WaitForSeconds(1f);

        home.SetActive(true);
        home_panel.SetActive(true);
    }

    public void StartTaskTutorial()
    {
        StartCoroutine(TaskTutorial());
    }

    IEnumerator TaskTutorial()
    {
        yield return new WaitForSeconds(1f);

        task.SetActive(true);
        task_small.SetActive(true);
        task_panel.SetActive(true);
    }

    public void EndTutorial()
    {
        StaticData.ini_time = present_time.GetTime();
        SceneManager.LoadScene("Main Scene");
    }

    public void CloseTutorialPanel()
    {
        is_panel_open = false;
    }
}
