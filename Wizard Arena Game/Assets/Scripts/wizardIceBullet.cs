using UnityEngine;

public class WizardIceBullet : MonoBehaviour
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

        // Check if the bullet hits an enemy or boss (ensure they are tagged as "Enemy" or similar)
        if (target.CompareTag("Enemy"))
        {
            EnemyFreeze freeze = target.GetComponent<EnemyFreeze>();
            if (freeze != null)
            {
                freeze.Freeze(freezeDuration);  // Freeze the enemy for the set duration
            }

            Health health = target.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);  // Apply damage to the enemy/boss
            }

            Destroy(gameObject);  // Destroy the ice bullet on impact
            return;  // Exit the method to avoid applying damage to other objects
        }

        // Destroy the ice bullet after it touches any other object that is not an enemy/boss
        Destroy(gameObject);
    }
}
