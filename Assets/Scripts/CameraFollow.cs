using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    public float x_adjust = 0;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            target.transform.position.x+ x_adjust,
            target.transform.position.y,
            transform.position.z
        );
    }
}
