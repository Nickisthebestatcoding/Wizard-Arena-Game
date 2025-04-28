
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkeletonBoss : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float damage = 1f;
    public float attackRange = 1.5f;
    public float stopDistance = 1.2f;
    public GameObject attackHitbox; // <-- ADD THIS!
    public float attackCooldown = 1f; // Added a cooldown variable for easier adjustment

    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isAttacking = false;
    public float knockbackForce = 5f;  // How hard the knockback is
    public float knockbackDuration = 0.2f; // How long the player is pushed

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Wizard").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (attackHitbox != null)
            attackHitbox.SetActive(false); // Make sure it's off at start
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
                rb.velocity = Vector2.zero; // Stop moving when in attack range
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

        // Choose the correct punch animation
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

        ActivateHitbox(); // Turn ON punch collider

        yield return new WaitForSeconds(0.3f); // Hitbox active time

        DeactivateHitbox(); // Turn OFF punch collider

        yield return new WaitForSeconds(1f); // <-- FULL 1 second wait AFTER attacking

        isAttacking = false;
    }

    void ActivateHitbox()
    {
        if (attackHitbox != null)
            attackHitbox.SetActive(true);
    }

    void DeactivateHitbox()
    {
        if (attackHitbox != null)
            attackHitbox.SetActive(false);
    }

    // Call these methods from animation events
    void RightPunchHit()
    {
        TryHitPlayer();
    }

    void LeftPunchHit()
    {
        TryHitPlayer();
    }

    // This method handles the damage logic when a punch hits
    void TryHitPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Wizard"))
            {
                Health wizardHealth = hit.GetComponent<Health>();
                Rigidbody2D playerRb = hit.GetComponent<Rigidbody2D>(); // Get the player's Rigidbody2D

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
    IEnumerator ApplyKnockback(Rigidbody2D playerRb, Vector2 direction)
    {
        float timer = 0f;

        while (timer < knockbackDuration)
        {
            playerRb.velocity = direction * knockbackForce;
            timer += Time.deltaTime;
            yield return null;
        }

        playerRb.velocity = Vector2.zero; // Stop player movement after knockback
    }
}