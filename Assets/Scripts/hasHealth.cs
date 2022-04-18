using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class hasHealth : MonoBehaviour
{
    public AudioClip hurtAudio;

    Subscription<alter_health_event> alterHealth_event_subscription;
    Subscription<alter_hunger_event> alterHunger_event_subscription;

    [SerializeField] GameObject EnergyUI;
    [SerializeField] GameObject HealthUI;
    [SerializeField] GameObject BlackPanel;

    ProgressBar energy_progress;
    ProgressBar health_progress;

    bool is_first_attack = true;
    // Start is called before the first frame update
    void Start()
    {
        alterHealth_event_subscription = EventBus.Subscribe<alter_health_event>(_alterHealth);
        alterHunger_event_subscription = EventBus.Subscribe<alter_hunger_event>(_alterHunger);
        energy_progress = EnergyUI.GetComponent<ProgressBar>();
        health_progress = HealthUI.GetComponent<ProgressBar>();
        health_progress.UpdateProgress(StaticData.health);
        energy_progress.UpdateProgress(StaticData.hunger);
    }

    public void _alterHealth(alter_health_event e)
    {
        if (e.alterHealth < 0)
        {
            AudioSource.PlayClipAtPoint(hurtAudio, Camera.main.transform.position);
        }
        if(is_first_attack)
        {
            if(e.alterHealth < 0)
            {
                EventBus.Publish<update_task_event>(new update_task_event(1,3));
            }
        }

        // Remind the player to eat fish when health is low
        if (StaticData.health > 25 && StaticData.health + e.alterHealth < 25)
        {
            EventBus.Publish<ToastRequest>(new ToastRequest("A friendly reminder, you know you can eat fishes to restore health, right?", 3.0f, false, false, false));
        }
        StaticData.health += e.alterHealth;
        if (StaticData.health > 100)
        {
            StaticData.health = 100;
        }
        else if (StaticData.health <= 0)
        {
            StaticData.health = 0;
            health_progress.UpdateProgress(StaticData.health);
            StartCoroutine(HandlePlayerDie());
        }
        health_progress.UpdateProgress(StaticData.health);



       

    }

    public void _alterHunger(alter_hunger_event e)
    {

        // Remind the player to eat fish when energy is low
        if (StaticData.hunger > 25 && StaticData.hunger + e.alterHunger < 25)
        {
            EventBus.Publish<ToastRequest>(new ToastRequest("Fishes are delicious. They can restore your energy. I am serious.", 3.0f, false, false, false));
        }

        StaticData.hunger += e.alterHunger;
        if (StaticData.hunger > 100)
        {
            StaticData.hunger = 100;
        }
        else if (StaticData.hunger <= 0)
        {
            StaticData.hunger = 0;
            EventBus.Publish<ToastRequest>(new ToastRequest("Too Hungry!",3.0f, false,false,false));
            EventBus.Publish<alter_health_event>(new alter_health_event(-40));
        }
        energy_progress.UpdateProgress(StaticData.hunger);
    }

    public static int getHealth()
    {
        return StaticData.health;
    }

    public static int getHunger()
    {
        return StaticData.hunger;
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe<alter_health_event>(alterHealth_event_subscription);
        EventBus.Unsubscribe<alter_hunger_event>(alterHunger_event_subscription);
    }

    IEnumerator HandlePlayerDie()
    {
        BlackPanel.SetActive(true);
        yield return new WaitForSeconds(3f);
        StaticData.usage = TransitionScreenUsage.to_dead;
        SceneManager.LoadScene("transition");
    }
}
