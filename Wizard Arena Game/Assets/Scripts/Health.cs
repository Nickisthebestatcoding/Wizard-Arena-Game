using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    

    [Header("Health Settings")]
    public float maxHealth = 10f;
    public float currentHealth;

    [Header("UI & Audio")]
    public WizardHealthBar healthBarUI;
    public AudioClip WizardPain;

    private BossSpawner bossSpawner;
    private AudioSource audioSource;

    // Optional: Invincibility frames after damage (uncomment to use)
    // public float invincibilityDuration = 0.5f;
    // private bool isInvincible = false;

    void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
        bossSpawner = FindObjectOfType<BossSpawner>();

        if (healthBarUI != null)
            healthBarUI.UpdateHealthBar(currentHealth / maxHealth);
    }

    public void TakeDamage(float amount)
    {
        // Uncomment if using invincibility frames
        // if (isInvincible) return;

        currentHealth -= amount;
        Debug.Log($"{gameObject.name} took {amount} damage. Remaining health: {currentHealth}");

        if (healthBarUI != null)
            healthBarUI.UpdateHealthBar(currentHealth / maxHealth);

        if (WizardPain != null && audioSource != null)
            audioSource.PlayOneShot(WizardPain);

        if (currentHealth <= 0)
        {
            Die();
        }

        // Uncomment if using invincibility frames
        // StartCoroutine(InvincibilityCoroutine());
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} died!");

        if (CompareTag("Wizard"))
        {
            if (bossSpawner != null)
                bossSpawner.OnWizardDeath();

            gameObject.SetActive(false);

            LevelManager levelManager = FindObjectOfType<LevelManager>();
            if (levelManager != null)
                levelManager.ResetLevel();
            else
                Debug.LogWarning("LevelManager not found.");
        }
        else if (CompareTag("Boss") || CompareTag("Necromancer"))
        {
            if (bossSpawner != null)
                bossSpawner.ResetBossState();

            

            gameObject.SetActive(false);
        }
        else if (CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        if (healthBarUI != null)
            healthBarUI.UpdateHealthBar(1f);

        Debug.Log($"{gameObject.name} health reset.");
    }

    public float GetHealthPercent()
    {
        return currentHealth / maxHealth;
    }

    // Optional: Invincibility frames coroutine
    /*
    IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }
    */
}
