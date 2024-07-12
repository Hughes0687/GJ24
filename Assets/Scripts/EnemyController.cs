using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float jumpForce = 5f;
    public float minJumpInterval = 1f;
    public float maxJumpInterval = 3f;

    private Rigidbody rb;
    private float nextJumpTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ScheduleNextJump();
    }

    void Update()
    {
        if (Time.time >= nextJumpTime)
        {
            Jump();
            ScheduleNextJump();
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        Debug.Log("Jumping");
    }

    void ScheduleNextJump()
    {
        nextJumpTime = Time.time + Random.Range(minJumpInterval, maxJumpInterval);
    }
}