using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firball : MonoBehaviour
{
    public float damage = 1f;
    public float pushForce = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject target = other.gameObject;

        if (target.CompareTag("Wizard"))
        {
            WizardMovement wizard = target.GetComponent<WizardMovement>();
            if (wizard != null)
            {
                Vector2 pushDirection = (target.transform.position - transform.position).normalized;
                wizard.ApplyPush(pushDirection * pushForce, 0.5f); // Push for 0.5 seconds
            }

            Destroy(gameObject);
            return;
        }

        Health health = target.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}