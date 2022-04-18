using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameFish : MonoBehaviour
{

    [Header("Fish status")]
    public int index = 0;

    public float fish_speed = 1f;
    public float stop_time = 0.1f;
    // Max/Min y pos relative to the initial position
    public float y_max = -1f;
    public float y_min = -6f;
    Vector2 desired_pos;
    private float stop_timer = 0.0f;

    [Header("Animation")]
    Animator animator;
    private bool is_up = true;
    private float currVel;

    FishingMiniGameControler controler;
    SpriteRenderer fish_icon_in_game;
    SpriteRenderer danger_icon;
    public bool in_region_indicator;
    public int fishID { get; private set; } = 0;

    private bool isActive = true;
    // Start is called before the first frame update
    void Start()
    {
        controler = gameObject.GetComponentInParent<FishingMiniGameControler>();
        fishID = controler.fishIDs[index];
        fish_icon_in_game = gameObject.GetComponent<SpriteRenderer>();
        danger_icon = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        desired_pos = new Vector2(transform.localPosition.x, Random.Range(y_min, y_max));
       
        if(fishID != -1) // not tutorial
        {
            fish_icon_in_game.color = Color.white;
            fish_icon_in_game.sprite = FishList.GetFishWithFishID(fishID).getSprite();

            // Make large size fish and small size look different but have collider with same size.
            if (FishList.GetFishWithFishID(fishID).fish_size == FishSize.Small)
            {
                gameObject.transform.localScale -= new Vector3(0.15f, 0.15f, 0.15f);
                gameObject.GetComponent<BoxCollider2D>().size += new Vector2(1f, 1f);
            }
            else if (FishList.GetFishWithFishID(fishID).fish_size == FishSize.Large)
            {
                gameObject.transform.localScale += new Vector3(0.15f, 0.15f, 0.15f);
                gameObject.GetComponent<BoxCollider2D>().size -= new Vector2(0.5f, 0.5f);
            }

            if (FishList.GetFishWithFishID(fishID).isFish)
            {
                fish_speed = FishList.GetFishWithFishID(fishID).mini_game_move_speed;
                stop_time = FishList.GetFishWithFishID(fishID).mini_game_stay_time;
            }
            else
            {
                fish_speed = 0;
                stop_time = 100f;
                gameObject.GetComponent<Animator>().enabled = false;
                transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
                transform.localPosition = new Vector2(transform.localPosition.x, y_min + 0.4f);
            }            
        }
        

        //animation
        animator = GetComponent<Animator>();
        animator.SetBool("is_up", is_up);
        StartCoroutine(CalcVelocity());
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector2.Lerp(transform.localPosition, desired_pos, Time.deltaTime * fish_speed);
        if (Vector2.Distance(transform.localPosition, desired_pos) <= 0.2f)
        {
            if(stop_timer <= stop_time)
            {
                stop_timer += Time.deltaTime;
            }
            else
            {
                stop_timer = 0.0f;
                float new_y_pos = Random.Range(y_min, y_max);
                desired_pos = new Vector2(transform.localPosition.x, new_y_pos);
                if(new_y_pos < transform.localPosition.y)
                {
                    is_up = false;
                }
                else
                {
                    is_up = true;
                }
                animator.SetBool("is_up", is_up);
            }
        }
        //animation
        animator.speed = currVel;
    }

    private void OnTiggerEnter2D(Collider2D collider)
    {
        if (isActive && collider.CompareTag("MiniGameInterval"))
        {
            in_region_indicator = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (isActive && collider.CompareTag("MiniGameInterval"))
        {
            in_region_indicator = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (isActive && collider.CompareTag("MiniGameInterval"))
        {
            in_region_indicator = false;
        }
    }

    public void DeactivateMiniGameFish()
    {
        in_region_indicator = false;
        fish_icon_in_game.enabled = false;
        danger_icon.enabled = false;
        isActive = false;
    }


    IEnumerator CalcVelocity()
    {
        while (Application.isPlaying)
        {
            // Position at frame start
            Vector3 prevPos = transform.position;
            // Wait till it the end of the frame
            yield return new WaitForEndOfFrame();
            // Calculate velocity: Velocity = DeltaPosition / DeltaTime
            currVel = Vector3.Magnitude((prevPos - transform.position) / Time.deltaTime);
        }
    }

}
