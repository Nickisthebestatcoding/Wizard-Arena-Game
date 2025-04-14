using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 10f;
    private float currentHealth;
    public HealthBarUI healthBarUI;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        if (healthBarUI != null)
            healthBarUI.UpdateHealthBar(currentHealth / maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float amount)
    {

        currentHealth -= amount;
        Debug.Log(gameObject.name + " took " + amount + " damage. Remaining health: " + currentHealth);

        if (healthBarUI != null)
            healthBarUI.UpdateHealthBar(currentHealth / maxHealth);


        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died!");
        Destroy(gameObject);  // Or trigger animation/effects before destruction
    }
}
