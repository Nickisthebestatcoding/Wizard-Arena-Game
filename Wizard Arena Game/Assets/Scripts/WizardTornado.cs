using UnityEngine;

public class WizardTornadoBullet : MonoBehaviour
{
    public float speed = 2f;
    public float damage = 2f;
    public float knockbackForce = 5f;
    public float bossKnockbackMultiplier = 0.4f; // Bosses get less knockback
    public float knockbackDuration = 0.3f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;
        bool isEnemy = target.CompareTag("Enemy");
        bool isBoss = target.CompareTag("Boss");

        if (isEnemy || isBoss)
        {
            // Apply knockback
            WizardMovement wizardMovement = target.GetComponent<WizardMovement>();
            if (wizardMovement != null)
            {
                Vector2 knockbackDir = (target.transform.position - transform.position).normalized;
                if (knockbackDir == Vector2.zero)
                    knockbackDir = Vector2.up;

                float force = isBoss ? knockbackForce * bossKnockbackMultiplier : knockbackForce;
                wizardMovement.ApplyPush(knockbackDir * force, knockbackDuration);
            }

            // Apply damage
            Health health = target.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }

            Destroy(gameObject);
            return;
        }

        // Damage other objects if they have Health
        Health otherHealth = target.GetComponent<Health>();
        if (otherHealth != null)
        {
            otherHealth.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
