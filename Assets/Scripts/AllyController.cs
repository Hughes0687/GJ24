using UnityEngine;

public class AllyController : MonoBehaviour
{
    public Transform player;
    public float minDistance = 3f;
    public float maxDistance = 10f;
    public float grassPursuitRange = 10f;
    public float enemyPursuitRange = 15f;
    public float moveSpeed = 50f;

    private Rigidbody rb;
    private Transform targetGrass;
    private Transform targetEnemy;

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
        CheckForEnemy();
        
        if (targetEnemy != null)
        {
            PursueEnemy();
        }
        else
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

    void CheckForEnemy()
    {
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        targetEnemy = null;
        float closestDistance = enemyPursuitRange;
        foreach (GameObject enemy in enemyObjects)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy <= enemyPursuitRange && distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                targetEnemy = enemy.transform;
            }
        }
    }

    void PursueGrass()
    {
        if (targetGrass != null)
        {
            Vector3 direction = (targetGrass.position - transform.position).normalized;
            rb.AddForce(direction * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void PursueEnemy()
    {
        if (targetEnemy != null)
        {
            Vector3 direction = (targetEnemy.position - transform.position).normalized;
            rb.AddForce(direction * moveSpeed * Time.fixedDeltaTime);
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

        rb.AddForce(direction * moveSpeed * Time.fixedDeltaTime);
    }
}
