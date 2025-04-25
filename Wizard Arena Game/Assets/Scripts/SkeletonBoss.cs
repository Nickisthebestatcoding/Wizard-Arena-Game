
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBoss : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float damage = 1f;
    public float attackRange = 2f;
    public float attackCooldown = 2f;
    public float knockbackForce = 5f;

    private float lastAttackTime = 0f;
    private Transform player;
    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Wizard")?.transform;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player == null) return;

        // Move toward player
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);

        // Check if it's time to attack
        float distance = Vector2.Distance(player.position, transform.position);
        if (distance <= attackRange && Time.time - lastAttackTime >= attackCooldown)
        {
            Attack();
        }
    }

    void Attack()
    {
        if (player == null) return;

        Health wizardHealth = player.GetComponent<Health>();
        if (wizardHealth != null)
        {
            wizardHealth.TakeDamage(damage);
        }

        animator.SetTrigger("Attack");
        lastAttackTime = Time.time;

        ApplyKnockback();
    }

    void ApplyKnockback()
    {
        Rigidbody2D wizardRb = player.GetComponent<Rigidbody2D>();
        if (wizardRb != null)
        {
            Vector2 knockDirection = (player.position - transform.position).normalized;
            wizardRb.AddForce(knockDirection * knockbackForce, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wizard"))
        {
            ApplyKnockback();

            Health wizardHealth = collision.collider.GetComponent<Health>();
            if (wizardHealth != null)
            {
                wizardHealth.TakeDamage(damage);
            }
        }
    }
}