using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    public float maxHealth = 10f;
    private float currentHealth;

    public WizardHealthBar healthBarUI;     // For Wizard
    public BossHealthBar bossHealthBarUI;   // For Boss
    public WorldspaceHealthBar worldspaceHealthBarUI;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log(gameObject.name + " took " + amount + " damage. Remaining health: " + currentHealth);

        UpdateHealthUI();
        if (worldspaceHealthBarUI != null)
            worldspaceHealthBarUI.UpdateHealthBar(currentHealth / maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        float percent = currentHealth / maxHealth;

        if (healthBarUI != null)
        {
            healthBarUI.UpdateHealthBar(percent); // Wizard health bar
        }

        if (bossHealthBarUI != null)
        {
            bossHealthBarUI.UpdateHealthBar(percent); // Boss health bar
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died!");

        if (CompareTag("Wizard"))
        {
            FindObjectOfType<LevelManager>().ShowGameOver();

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
            if (CompareTag("Boss"))
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
        UpdateHealthUI();
        Debug.Log(gameObject.name + " health reset.");
        if (worldspaceHealthBarUI != null)
            worldspaceHealthBarUI.UpdateHealthBar(currentHealth / maxHealth);
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
