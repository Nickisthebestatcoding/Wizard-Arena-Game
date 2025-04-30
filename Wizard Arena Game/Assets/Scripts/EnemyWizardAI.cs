using UnityEngine;

public class EnemyWizardAI : MonoBehaviour
{
    public Transform player; // The player's transform
    public float aggroRange = 10f; // Range at which the wizard starts moving
    public float shootRange = 5f; // Range at which the wizard starts moving randomly
    public float followDistance = 3f; // Distance to maintain from the player
    public float moveSpeed = 3f; // Speed at which the wizard moves
    public GameObject projectilePrefab; // The prefab the wizard will shoot
    public float shootCooldown = 2f; // Cooldown time between shots

    private float lastShootTime;
    private bool isAggroed = false;
    private bool isInShootRange = false;

    void Update()
    {
        // Check distance to player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= aggroRange)
        {
            isAggroed = true;
        }
        else
        {
            isAggroed = false;
        }

        if (isAggroed)
        {
            // Move towards the player, but keep distance
            Vector2 direction = (player.position - transform.position).normalized;
            if (distanceToPlayer > followDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            }

            // Rotate to face the player
            Vector2 directionToPlayer = player.position - transform.position;
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            if (distanceToPlayer <= shootRange && Time.time - lastShootTime > shootCooldown)
            {
                ShootAtPlayer();
                lastShootTime = Time.time;
            }

            // If in shooting range, move randomly
            if (distanceToPlayer <= shootRange)
            {
                MoveRandomly();
            }
        }
    }

    void ShootAtPlayer()
    {
        // Instantiate the projectile and shoot it towards the player
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Vector2 direction = (player.position - transform.position).normalized;
        projectile.GetComponent<Rigidbody2D>().velocity = direction * 5f; // Adjust speed as necessary
    }

    void MoveRandomly()
    {
        // Randomly move in a small area
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        Vector2 randomDirection = new Vector2(randomX, randomY).normalized;
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + randomDirection, moveSpeed * Time.deltaTime);
    }
}
