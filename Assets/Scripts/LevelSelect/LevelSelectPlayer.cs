using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectPlayer : MonoBehaviour
{
    public float speed = 3f;
    public AudioClip walkAudio;
    public bool is_moving { get; private set; }

    public LevelSelectPoint current_point { get; private set; }

    private LevelSelectPoint target_point;

    private AudioSource Audio;
    private void Awake()
    {
        Audio = GetComponent<AudioSource>();
    }


    // Start is called before the first frame update
    public void Initialize(LevelSelectPoint point)
    {
        SetCurrentPoint(point);
    }

    // Update is called once per frame
    void Update()
    {
        if(target_point == null)
        {
            return;
        }

        if(Vector2.Distance(transform.position, target_point.transform.position) > 0.02f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target_point.transform.position, Time.deltaTime * speed);
        }
        else
        {
            if (target_point.is_automatic)
            {
                EventBus.Publish<leave_level_event>(new leave_level_event());
                MoveToPoint(target_point.GetValidFirstNextPoint(current_point)); //If automatic, it will only have one valid path.
            }
            else
            {
                SetCurrentPoint(target_point);
                if(current_point != null)
                {
                    if (current_point.levelIdx!=-1)
                    {
                        EventBus.Publish<reach_level_event>(new reach_level_event(current_point.levelIdx));
                    }
                    else
                    {
                        EventBus.Publish<leave_level_event>(new leave_level_event());
                    }
                }
            }
        }
    }

    public void TrySetDirection(Direction direction)
    {
        LevelSelectPoint point = current_point.GetPointInDirection(direction);

        if (point == null || point.is_locked) return;

        Audio.clip = walkAudio;
        Audio.loop = true;
        MoveToPoint(point);
    }

    public void SetCurrentPoint( LevelSelectPoint point)
    {
        current_point = point;
        target_point = null;
        transform.position = point.transform.position;
        is_moving = false;
        Audio.Stop();

    }

    private void MoveToPoint(LevelSelectPoint point)
    {
        target_point = point;
        is_moving = true;
        Audio.Play();
    }
}
