using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform ball;
    public Transform topPiece;
    public float distance = 7.5f;
    public float sensitivityX = 10f;
    public float sensitivityY = 10f;
    public float minY = -60f;
    public float maxY = 60f;

    private float rotationY = 0f;
    private float rotationX = 0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        rotationY = angles.y;
        rotationX = angles.x;
    }

    void LateUpdate()
    {
        if (ball != null)
        {
            if (Input.GetMouseButton(1))
            {
                rotationY += Input.GetAxis("Mouse X") * sensitivityX;
                rotationX -= Input.GetAxis("Mouse Y") * sensitivityY;
                rotationX = Mathf.Clamp(rotationX, minY, maxY);
                
                if (topPiece != null)
                {
                    topPiece.rotation = Quaternion.Euler(0, rotationY, 0);
                }
            }

            Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
            Vector3 position = ball.position - (rotation * Vector3.forward * distance + new Vector3(0, -1, 0));

            transform.rotation = rotation;
            transform.position = position;

            if (Input.GetMouseButtonUp(1))
            {
                ball.rotation = Quaternion.Euler(0, rotationY, 0);
            }
        }
    }
}
