using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    //public Button castRodButton;
    // add a new cast rod sprite
    public GameObject castRodSprite;
    public Button pullFishButton;
    public GameObject Rod;
    public Image catchFishTimerImg;
    public AudioClip castRodAudio;
    public GameObject hurtEffectImg;

    private bool isRodCasted = false;
    private bool isTimerFinished = true;

    private DateTime? clickPullFishButtonTime;
    private DateTime? circleTimerStartTime;
    private DateTime? circleTimerEndTime;

    private Vector2 rodOriginalPosition;
    private Coroutine rodCoroutine;
    private bool isPullRodButtonEnabled = false;
    private bool isDayFinished = false;


    // Fishing probability algorithm
    List<int> fish_pool;
    private int[] fishIDsForTheFiveFishInMiniGame;
    private int[] fishProbWeights = new int[] { 60, 20, 10, 10 }; // R, SR, SSR, item
    //private int[] fishProbWeights = new int[] { 10, 10, 10, 100 }; // R, SR, SSR, item
    private List<int> R_fish_in_pool = new List<int>();
    private List<int> SR_fish_in_pool = new List<int>();
    private List<int> SSR_fish_in_pool = new List<int>();
    private List<int> item_in_pool = new List<int>();
    private RandomWithProbWeight random_with_Probweight;



    Subscription<get_fish_event> getFish_event_subscription;
    Subscription<fish_escape_event> fishEscape_event_subscription;
    Subscription<fish_hooked_event> fishHook_event_subscription;
    Subscription<get_item_event> getItem_event_subscription;
    Subscription<refresh_the_day> refresh_day_subcscription;

    void Start()
    {
        //castRodButton.onClick.AddListener(handleCastClick);
        pullFishButton.onClick.AddListener(handlePullFishButtonClick);

        getFish_event_subscription = EventBus.Subscribe<get_fish_event>(handleFishCaught);
        fishEscape_event_subscription = EventBus.Subscribe<fish_escape_event>(handleFishEscape);
        fishHook_event_subscription = EventBus.Subscribe<fish_hooked_event>(handleFishHooked);
        getItem_event_subscription = EventBus.Subscribe<get_item_event>(handleGetItem);
        refresh_day_subcscription = EventBus.Subscribe<refresh_the_day>(refreshDay);

        catchFishTimerImg.gameObject.SetActive(false);

        rodOriginalPosition = Rod.transform.position;

        fishIDsForTheFiveFishInMiniGame = new int[5];


        fish_pool = StaticData.fishInEachLevel[StaticData.currentLevelIdx];
        for(int i = 0; i<fish_pool.Count;i++)
        {
            if (!FishList.GetFishWithFishID(fish_pool[i]).isFish)
            {
                item_in_pool.Add(fish_pool[i]);
            }
            else
            {
                switch (FishList.GetFishWithFishID(fish_pool[i]).fish_rarity)
                {
                    case FishRarity.R:
                        R_fish_in_pool.Add(fish_pool[i]);
                        break;
                    case FishRarity.SR:
                        SR_fish_in_pool.Add(fish_pool[i]);
                        break;
                    case FishRarity.SSR:
                        SSR_fish_in_pool.Add(fish_pool[i]);
                        break;
                    default:
                        Debug.Log("Wrong Rarity");
                        break;
                }
            }
        }
        random_with_Probweight = new RandomWithProbWeight(fishProbWeights);
    }

    private void Update()
    {

        // for turning screen to red when player gets attacked
        if (hurtEffectImg.GetComponent<SpriteRenderer>().color.a > 0)
        {
            var hurtColor = hurtEffectImg.GetComponent<SpriteRenderer>().color;
            hurtColor.a -= 0.05f;

            hurtEffectImg.GetComponent<SpriteRenderer>().color = hurtColor;

        }
    }

    void refreshDay(refresh_the_day e)
    {
        isDayFinished = true;
    }

    public void handleCastClick()
    {
        if(StaticData.has_open_panel)
        {
            return;
        }
        isDayFinished = false;
        //EventBus.Publish<ToastRequest>(new ToastRequest("Rod Casted...Waiting...", 1f, false));
        //castRodButton.gameObject.SetActive(false);
        castRodSprite.gameObject.SetActive(false);
        if (!isRodCasted)
        {
            StartCoroutine(AttemptCatchFish());

        }
    }
    void setRodCastStatus(string direction, bool IsRodCasted)
    {
        if (direction == "down")
        {
            Rod.transform.position = new Vector2(Rod.transform.position.x, Rod.transform.position.y - 0.25f);
            isRodCasted = IsRodCasted;
        }
        else
        {
            Rod.transform.position = rodOriginalPosition;
            isRodCasted = IsRodCasted;
            StartCoroutine(WaitToCastAgain());
        }
    }

    IEnumerator WaitToCastAgain()
    {
        bool isCastRodWaitTimeDone = false;
        while (!isCastRodWaitTimeDone)
        {
            yield return new WaitForSeconds(2);
            //castRodButton.gameObject.SetActive(true);
            castRodSprite.SetActive(true);
            isCastRodWaitTimeDone = true;
            yield return null;
        }
    }

    void handleFishCaught(get_fish_event e)
    {
        isTimerFinished = true; // for fish hook animation
        StopCoroutine(rodCoroutine);
        setRodCastStatus("up", false);
        Rod.SetActive(false);
        if (FishList.GetFishWithFishID(e.fishHookedID).getAttack() > 0)
        {
            getHurtEffect();
            EventBus.Publish<alter_health_event>(new alter_health_event(-1 * FishList.GetFishWithFishID(e.fishHookedID).getAttack()));
        }
        //EventBus.Publish<ToastRequest>(new ToastRequest("You caught a fish!", 1f, false));
    }
    void getHurtEffect()
    {
        var hurtColor = hurtEffectImg.GetComponent<SpriteRenderer>().color;
        hurtColor.a = 0.8f;

        hurtEffectImg.GetComponent<SpriteRenderer>().color = hurtColor;

    }
    void handleFishEscape(fish_escape_event e)
    {
        Rod.SetActive(false);
        isTimerFinished = true; // for fish hook animation
        StopCoroutine(rodCoroutine);
        EventBus.Publish<ToastRequest>(new ToastRequest("Fish Escaped", 1f, false));
        setRodCastStatus("up", false);
    }

    void handleFishHooked(fish_hooked_event e)
    {
        //EventBus.Publish<ToastRequest>(new ToastRequest("Fish hooked!! ", 1f, false));
    }

    void handleGetItem(get_item_event e)
    {
        StaticData.fishInEachLevel[StaticData.currentLevelIdx].Remove(e.itemID);
        fish_pool.Remove(e.itemID);
        item_in_pool.Remove(e.itemID);
        StaticData.special_item_unlock_status[e.itemID] = true;

        isTimerFinished = true; // for fish hook animation
        StopCoroutine(rodCoroutine);
        setRodCastStatus("up", false);
        Rod.SetActive(false);

        EventBus.Publish<ToastRequest>(new ToastRequest("New item get.", 1f, false));
    }

    IEnumerator AttemptCatchFish()
    {
        catchFishTimerImg.fillAmount = 1;
        isTimerFinished = false;
        bool isFishHooked = false;
        Rod.SetActive(true);
        while (true)
        {
            if (!isFishHooked)
            {
                // waiting for fish to get hooked
                AudioSource.PlayClipAtPoint(castRodAudio, Camera.main.transform.position);
                yield return new WaitForSeconds(UnityEngine.Random.Range(5, 10));

                // if day is finished, return and stop attempting to catch fish
                if (isDayFinished) yield break;

                // fish hooked
                isFishHooked = true;
                rodCoroutine = StartCoroutine(RodWaitEffect());
                List<int> item_hooked_in_this_round = new List<int>();
                EventBus.Publish<fish_hooked_event>(new fish_hooked_event("Fish hooked!!"));
                for (int i = 0; i < 5; i++)
                {
                    int fish_rarity = random_with_Probweight.PickIndex(); // 0 for R, 1 for SR, 2 for SSR, 3 for item;
                    switch (fish_rarity)
                    {
                        case 0:
                            if(R_fish_in_pool.Count == 0)
                            {
                                i--;
                                break;
                            }
                            fishIDsForTheFiveFishInMiniGame[i] = R_fish_in_pool[UnityEngine.Random.Range(0, R_fish_in_pool.Count)];
                            break;
                        case 1:
                            if (SR_fish_in_pool.Count == 0)
                            {
                                i--;
                                break;
                            }
                            fishIDsForTheFiveFishInMiniGame[i] = SR_fish_in_pool[UnityEngine.Random.Range(0, SR_fish_in_pool.Count)];
                            break;
                        case 2:
                            if (SSR_fish_in_pool.Count == 0)
                            {
                                i--;
                                break;
                            }
                            fishIDsForTheFiveFishInMiniGame[i] = SSR_fish_in_pool[UnityEngine.Random.Range(0, SSR_fish_in_pool.Count)];
                            break;
                        case 3:
                            if (item_in_pool.Count == 0)
                            {
                                i--;
                                break;
                            }
                            int temp = item_in_pool[UnityEngine.Random.Range(0, item_in_pool.Count)];
                            if (item_hooked_in_this_round.Contains(temp)) { // prevent an item exists multiple time in the game
                                i--;
                                break;
                            }
                            else
                            {
                                fishIDsForTheFiveFishInMiniGame[i] = temp;
                                item_hooked_in_this_round.Add(temp);
                            }
                            break;
                        default:
                            Debug.Log("Wrong fish rarity");
                            break;
                    }
                }
                

                // start pull fish timer
                catchFishTimerImg.gameObject.SetActive(true);
                pullFishButton.gameObject.SetActive(true);
                isPullRodButtonEnabled = true;
                yield return StartCoroutine(
                    FillTimer()
                );

                // pull fish timer done
                catchFishTimerImg.gameObject.SetActive(false);
                pullFishButton.gameObject.SetActive(false);
                isPullRodButtonEnabled = false;
                if (didPlayerPullFishWithinTimer())
                {

                    EventBus.Publish<pull_fish_event>(new pull_fish_event("start pulling fish!!"));
                    // instantiate the fishing mini game
                    System.Collections.Generic.List<int> tempList = fishIDsForTheFiveFishInMiniGame.ToList();
                    tempList.Sort((f1, f2) => {
                        int res = FishList.GetFishWithFishID(f1).fish_size - FishList.GetFishWithFishID(f2).fish_size;
                        if (res == 0)
                        {
                            return f2-f1;
                        }
                        return res; 
                    });
                    int tmp = 0;
                    foreach(var i in tempList)
                    {
                        fishIDsForTheFiveFishInMiniGame[tmp] = i;
                        tmp++;
                    }

                    GetComponent<PrefabInstantiater>().MiniGameInstantiate(gameObject.transform,fish_ids: fishIDsForTheFiveFishInMiniGame);
                    // GetComponent<PrefabInstantiater>().MiniGameTutorialInstantiate(gameObject.transform);
                    setRodCastStatus("down", true);
                }
                else
                {

                    EventBus.Publish<fish_escape_event>(new fish_escape_event("Fish Escaped"));
                }

                // resetting time for circularTimer to avoid potential bugs
                clickPullFishButtonTime = null;
                circleTimerStartTime = null;
                circleTimerEndTime = null;

            }
            yield return null;
        }
    }

    bool didPlayerPullFishWithinTimer()
    {
        return circleTimerStartTime <= clickPullFishButtonTime && clickPullFishButtonTime <= circleTimerEndTime;
    }

    IEnumerator FillTimer()
    {
        float waitTime = UnityEngine.Random.Range(2, 4);
        circleTimerStartTime = System.DateTime.Now;
        circleTimerEndTime = circleTimerStartTime?.AddSeconds(waitTime);

        while (catchFishTimerImg.fillAmount > 0 && !didPlayerPullFishWithinTimer())
        {
            catchFishTimerImg.fillAmount -= 1.0f / waitTime * Time.deltaTime;
            yield return null;
        }
    }


    IEnumerator RodWaitEffect()
    {
        while (!isTimerFinished)
        {
            yield return StartCoroutine(MoveRod(0, 3, 0.5f));
            yield return StartCoroutine(MoveRod(3, 0, 0.5f));
        }
    }



    IEnumerator MoveRod(float startz, float endz, float duration_sec)
    {
        float initial_time = Time.time;
        float progress = (Time.time - initial_time) / duration_sec;

        while (progress < 1.0f)
        {
            progress = (Time.time - initial_time) / duration_sec;
            Rod.transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, startz), Quaternion.Euler(0, 0, endz), progress);

            yield return null;
        }

    }

    void handlePullFishButtonClick()
    {
        clickPullFishButtonTime = System.DateTime.Now;
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe<get_fish_event>(getFish_event_subscription);
        EventBus.Unsubscribe<fish_escape_event>(fishEscape_event_subscription);
        EventBus.Unsubscribe<fish_hooked_event>(fishHook_event_subscription);
        EventBus.Unsubscribe<get_item_event>(getItem_event_subscription);
    }

    private void LateUpdate()
    {
        if (isPullRodButtonEnabled)
        {
            float signal = Mathf.Abs(Mathf.Sin(Time.time * 5));
            Color green = new Color32(162, 243, 97, 255);
            pullFishButton.GetComponent<Image>().color = Color.Lerp(green, Color.white, signal);
        }
    }
}


public class RandomWithProbWeight
{
    int[] pre;
    int total;
    System.Random ran = new System.Random();

    public RandomWithProbWeight(int[] w)
    {
        pre = new int[w.Length];
        pre[0] = w[0];
        for (int i = 1; i < w.Length; ++i)
        {
            pre[i] = pre[i - 1] + w[i];
        }
        total = w.Sum();
    }
    public int PickIndex()
    {
        int x = ran.Next(1, total + 1);
        return BinarySearch(x);
    }

    private int BinarySearch(int x)
    {
        int low = 0, high = pre.Length - 1;
        while (low < high)
        {
            int mid = (high - low) / 2 + low;
            if (pre[mid] < x)
            {
                low = mid + 1;
            }
            else
            {
                high = mid;
            }
        }
        return low;
    }
}
