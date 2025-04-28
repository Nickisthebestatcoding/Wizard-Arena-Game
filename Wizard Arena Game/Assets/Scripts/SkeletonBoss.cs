
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

        if (distance > stopDistance)
        {
            isAttacking = false;
            MoveTowardsPlayer();
        }
        else
        {
            if (!isAttacking)
            {
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
        rb.velocity = Vector2.zero; // Stop moving to punch

        // Handle attack animation based on player position
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

        yield return new WaitForSeconds(0.3f); // How long the punch is active

        DeactivateHitbox(); // Turn OFF punch collider

        // Wait for cooldown before allowing the next attack
        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;

        // Start a new attack if the player is still in range
        if (Vector2.Distance(transform.position, player.position) <= stopDistance)
        {
            StartCoroutine(AttackRoutine());
        }
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
                if (wizardHealth != null)
                {
                    wizardHealth.TakeDamage(damage);
                }
            }
        }
    }
}