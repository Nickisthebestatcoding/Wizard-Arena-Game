using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 10f;
    private float currentHealth;
    public WizardHealthBar healthBarUI;
    public AudioClip WizardPain;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        if (healthBarUI != null)
            healthBarUI.UpdateHealthBar(currentHealth / maxHealth);
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        Debug.Log(gameObject.name + " took " + amount + " damage. Remaining health: " + currentHealth);

        if (healthBarUI != null)
            healthBarUI.UpdateHealthBar(currentHealth / maxHealth);

        if (WizardPain != null)
            GetComponent<AudioSource>().PlayOneShot(WizardPain);

        if (currentHealth <= maxHealth / 2)
        {
            SkeletonBoss boss = GetComponent<SkeletonBoss>();
            if (boss != null)
            {
                boss.EnterRageMode();
            }
        }

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log(gameObject.name + " died!");

        BossSummonTrigger summonTrigger = FindObjectOfType<BossSummonTrigger>();

        if (CompareTag("Wizard"))
        {
            if (summonTrigger != null)
            {
                summonTrigger.OpenBorders();
                summonTrigger.ResetBossState();
                summonTrigger.ResetZoom(); // Reset camera zoom
            }

            gameObject.SetActive(false);
            FindObjectOfType<LevelManager>().ResetLevel();
        }
        else if (CompareTag("Boss"))
        {
            if (summonTrigger != null)
            {
                summonTrigger.OpenBorders();
                summonTrigger.ResetZoom(); // Reset camera zoom
            }

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
        isDead = false;

        if (healthBarUI != null)
            healthBarUI.UpdateHealthBar(1f);

        Debug.Log(gameObject.name + " health reset.");
    }

    // Method to get the health percentage (0 to 1)
    public float GetHealthPercent()
    {
        return currentHealth / maxHealth;
    }
}
