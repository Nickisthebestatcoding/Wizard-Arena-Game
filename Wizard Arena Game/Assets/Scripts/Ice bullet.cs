using UnityEngine;

public class IceBullet : MonoBehaviour
{
    public float speed = 2f;
    public float freezeDuration = 2f;
    public float damage = 2f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;  // Move in the forward direction of the ice bullet
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;

        // Check if the bullet hits the wizard
        if (target.CompareTag("Wizard"))
        {
            PlayerFreeze freeze = target.GetComponent<PlayerFreeze>();
            if (freeze != null)
            {
                freeze.Freeze(freezeDuration);  // Freeze the wizard for the set duration
            }

            Health health = target.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);  // Apply damage to the wizard
            }

            Destroy(gameObject);  // Destroy the ice bullet on impact
            return;  // Exit the method to avoid applying damage to other objects
        }

        // If the bullet hits anything else, check if it has health and apply damage
        Health otherHealth = target.GetComponent<Health>();
        if (otherHealth != null)
        {
            otherHealth.TakeDamage(damage);  // Apply damage to other objects
        }

        // Destroy the ice bullet after it touches anything (wizard or other objects)
        Destroy(gameObject);
    }
}



