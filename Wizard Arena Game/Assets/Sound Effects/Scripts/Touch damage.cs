using UnityEngine;

public class TouchDamage : MonoBehaviour
{
    public float damageAmount = 2f;
    public float damageInterval = 1f; // How often to deal damage (in seconds)

    private float damageTimer;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Wizard"))
        {
            damageTimer += Time.deltaTime;

            if (damageTimer >= damageInterval)
            {
                Health wizardHealth = other.GetComponent<Health>();
                if (wizardHealth != null)
                {
                    wizardHealth.TakeDamage(damageAmount);
                }
                damageTimer = 0f; // Reset the timer
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Reset timer when the player leaves the area
        if (other.CompareTag("Wizard"))
        {
            damageTimer = 0f;
        }
    }
}
