using UnityEngine;

public class SlowRotation : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 10, 0); // Rotation speed in degrees per second

    void Update()
    {
        // Apply rotation based on the rotation speed and the time elapsed since the last frame
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}