using UnityEngine;

public class EnemyWizardAI : MonoBehaviour
{
    public float detectionRadius = 10f;  // How close the player must be to be detected
    public float attackRange = 5f;       // Range within which the enemy stays but attacks
    public float moveSpeed = 3f;         // Movement speed of the wizard
    public GameObject projectilePrefab;  // The projectile prefab to shoot
    public Transform shootPoint;         // Where the projectile will shoot from
    public float shootDelay = 1f;        // Time between shots
    private float shootCooldown;

    private Transform player;            // The player reference
    private bool isPlayerInRange = false; // Whether the player is within range
    private Vector2 randomMoveTarget;     // Target position for random movement
    private float randomMoveCooldown = 2f; // Time interval before the enemy chooses a new random move target
    private float randomMoveTimer;        // Timer to keep track of when to change target

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Wizard").transform;
        randomMoveTarget = transform.position;  // Start with the enemy's current position
        randomMoveTimer = randomMoveCooldown;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) <= detectionRadius)
        {
            isPlayerInRange = true;
            RotateTowardsPlayer();
            if (Vector2.Distance(transform.position, player.position) <= attackRange)
            {
                RandomMoveInRange();
                AttackPlayer();
            }
            else
            {
                MoveTowardsPlayer();
            }
        }
        else
        {
            isPlayerInRange = false;
            StopMoving();
        }

        // Handle random movement cooldown
        if (randomMoveTimer > 0)
        {
            randomMoveTimer -= Time.deltaTime;
        }
        else
        {
            randomMoveTarget = new Vector2(transform.position.x + Random.Range(-3f, 3f), transform.position.y + Random.Range(-3f, 3f));
            randomMoveTimer = randomMoveCooldown;  // Reset the timer
        }
    }

    void RotateTowardsPlayer()
    {
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void MoveTowardsPlayer()
    {
        if (Vector2.Distance(transform.position, player.position) > attackRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    void RandomMoveInRange()
    {
        // Move randomly within the attack range
        transform.position = Vector2.MoveTowards(transform.position, randomMoveTarget, moveSpeed * Time.deltaTime);
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
        Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
    }

    void StopMoving()
    {
        // Optional: Stop movement when the player is not within range
        // You can add more idle animations or logic here
    }
}
