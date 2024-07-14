using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float jumpForce = 5f;
    public float minJumpInterval = 1f;
    public float maxJumpInterval = 3f;
    public float detectionRadius = 1f;
    public float aggroDetectionRadius = 15f;
    public float flingDistance = 10f;
    public float flingForce = 20f;
    public float explosionRadius = 5f;
    public float explosionForce = 10f;
    public string playerTag = "Player";
    public LayerMask damageableLayer;
    public float explosionOffset = 2f;
    public float flingCooldown = 3f;

    private Rigidbody rb;
    private float nextJumpTime;
    private float nextFlingTime;

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

        CheckPlayerAggro();
        CheckPlayerProximity();
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

    void CheckPlayerProximity()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(playerTag) || collider.CompareTag("Ally"))
            {
                Explode();
                break;
            }
        }
    }
    
    void CheckPlayerAggro()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, aggroDetectionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(playerTag) || collider.CompareTag("Ally"))
            {
                float distanceToPlayer = Vector3.Distance(transform.position, collider.transform.position);
                if (distanceToPlayer <= flingDistance && Time.time >= nextFlingTime)
                {
                    FlingTowardsPlayer(collider.transform.position);
                    nextFlingTime = Time.time + flingCooldown;
                }
                else
                {
                    FacePlayer(collider.transform.position);
                }
                break;
            }
        }
    }

    void FlingTowardsPlayer(Vector3 playerPosition)
    {
        Vector3 direction = (playerPosition - transform.position).normalized;
        rb.AddForce(new Vector3(direction.x, 1.5f, direction.z) * flingForce, ForceMode.Impulse);
        Debug.Log("Flinging towards player");
    }

    void Explode()
    {
        Vector3 explosionPosition = transform.position - new Vector3(0, explosionOffset, 0);
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius, damageableLayer);
        foreach (Collider hit in colliders)
        {
            Rigidbody hitRb = hit.attachedRigidbody;
            if (hitRb != null)
            {
                WaitForSeconds wait = new WaitForSeconds(0.4f);
                hitRb.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
                Debug.Log($"Applying explosion force to {hitRb.gameObject.name}");

                // Example: Apply damage if the object has a health component
                // Health health = hitRb.GetComponent<Health>();
                // if (health != null)
                // {
                //     health.TakeDamage(10); // Adjust the damage amount as needed
                //     Debug.Log($"{hitRb.gameObject.name} took damage");
                // }
            }
        }

        Destroy(gameObject);
    }

    void FacePlayer(Vector3 playerPosition)
    {
        Vector3 direction = (playerPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position - new Vector3(0, explosionOffset, 0), explosionRadius);
    }
}
