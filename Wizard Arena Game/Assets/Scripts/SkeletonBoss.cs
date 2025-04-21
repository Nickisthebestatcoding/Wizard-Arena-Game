using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBoss : MonoBehaviour
{
    public float damage = 10f;
    public float attackRange = 2f;
    public float attackCooldown = 2f;

    private float lastAttackTime = 0f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            Attack();
        }
    }

    void Attack()
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

                animator.SetTrigger("Attack");
                lastAttackTime = Time.time;
            }
        }
    }
}