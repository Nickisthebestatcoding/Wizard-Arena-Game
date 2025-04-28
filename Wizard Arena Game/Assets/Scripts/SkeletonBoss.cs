
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
        Debug.Log("Starting Attack Routine!");
        isAttacking = true;
        rb.velocity = Vector2.zero; // Stop moving to punch

        if (animator != null && player != null)
        {
            float xDifference = player.position.x - transform.position.x;

            if (xDifference > 0f)
            {
                // Player is on the right
                animator.SetTrigger("RightPunch");
            }
            else
            {
                // Player is on the left
                animator.SetTrigger("LeftPunch");
            }
        }

        ActivateHitbox(); // Turn ON punch collider

        yield return new WaitForSeconds(0.3f); // How long the punch is active (adjust if needed)

        DeactivateHitbox(); // Turn OFF punch collider

        yield return new WaitForSeconds(1f); // Attack cooldown before moving again

        isAttacking = false;
        if (!isAttacking && Vector2.Distance(transform.position, player.position) <= stopDistance)
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
}