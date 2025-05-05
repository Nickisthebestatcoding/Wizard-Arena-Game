using UnityEngine;

public class WizardTornadoBullet : MonoBehaviour
{
    public float speed = 2f;
    public float damage = 2f;
    public float knockbackForce = 5f;
    public float knockbackDuration = 0.3f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;  // Move in the bullet's forward direction
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;

        if (target.CompareTag("Wizard"))
        {
            // Apply knockback
            WizardMovement wizardMovement = target.GetComponent<WizardMovement>();
            if (wizardMovement != null)
            {
                Vector2 knockbackDir = (target.transform.position - transform.position).normalized;

                // Safety fallback in case direction is zero
                if (knockbackDir == Vector2.zero)
                    knockbackDir = Vector2.up;

                wizardMovement.ApplyPush(knockbackDir * knockbackForce, knockbackDuration);
            }

            // Apply damage
            Health health = target.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }

            Destroy(gameObject);  // Destroy bullet on impact
            return;
        }

        // Damage other objects if they have Health
        Health otherHealth = target.GetComponent<Health>();
        if (otherHealth != null)
        {
            otherHealth.TakeDamage(damage);
        }

        Destroy(gameObject);  // Destroy bullet after hitting anything
    }
}
