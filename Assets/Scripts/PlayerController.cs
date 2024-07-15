using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rigid;
    private Camera mainCamera;
    
    public Vector3 orientation;
    public float verticalInput;
    public float horizontalInput;
    
    public LayerMask ground;
    private Vector3 originalScale;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.maxAngularVelocity = Player.instance.maxAngularVelocity;
        rigid.mass = Player.instance.mass;
        GameObject cameraObject = GameObject.Find("Camera");
        mainCamera = cameraObject.GetComponent<Camera>();
    }
    void Update()
    {
        Player.instance.grounded = Physics.Raycast(transform.position, Vector3.down, 0.55f, ground);
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        var transform1 = mainCamera.transform;
        var forward = transform1.forward;
        var right = transform1.right;
        var up = transform1.up;
        orientation = (right * verticalInput + -forward * horizontalInput).normalized;
        rigid.maxAngularVelocity = Player.instance.maxAngularVelocity;

        if (horizontalInput > 0)
        {
            // rigid.AddForce(right * Player.instance.power);
            rigid.AddTorque(orientation * (Player.instance.power * 0.5f), ForceMode.Impulse);
        }

        if (horizontalInput < 0)
        {
            // rigid.AddForce(-right * Player.instance.power);
            rigid.AddTorque(orientation * (Player.instance.power * 0.2f), ForceMode.Impulse);
        }

        if (verticalInput > 0)
        {
            // rigid.AddForce(forward * Player.instance.power);
            rigid.AddTorque(orientation * (Player.instance.power * 0.2f), ForceMode.Impulse);
        }

        if (verticalInput < 0)
        {
            // rigid.AddForce(-forward * Player.instance.power);
            rigid.AddTorque(orientation * (Player.instance.power * 0.25f), ForceMode.Impulse);
        }
        
        if (Input.GetKey(KeyCode.Space) && Player.instance.grounded)
        {
            if (Player.instance.fuel > 5f)
            {
                rigid.AddForce(up * 75f, ForceMode.Impulse);
                Player.instance.fuel -= 5f;
            }
            else
            {
                rigid.AddForce(up * 25f, ForceMode.Impulse);
            }
        }
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Player.instance.fuel > 0.01f)
            {
                rigid.maxAngularVelocity = Player.instance.maxAngularVelocity * 2f;
                rigid.AddForce(forward * (Player.instance.power * 2f));
                rigid.AddTorque(orientation * (Player.instance.power * 3f), ForceMode.Impulse);
                Player.instance.fuel -= 1f * Time.deltaTime;
            }
            else
            {
                rigid.maxAngularVelocity = Player.instance.maxAngularVelocity;
                rigid.angularDrag = Player.instance.angularDrag;
                rigid.drag = Player.instance.drag;
            }
        }
    }
}