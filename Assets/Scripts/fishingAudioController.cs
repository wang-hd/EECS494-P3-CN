using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishingAudioController : MonoBehaviour
{
    Subscription<get_fish_event> getFish_event_subscription;
    Subscription<fish_escape_event> fishEscape_event_subscription;
    Subscription<fish_hooked_event> fishHook_event_subscription;
    Subscription<get_item_event> getItem_event_subscription;

    public AudioClip bubbleAudio;
    public AudioClip catchFishAudio;
    public AudioClip fishHookedAudio;
    public AudioClip fishEscapedAudio;
    private AudioSource Audio;
    //AudioClip catchFishAudio;

    // Start is called before the first frame update
    void Start()
    {
        getFish_event_subscription = EventBus.Subscribe<get_fish_event>(handleFishCaught);
        fishEscape_event_subscription = EventBus.Subscribe<fish_escape_event>(handleFishEscape);
        fishHook_event_subscription = EventBus.Subscribe<fish_hooked_event>(handleFishHooked);
        getItem_event_subscription = EventBus.Subscribe<get_item_event>(handleItemCaught);


        Audio = GetComponent<AudioSource>();
    }

    void handleFishCaught(get_fish_event e)
    {
        Audio.Stop();
        if (FishList.GetFishWithFishID(e.fishHookedID).getAttack() == 0)
        {
            AudioSource.PlayClipAtPoint(catchFishAudio, Camera.main.transform.position);
        }
    }

    void handleItemCaught(get_item_event e)
    {
        Audio.Stop();
        AudioSource.PlayClipAtPoint(catchFishAudio, Camera.main.transform.position);
    }

    void handleFishHooked(fish_hooked_event e)
    {
        Audio.Stop();
        AudioSource.PlayClipAtPoint(fishHookedAudio, Camera.main.transform.position);
        Audio.clip = bubbleAudio;
        Audio.loop = true;
        Audio.Play();
    }

    void handleFishEscape(fish_escape_event e)
    {
        Audio.Stop();
        AudioSource.PlayClipAtPoint(fishEscapedAudio, Camera.main.transform.position);
    }
}
