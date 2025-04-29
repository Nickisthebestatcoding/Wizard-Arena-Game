
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
    public float spikeAttackCooldown = 5f;

    private float lastSpikeAttackTime = -Mathf.Infinity;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Wizard").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (!isAttacking)
        {
            if (distance > stopDistance)
            {
                if (Time.time >= lastSpikeAttackTime + spikeAttackCooldown)
                {
                    StartCoroutine(SpikeAttackRoutine());
                }
                else
                {
                    MoveTowardsPlayer();
                }
            }
            else
            {
                rb.velocity = Vector2.zero;
                StartCoroutine(AttackRoutine());
            }
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
                animator.SetTrigger("RightPunch");
            else
                animator.SetTrigger("LeftPunch");
        }

        yield return new WaitForSeconds(0.3f); // Wait for punch hit
        yield return new WaitForSeconds(1f);   // Recovery time
        isAttacking = false;
    }

    IEnumerator SpikeAttackRoutine()
    {
        isAttacking = true;
        rb.velocity = Vector2.zero;

        if (animator != null)
        {
            animator.SetTrigger("SummonSpikes");
        }

        lastSpikeAttackTime = Time.time;

        yield return new WaitForSeconds(1.5f); // Adjust based on your animation
        isAttacking = false;
    }

    public void SummonSpikes()
    {
        if (player != null && spikePrefab != null)
        {
            Vector3 spawnPosition = player.position;
            GameObject spike = Instantiate(spikePrefab, spawnPosition, Quaternion.identity);
            Destroy(spike, spikeLifetime);
        }
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