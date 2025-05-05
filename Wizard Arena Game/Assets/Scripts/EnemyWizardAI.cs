using UnityEngine;

public class EnemyWizardAI : MonoBehaviour
{
    public float detectionRadius = 10f;     // Detection range
    public float attackRange = 5f;          // Range to attack and move randomly
    public float moveSpeed = 3f;            // Movement speed
    public GameObject projectilePrefab;     // Projectile to shoot
    public Transform shootPoint;            // Where projectile spawns
    public float shootDelay = 1f;           // Delay between shots

    private Transform player;               // Reference to player
    private float shootCooldown;            // Internal timer for shooting
    private Vector2 randomMoveTarget;       // Random position to move to

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Wizard").transform;
        randomMoveTarget = transform.position; // Start at current pos
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            RotateTowardsPlayer();

            if (distanceToPlayer <= attackRange)
            {
                MoveRandomly();
                AttackPlayer();
            }
            else
            {
                MoveTowardsPlayer();
            }
        }
        else
        {
            // Optional: idle behavior when player is out of range
        }
    }

    void RotateTowardsPlayer()
    {
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    void MoveRandomly()
    {
        // Move toward random target
        transform.position = Vector2.MoveTowards(transform.position, randomMoveTarget, moveSpeed * Time.deltaTime);

        // Pick a new target if close enough to current
        if (Vector2.Distance(transform.position, randomMoveTarget) < 0.2f)
        {
            PickNewRandomTarget();
        }
    }

    void PickNewRandomTarget()
    {
        Vector2 offset = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
        randomMoveTarget = (Vector2)transform.position + offset;
    }

    void AttackPlayer()
    {
        if (shootCooldown <= 0f)
        {
            ShootProjectile();
            shootCooldown = shootDelay;
        }
        else
        {
            shootCooldown -= Time.deltaTime;
        }
    }

    void ShootProjectile()
    {
        Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
    }

}


