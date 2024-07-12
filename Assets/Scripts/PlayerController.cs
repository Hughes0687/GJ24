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
        Player.instance.grounded = Physics.Raycast(transform.position, Vector3.down, 1f, ground);
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
            rigid.AddTorque(orientation * (Player.instance.power * 0.25f), ForceMode.Impulse);
        }

        if (horizontalInput < 0)
        {
            // rigid.AddForce(-right * Player.instance.power);
            rigid.AddTorque(orientation * (Player.instance.power * 0.25f), ForceMode.Impulse);
        }

        if (verticalInput > 0)
        {
            // rigid.AddForce(forward * Player.instance.power);
            rigid.AddTorque(orientation * (Player.instance.power * 0.25f), ForceMode.Impulse);
        }

        if (verticalInput < 0)
        {
            // rigid.AddForce(-forward * Player.instance.power);
            rigid.AddTorque(orientation * (Player.instance.power * 0.5f), ForceMode.Impulse);
        }
        
        if (Input.GetKey(KeyCode.Space) && Player.instance.grounded)
        {
            rigid.AddForce(up * 1f, ForceMode.Impulse);
        }
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Player.instance.boost > 0f)
            {
                rigid.maxAngularVelocity = Player.instance.maxAngularVelocity * 2f;
                rigid.AddForce(forward * (Player.instance.power * 2f));
                rigid.AddTorque(orientation * (Player.instance.power * 3f), ForceMode.Impulse);
                Player.instance.boost -= 2f * Time.deltaTime;
            }
            else
            {
                rigid.maxAngularVelocity = Player.instance.maxAngularVelocity;
                rigid.angularDrag = Player.instance.angularDrag;
                rigid.drag = Player.instance.drag;
            }
        }
        
        // if (Player.instance.grounded && Player.instance.boost < 10f)
        // {
            // Player.instance.boost += 1f * Time.deltaTime;
        // }
    }
}