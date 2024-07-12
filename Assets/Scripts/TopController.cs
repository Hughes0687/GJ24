using UnityEngine;

public class TopController : MonoBehaviour
{
    public Transform ball;
    public Vector3 offset = new Vector3(0, 0.25f, 0);

    void Update()
    {
        if (ball != null)
        {
            transform.position = ball.position + offset;
        }
    }
}
