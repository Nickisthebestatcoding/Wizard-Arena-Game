using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightBehavior : MonoBehaviour
{
    public float moveSpeed = 2.1f;
    public float chaseRange = 15.0f;
    public float attackRange = 1.5f;
    public float attackCooldown = 1.5f;
    public float damage = 1f;

    private Transform wizardTransform;
    private Animator animator;
    private float lastAttackTime;
    public float knockbackForce = 5f; // Set in Inspector
    public LayerMask wizardLayer; // Set this to the Wizard layer in Inspector
    public float knockbackRadius = 1.5f;
    private Health health;
    private Vector3 startPos; // ?? Start position
    private Quaternion startRot; // ?? Start rotation


    // Start is called before the first frame update
    private void Start()
    {
        GameObject wizard = GameObject.FindGameObjectWithTag("Wizard");

        if (wizard != null)
        {
            wizardTransform = wizard.transform;
        }
        else
        {
            Debug.LogError("Wizard not found in scene. Make sure it's tagged as 'Wizard'.");
        }

        animator = GetComponent<Animator>();
        health = GetComponent<Health>(); // ?? Cache the health


        // ?? Record starting position/rotation
        startPos = transform.position;
        startRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (wizardTransform == null) return;

        Vector3 direction = wizardTransform.position - transform.position;
        direction.z = 0f;

        float distanceToWizard = direction.magnitude;

        if (distanceToWizard <= attackRange)
        {
            // Stop moving & attack
            animator.SetBool("isAttacking", true);

            if (Time.time - lastAttackTime >= attackCooldown)
            {
                lastAttackTime = Time.time;
                // Attack animation will trigger damage via animation event
            }
        }
        else if (distanceToWizard <= chaseRange)
        {
            animator.SetBool("isAttacking", false);

            if (direction.sqrMagnitude > 0.01f)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 270;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }

            Vector3 movement = direction.normalized * moveSpeed * Time.deltaTime;
            transform.position += movement;
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }

    }
    public void DealDamage()
    {
        if (wizardTransform == null) return;

        float distance = Vector3.Distance(transform.position, wizardTransform.position);
        if (distance <= attackRange)
        {
            // Deal damage
            Health wizardHealth = wizardTransform.GetComponent<Health>();
            if (wizardHealth != null)
            {
                wizardHealth.TakeDamage(damage);
            }

            // Apply knockback if possible
            WizardMovement wizardMovement = wizardTransform.GetComponent<WizardMovement>();
            if (wizardMovement != null)
            {
                Vector2 knockbackDirection = (wizardTransform.position - transform.position).normalized;
                wizardMovement.ApplyPush(knockbackDirection * knockbackForce, 0.3f); // 0.3s duration, tweak as needed
            }
        }
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, knockbackRadius, wizardLayer);

        foreach (var hit in hits)
        {
            Health wizardHealth = hit.GetComponent<Health>();
            WizardMovement wizardMovement = hit.GetComponent<WizardMovement>();

            if (wizardHealth != null)
            {
                wizardHealth.TakeDamage(damage);
            }

            if (wizardMovement != null)
            {
                Vector2 knockbackDir = (hit.transform.position - transform.position).normalized;

                // Fallback in case they're overlapping perfectly (zero vector)
                if (knockbackDir == Vector2.zero)
                {
                    knockbackDir = Vector2.up; // arbitrary direction
                }

                wizardMovement.ApplyPush(knockbackDir * knockbackForce, 0.3f);
            }
        }
    }

    // ?? This is the method your LevelManager will call to reset this knight
    public void ResetEnemy()
    {
        transform.position = startPos;
        transform.rotation = startRot;

        if (health != null)
        {
            health.ResetHealth(); // Make sure Health.cs has this method
        }

        gameObject.SetActive(true); // Reactivate if disabled
        animator.SetBool("isAttacking", false); // Reset animation state

        Debug.Log(gameObject.name + " has been reset.");
    }

}


