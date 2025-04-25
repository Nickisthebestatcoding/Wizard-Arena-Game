
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkeletonBoss : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float damage = 1f;
    public float attackRange = 1.5f;
    public float attackCooldown = 2f;
    public float knockbackForce = 5f;

    private float lastAttackTime = 0f;
    private Animator animator;
    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Wizard")?.transform;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.freezeRotation = true;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            rb.velocity = Vector2.zero;

            if (Time.time - lastAttackTime >= attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }
        else
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
    }

    void Attack()
    {
        // Pick random punch
        if (Random.value > 0.5f)
        {
            animator.SetTrigger("LeftPunch");
        }
        else
        {
            animator.SetTrigger("RightPunch");
        }

        // Damage if player still within range
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= attackRange)
        {
            Health wizardHealth = player.GetComponent<Health>();
            if (wizardHealth != null)
            {
                wizardHealth.TakeDamage(damage);

                // Apply knockback to player
                Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
                if (playerRb != null)
                {
                    Vector2 knockbackDir = (player.position - transform.position).normalized;
                    playerRb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wizard"))
        {
            // Bounce the player away if they touch the boss outside of an attack
            Rigidbody2D playerRb = collision.collider.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                Vector2 knockbackDir = (collision.transform.position - transform.position).normalized;
                playerRb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
            }

            Health wizardHealth = collision.collider.GetComponent<Health>();
            if (wizardHealth != null)
            {
                wizardHealth.TakeDamage(damage);
            }
        }
    }
}