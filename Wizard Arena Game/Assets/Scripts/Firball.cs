using UnityEngine;

public class Firball : MonoBehaviour
{
    public float damage = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject target = other.gameObject;

        // Check if it hits a wizard
        if (target.CompareTag("Wizard"))
        {
            Health health = target.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);  // Apply damage
            }

            // Destroy the fireball after hitting the wizard
            Destroy(gameObject);
            return;  // Exit the method to avoid applying damage to other objects
        }

        // If it hits other objects with health (e.g., enemies), apply damage
        Health otherHealth = target.GetComponent<Health>();
        if (otherHealth != null)
        {
            otherHealth.TakeDamage(damage);  // Apply damage
        }

        // Destroy the fireball after hitting any object
        Destroy(gameObject);
    }
}
