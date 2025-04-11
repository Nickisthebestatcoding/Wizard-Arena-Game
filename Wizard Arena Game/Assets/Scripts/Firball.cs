using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firball : MonoBehaviour
{
    public float damage = 1f;
    public float pushForce = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject target = collision.gameObject;

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

        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }


        // Implement behavior when fireball hits something (e.g., damage, destroy)
        Destroy(gameObject);  // Destroy the fireball on collision (simple example)
    }
}
