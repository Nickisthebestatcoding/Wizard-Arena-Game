using UnityEngine;

public class WizardTornadoBullet : MonoBehaviour
{
    public float speed = 2f;
    public float damage = 2f;
    public float lifetime = 20f; // The bullet will live for 20 seconds

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;

        // Destroy the bullet after 20 seconds
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;
        bool isEnemy = target.CompareTag("Enemy");
        bool isBoss = target.CompareTag("Boss");

        if (isEnemy || isBoss)
        {
            Debug.Log("Tornado hit: " + target.name);

            // Apply damage
            Health health = target.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }

        // Damage other objects if they have Health
        Health otherHealth = target.GetComponent<Health>();
        if (otherHealth != null)
        {
            otherHealth.TakeDamage(damage);
        }
    }
}
