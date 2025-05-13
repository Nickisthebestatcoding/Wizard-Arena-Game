using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    public float maxHealth = 10f;
    private float currentHealth;
    public WizardHealthBar healthBarUI;
    public AudioClip WizardPain;
    

    void Start()
    {
        currentHealth = maxHealth;
        if (healthBarUI != null)
            healthBarUI.UpdateHealthBar(currentHealth / maxHealth);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log(gameObject.name + " took " + amount + " damage. Remaining health: " + currentHealth);

        if (healthBarUI != null)
            healthBarUI.UpdateHealthBar(currentHealth / maxHealth);

        if (currentHealth <= 0)
            Die();

        if (WizardPain != null)
        {
            GetComponent<AudioSource>().PlayOneShot(WizardPain);
        }

        
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died!");
        

        BossSummonTrigger summonTrigger = FindObjectOfType<BossSummonTrigger>();
        if (CompareTag("Wizard"))
        {
            if (summonTrigger != null)
            {
                summonTrigger.OpenBorders();
                summonTrigger.ResetBossState(); // <- ADD THIS
            }

            gameObject.SetActive(false);
            FindObjectOfType<LevelManager>().ResetLevel();
        }


        else if (CompareTag("Boss"))
        {
            if (summonTrigger != null)
                summonTrigger.OpenBorders();

            gameObject.SetActive(false);
        }
        else if (CompareTag("Enemy")) // Basic enemy
        {
            gameObject.SetActive(false);
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;

        if (healthBarUI != null)
            healthBarUI.UpdateHealthBar(1f);

        Debug.Log(gameObject.name + " health reset.");
    }
    public float GetHealthPercent()
    {
        return currentHealth / maxHealth;
    }
}
