using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBoss : MonoBehaviour
{
    public int maxHealth = 100;  // Maximum health of the boss
    private int currentHealth;

    public int damage = 10;  // Damage dealt by the boss' attack
    public float attackRange = 3f;  // Attack range of the boss
    public float attackCooldown = 2f;  // Time between attacks

    private float lastAttackTime = 0f;  // Time of the last attack
    private Animator animator;  // Animator for boss animations

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if it's time to attack (this can be adjusted with attack logic)
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            Attack();
        }

        // Check if the boss is dead
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    // Call this method when the boss takes damage
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth < 0) currentHealth = 0;
    }

    // The boss attack logic (this can be triggered when certain conditions are met, e.g., player in range)
    void Attack()
    {
        // Placeholder for actual attack logic
        Collider[] hitPlayers = Physics.OverlapSphere(transform.position, attackRange);
        foreach (Collider hit in hitPlayers)
        {
            if (hit.CompareTag("Player"))  // Check if the colliding object is the player
            {
                hit.GetComponent<Health>().TakeDamage(damage);
                animator.SetTrigger("Attack");  // Trigger the attack animation
                lastAttackTime = Time.time;  // Reset attack cooldown
            }
        }
    }

    // Handle boss death
    void Die()
    {
        animator.SetTrigger("Die");  // Trigger the death animation
        Destroy(gameObject, 2f);  // Destroy the boss after the death animation plays
    }

    // Debugging method to show health on screen
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 20), "Boss Health: " + currentHealth);
    }
}
