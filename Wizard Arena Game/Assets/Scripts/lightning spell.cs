using UnityEngine;
using System.Collections;

public class LightningSpell : MonoBehaviour
{
    public float damage = 3f;
    public float duration = 0.3f;
    public float chainRadius = 3f; // Max distance to find chained enemies
    public int chainCount = 3; // How many times it will chain to new enemies
    public float chainDamageMultiplier = 0.5f; // Multiplier for the chain damage (50% of original damage)

    private void Start()
    {
        // Optionally play an animation or sound here
        Invoke(nameof(DestroySelf), duration);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;

        // Ignore wizard (caster)
        if (target.CompareTag("Wizard"))
            return;

        // Only damage enemies
        if (target.CompareTag("Enemy") || target.CompareTag("Boss"))
        {
            // Damage the current enemy with normal damage
            Health health = target.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }

            // Turn the enemy purple
            SpriteRenderer sr = target.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.color = Color.magenta; // Change color to purple
                StartCoroutine(ResetColorAfterDelay(sr)); // Start the coroutine to reset the color
            }

            // Chain to other enemies within the range
            ChainLightning(target);
        }
    }

    private void ChainLightning(GameObject lastTarget)
    {
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(lastTarget.transform.position, chainRadius);

        int chainedCount = 0;

        foreach (var enemy in enemiesInRange)
        {
            if (chainedCount >= chainCount) break; // Stop chaining after reaching chainCount

            if (enemy.CompareTag("Enemy") || enemy.CompareTag("Boss"))
            {
                Health health = enemy.GetComponent<Health>();
                if (health != null)
                {
                    // Apply chain damage (reduced)
                    health.TakeDamage(damage * chainDamageMultiplier); // Apply reduced damage
                }

                // Turn the chained enemy purple
                SpriteRenderer sr = enemy.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.color = Color.magenta; // Change color to purple
                    StartCoroutine(ResetColorAfterDelay(sr)); // Start the coroutine to reset the color
                }

                chainedCount++; // Increase the count of chained enemies
            }
        }
    }

    private IEnumerator ResetColorAfterDelay(SpriteRenderer sr)
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second
        if (sr != null) // Make sure the SpriteRenderer still exists
        {
            sr.color = Color.white; // Reset the color to original
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    // Optional: Gizmo to visualize the chain radius in the editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chainRadius);
    }
}
