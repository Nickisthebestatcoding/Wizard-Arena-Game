using System.Collections;
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
    private Vector3 startPos;
    private Quaternion startRot;

    private Coroutine knockbackRoutine;

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
        health = GetComponent<Health>();

        startPos = transform.position;
        startRot = transform.rotation;
    }

    void Update()
    {
        if (wizardTransform == null) return;

        Vector3 direction = wizardTransform.position - transform.position;
        direction.z = 0f;

        float distanceToWizard = direction.magnitude;

        if (distanceToWizard <= attackRange)
        {
            animator.SetBool("isAttacking", true);

            if (Time.time - lastAttackTime >= attackCooldown)
            {
                lastAttackTime = Time.time;
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
            Health wizardHealth = wizardTransform.GetComponent<Health>();
            if (wizardHealth != null)
            {
                wizardHealth.TakeDamage(damage);
            }

            WizardMovement wizardMovement = wizardTransform.GetComponent<WizardMovement>();
            if (wizardMovement != null)
            {
                Vector2 knockbackDirection = (wizardTransform.position - transform.position).normalized;
                wizardMovement.ApplyPush(knockbackDirection * knockbackForce, 0.3f);
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
                wizardMovement.ApplyPush(knockbackDir * knockbackForce, 0.3f);
            }
        }
    }

    public void ApplyPush(Vector2 force, float duration)
    {
        if (knockbackRoutine != null)
            StopCoroutine(knockbackRoutine);

        knockbackRoutine = StartCoroutine(ApplyKnockback(force, duration));
    }

    private IEnumerator ApplyKnockback(Vector2 force, float duration)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null) yield break;

        float timer = 0f;
        while (timer < duration)
        {
            rb.velocity = force;
            timer += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero;
    }

    public void ResetEnemy()
    {
        transform.position = startPos;
        transform.rotation = startRot;

        if (health != null)
        {
            health.ResetHealth();
        }

        gameObject.SetActive(true);
        animator.SetBool("isAttacking", false);

        Debug.Log(gameObject.name + " has been reset.");
    }
}
