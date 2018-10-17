using UnityEngine;

public class LightMover : MonoBehaviour
{
    private float minX;
    private float maxX;

    void Start()
    {
        minX = transform.position.x;
        maxX = minX + 3f;
    }

    void Update()
    {
        transform.position = new Vector3(
            Mathf.PingPong(Time.time * 1.5f, maxX - minX) + minX,
            transform.position.y,
            transform.position.z
        );
    }
}
