using UnityEngine;

public class AllyController : MonoBehaviour
{
    public Transform player;
    public float minDistance = 3f;
    public float maxDistance = 10f;
    public float grassPursuitRange = 10f;
    public float moveSpeed = 5f;

    private Rigidbody rb;
    private Transform targetGrass;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform;
        }

        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        CheckForGrass();

        if (targetGrass != null)
        {
            PursueGrass();
        }
        else
        {
            FollowPlayer();
        }
    }

    void CheckForGrass()
    {
        GameObject[] grassObjects = GameObject.FindGameObjectsWithTag("Grass");
        targetGrass = null;
        float closestDistance = grassPursuitRange;
        foreach (GameObject grass in grassObjects)
        {
            float distanceToGrass = Vector3.Distance(transform.position, grass.transform.position);
            if (distanceToGrass <= grassPursuitRange && distanceToGrass < closestDistance)
            {
                closestDistance = distanceToGrass;
                targetGrass = grass.transform;
            }
        }
    }

    void PursueGrass()
    {
        if (targetGrass != null)
        {
            Vector3 direction = (targetGrass.position - transform.position).normalized;
            rb.MovePosition(transform.position + direction * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void FollowPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        Vector3 direction;

        if (distanceToPlayer > maxDistance)
        {
            direction = (player.position - transform.position).normalized;
        }
        else if (distanceToPlayer < minDistance)
        {
            direction = (transform.position - player.position).normalized;
        }
        else
        {
            return;
        }

        rb.MovePosition(transform.position + direction * moveSpeed * Time.fixedDeltaTime);
    }
}
