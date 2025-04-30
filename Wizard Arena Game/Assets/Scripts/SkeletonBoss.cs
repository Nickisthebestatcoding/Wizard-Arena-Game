using System.Collections;
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
    public float spikeSpawnInterval = 0.5f;
    public float spikeAttackCooldown = 5f;

    private float lastSpikeAttackTime = -999f;

    [Header("Player Tracking Settings")]
    public GameObject spikeWarningPrefab;
    public float warningDelay = 1f;

    [Header("Enrage Settings")]
    public float enragedMoveSpeed = 3.5f;
    public float enragedDamage = 2f;
    public Color enragedColor = Color.red;
    private bool isEnraged = false;
    private Health health;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Wizard").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>(); // <-- Get Health component
        spriteRenderer = GetComponent<SpriteRenderer>(); // <-- Get SpriteRenderer
    }

    void Update()
    {
        if (player == null || health == null) return;

        CheckEnrage(); // <-- Check for enrage

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
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    void CheckEnrage()
    {
        if (!isEnraged && health != null && health.GetHealthPercent() <= 0.5f)
        {
            isEnraged = true;
            moveSpeed = enragedMoveSpeed;
            damage = enragedDamage;

            if (spriteRenderer != null)
                spriteRenderer.color = enragedColor;

            Debug.Log("Skeleton Boss is enraged!");
        }
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;
        rb.velocity = Vector2.zero;

        if (animator != null && player != null)
        {
            float xDifference = player.position.x - transform.position.x;
            animator.SetTrigger(xDifference > 0f ? "RightPunch" : "LeftPunch");
        }

        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }

    IEnumerator SpikeTrailRoutine()
    {
        isAttacking = true;
        rb.velocity = Vector2.zero;

        if (animator != null)
            animator.SetTrigger("SummonSpikes");

        lastSpikeAttackTime = Time.time;

        yield return new WaitForSeconds(1.2f);

        StartCoroutine(PerformSpikeAttack());

        yield return new WaitForSeconds(spikeTrailDuration + 1f);
        isAttacking = false;
    }

    IEnumerator PerformSpikeAttack()
    {
        isAttacking = true;
        rb.velocity = Vector2.zero;

        if (animator != null)
            animator.SetTrigger("SpikeAttack");

        float timePassed = 0f;

        while (timePassed < spikeTrailDuration)
        {
            Vector3 playerPosition = player.position;

            if (spikeWarningPrefab != null)
            {
                GameObject warning = Instantiate(spikeWarningPrefab, playerPosition, Quaternion.identity);
                Destroy(warning, warningDelay);
            }

            yield return new WaitForSeconds(spikeSpawnInterval);

            GameObject spike = Instantiate(spikePrefab, playerPosition, Quaternion.identity);
            Destroy(spike, spikeLifetime);

            timePassed += spikeSpawnInterval;
        }

        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }

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
        float timer = 0f;
        while (timer < knockbackDuration)
        {
            targetRb.velocity = direction * knockbackForce;
            timer += Time.deltaTime;
            yield return null;
        }
        targetRb.velocity = Vector2.zero;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
