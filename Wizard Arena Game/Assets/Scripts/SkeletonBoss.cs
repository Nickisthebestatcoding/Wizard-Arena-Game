using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBoss : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float damage = 1f;
    public float attackRange = 1.5f;
    public float knockbackForce = 5f;
    public float knockbackDuration = 0.2f;
    public float stopDistance = 1.2f;

    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isAttacking = false;

    [Header("Spike Attack Settings")]
    public GameObject spikePrefab;
    public float spikeLifetime = 3f;
    public float spikeTrailDuration = 2f;
    public float spikeSpawnInterval = 0.5f; // Adjusted spawn interval for slower spikes
    public float spikeAttackCooldown = 5f;

    private float lastSpikeAttackTime = -999f;

    // Public variables to track player position
    [Header("Player Tracking Settings")]
    public Vector3 previousPlayerPosition; // Expose this to see in the Inspector
    public float playerPositionTrackingInterval = 1f; // Track player's position every second (can change in Inspector)

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Wizard").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        previousPlayerPosition = player.position; // Initialize the position tracker
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (!isAttacking)
        {
            if (distance <= stopDistance)
            {
                rb.velocity = Vector2.zero;
                StartCoroutine(AttackRoutine());
            }
            else if (Time.time - lastSpikeAttackTime >= spikeAttackCooldown)
            {
                rb.velocity = Vector2.zero;
                StartCoroutine(SpikeTrailRoutine());
            }
            else
            {
                MoveTowardsPlayer();
            }
        }

        // Track player's position every interval
        if (Time.time % playerPositionTrackingInterval < 0.1f)
        {
            previousPlayerPosition = player.position;
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;
        rb.velocity = Vector2.zero;

        if (animator != null && player != null)
        {
            float xDifference = player.position.x - transform.position.x;

            if (xDifference > 0f)
            {
                animator.SetTrigger("RightPunch");
            }
            else
            {
                animator.SetTrigger("LeftPunch");
            }
        }

        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }

    IEnumerator SpikeTrailRoutine()
    {
        isAttacking = true;
        rb.velocity = Vector2.zero;

        if (animator != null)
        {
            animator.SetTrigger("SummonSpikes");
        }

        lastSpikeAttackTime = Time.time;

        // Add a short delay before spikes start spawning
        yield return new WaitForSeconds(0.5f); // Initial delay before spikes start

        // Call the PerformSpikeAttack coroutine
        StartCoroutine(PerformSpikeAttack());

        yield return new WaitForSeconds(spikeTrailDuration + 1f); // Wait for attack to finish
        isAttacking = false;
    }

    // Perform the Spike Attack
    IEnumerator PerformSpikeAttack()
    {
        float spawnDuration = 2f; // Duration of the attack
        float timePassed = 0f;
        float spawnInterval = 0.5f; // Delay between each spike spawn
        float initialDelay = 0.5f;  // Delay before first spike spawn to make it dodgeable

        // Wait for the initial delay before starting the spike spawn
        yield return new WaitForSeconds(initialDelay);

        while (timePassed < spawnDuration)
        {
            if (player != null)
            {
                // Spawn the spike at the position where the wizard was 1 second ago
                Vector3 spawnPosition = new Vector3(previousPlayerPosition.x, previousPlayerPosition.y - 1f, previousPlayerPosition.z);

                // Instantiate the spike at the calculated position
                GameObject spike = Instantiate(spikePrefab, spawnPosition, Quaternion.identity);
                Destroy(spike, spikeLifetime); // Destroy after the lifetime
            }

            yield return new WaitForSeconds(spawnInterval); // Delay between spikes
            timePassed += spawnInterval;
        }

        yield return new WaitForSeconds(1f); // Post-attack cooldown
        isAttacking = false;
    }

    // Animation Event Methods
    public void RightPunchHit() => TryHitPlayer();
    public void LeftPunchHit() => TryHitPlayer();

    void TryHitPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Wizard"))
            {
                Health wizardHealth = hit.GetComponent<Health>();
                Rigidbody2D playerRb = hit.GetComponent<Rigidbody2D>();

                if (wizardHealth != null)
                    wizardHealth.TakeDamage(damage);

                if (playerRb != null)
                {
                    Vector2 knockbackDir = (hit.transform.position - transform.position).normalized;
                    StartCoroutine(ApplyKnockback(playerRb, knockbackDir));
                }
            }
        }
    }

    IEnumerator ApplyKnockback(Rigidbody2D targetRb, Vector2 direction)
    {
        if (targetRb != null)
        {
            float timer = 0f;
            while (timer < knockbackDuration)
            {
                targetRb.velocity = direction * knockbackForce;
                timer += Time.deltaTime;
                yield return null;
            }
            targetRb.velocity = Vector2.zero;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

