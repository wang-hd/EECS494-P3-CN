using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Direction
{
    up,
    down,
    left,
    right
}

public class LevelSelectPoint : MonoBehaviour
{
    [Header("Options")]
    public bool is_automatic;
    public bool hide_icon;
    public bool is_locked;
    public int levelIdx = -1;

    [Header("Neighbours")]
    public LevelSelectPoint up_point;
    public LevelSelectPoint down_point;
    public LevelSelectPoint left_point;
    public LevelSelectPoint right_point;

    public LevelSelectPoint[] AttachedAutoPoints;
    public GameObject StopSignForThisLevel;

    private Dictionary<Direction, LevelSelectPoint> point_directions;

    // Start is called before the first frame update
    void Start()
    {
        point_directions = new Dictionary<Direction, LevelSelectPoint>
        {
            {Direction.up, up_point},
            {Direction.down, down_point},
            {Direction.left, left_point },
            {Direction.right, right_point }
        };

        if (hide_icon)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public LevelSelectPoint GetPointInDirection(Direction direction)
    {
        switch(direction)
        {
            case Direction.up:
                return up_point;
            case Direction.down:
                return down_point;
            case Direction.left:
                return left_point;
            case Direction.right:
                return right_point;
            default:
                throw new ArgumentOutOfRangeException("direction", direction, null);
        }
    }
    public LevelSelectPoint GetValidFirstNextPoint(LevelSelectPoint point)
    {
        return point_directions.FirstOrDefault(x => x.Value != null && x.Value != point && x.Value.is_locked != true).Value;
    }

    protected void DrawLine(LevelSelectPoint point)
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, point.transform.position);
    }
    private void OnDrawGizmos()
    {
        if (up_point != null) DrawLine(up_point);
        if (down_point != null) DrawLine(down_point);
        if (left_point != null) DrawLine(left_point);
        if (right_point != null) DrawLine(right_point);

    }
}
