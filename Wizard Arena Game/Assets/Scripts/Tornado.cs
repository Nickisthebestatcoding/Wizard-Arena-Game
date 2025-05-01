using System.Collections;
using UnityEngine;

public class TornadoProjectile : MonoBehaviour
{
    public float moveSpeed = 5f;          // Speed at which the tornado moves
    public float knockbackForce = 10f;    // The force applied to the player when hit by the tornado
    public float knockbackDuration = 0.5f; // Duration of knockback
    public Vector2 direction;             // Direction the tornado moves in

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Make the tornado move in the given direction
        if (direction != Vector2.zero)
        {
            rb.velocity = direction.normalized * moveSpeed;
        }
        else
        {
            // If no direction is given, just move upward by default
            rb.velocity = Vector2.up * moveSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wizard"))
        {
            // Apply knockback to the wizard
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                // Calculate the knockback direction (opposite of the tornado's position)
                Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
                StartCoroutine(ApplyKnockback(playerRb, knockbackDirection));
            }

            // Call ResetSliding on the wizard
            WizardScript wizardScript = collision.gameObject.GetComponent<WizardScript>();
            if (wizardScript != null)
            {
                wizardScript.ResetSliding();
            }

            // Destroy the tornado projectile after collision
            Destroy(gameObject);
        }
    }

    // Knockback effect applied to the wizard
    IEnumerator ApplyKnockback(Rigidbody2D targetRb, Vector2 direction)
    {
        float timer = 0f;
        while (timer < knockbackDuration)
        {
            targetRb.velocity = direction * knockbackForce; // Apply knockback force
            timer += Time.deltaTime;
            yield return null;
        }
        targetRb.velocity = Vector2.zero; // Stop the knockback after duration
    }
}
