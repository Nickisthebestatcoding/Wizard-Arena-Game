using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    public float maxHealth = 10f;
    private float currentHealth;
    public WizardHealthBar healthBarUI;

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
        {
            healthBarUI.UpdateHealthBar(currentHealth / maxHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died!");

        if (CompareTag("Wizard"))
        {
            FindObjectOfType<LevelManager>().ShowGameOver();

            // Open borders on player death
            BossSummonTrigger summonTrigger = FindObjectOfType<BossSummonTrigger>();
            if (summonTrigger != null)
            {
                summonTrigger.OpenBorders();
            }

            gameObject.SetActive(false);
            FindObjectOfType<LevelManager>().ResetLevel();
        }
        else
        {
            // Open borders if the boss dies
            if (CompareTag("Boss")) // Make sure boss has this tag
            {
                BossSummonTrigger summonTrigger = FindObjectOfType<BossSummonTrigger>();
                if (summonTrigger != null)
                {
                    summonTrigger.OpenBorders();
                }
            }

            gameObject.SetActive(false);
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;

        if (healthBarUI != null)
        {
            healthBarUI.UpdateHealthBar(1f);
        }

        Debug.Log(gameObject.name + " health reset.");
    }

    IEnumerator DelayedReset()
    {
        yield return null;
        FindObjectOfType<LevelManager>().ResetLevel();
        yield return null;
        gameObject.SetActive(false);
    }

    IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
