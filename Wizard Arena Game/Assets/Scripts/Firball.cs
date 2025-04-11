using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firball : MonoBehaviour
{
    public float damage = 1f;
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

        Health targetHealth = collision.gameObject.GetComponent<Health>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage);
        }



        // Implement behavior when fireball hits something (e.g., damage, destroy)
        Destroy(gameObject);  // Destroy the fireball on collision (simple example)
    }
}
