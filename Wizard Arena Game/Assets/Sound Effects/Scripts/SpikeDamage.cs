using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    public float damage = 1f;
    public float knockbackForce = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wizard"))
        {
            Health wizardHealth = collision.GetComponent<Health>();
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

            if (wizardHealth != null)
            {
                wizardHealth.TakeDamage(damage);
            }

            if (rb != null)
            {
                Vector2 knockbackDir = (collision.transform.position - transform.position).normalized;
                rb.velocity = knockbackDir * knockbackForce;
            }
        }
    }
}