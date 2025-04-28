
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
                MoveTowardsPlayer();
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
        rb.velocity = Vector2.zero; // Stop moving during attack

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

        yield return new WaitForSeconds(0.3f); // Wait for punch to hit

        // No need to manually deactivate anything - punch just happens invisibly

        yield return new WaitForSeconds(1f); // Wait after punching

        isAttacking = false;
    }

    // Called by Animation Events
    public void RightPunchHit()
    {
        TryHitPlayer();
    }

    public void LeftPunchHit()
    {
        TryHitPlayer();
    }

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
                {
                    wizardHealth.TakeDamage(damage);
                }

                if (playerRb != null)
                {
                    Vector2 knockbackDirection = (hit.transform.position - transform.position).normalized;
                    StartCoroutine(ApplyKnockback(playerRb, knockbackDirection));
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

    // (Optional) Draw attack range in Scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}