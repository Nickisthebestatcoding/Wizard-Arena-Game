using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 10f;
    private float currentHealth;
    public WizardHealthBar healthBarUI;
    public AudioClip WizardPain;

    private bool isDead = false;

    [Header("Healing")]
    public int startingHeals = 5;
    private int currentHeals;

    void Start()
    {
        currentHealth = maxHealth;
        currentHeals = startingHeals;

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

    public void Heal(float amount)
    {
        if (isDead) return;
        if (currentHeals <= 0)
        {
            Debug.Log("No heals left!");
            return;
        }

        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        currentHeals--;

        if (healthBarUI != null)
            healthBarUI.UpdateHealthBar(currentHealth / maxHealth);

        Debug.Log(gameObject.name + " healed. Remaining health: " + currentHealth + " | Heals left: " + currentHeals);
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log(gameObject.name + " died!");

        BossSummonTrigger summonTrigger = FindObjectOfType<BossSummonTrigger>(); // Keeps BossSummonTrigger intact

        if (CompareTag("Wizard"))
        {
            if (summonTrigger != null)
            {
                summonTrigger.OpenBorders();
                summonTrigger.ResetBossState();
                summonTrigger.ResetZoom();
            }

            BossSpawner bossSpawner = FindObjectOfType<BossSpawner>();
            if (bossSpawner != null)
            {
                bossSpawner.DeactivateBoss();
            }

            gameObject.SetActive(false);
            LevelManager levelManager = FindObjectOfType<LevelManager>();
            if (levelManager != null)
            {
                levelManager.ResetLevel();
            }
        }
        else if (CompareTag("Boss"))
        {
            if (summonTrigger != null)
            {
                summonTrigger.OpenBorders();
                summonTrigger.ResetZoom();
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
        currentHeals = startingHeals;

        if (healthBarUI != null)
            healthBarUI.UpdateHealthBar(1f);

        Debug.Log(gameObject.name + " health reset.");
    }

    public float GetHealthPercent()
    {
        return currentHealth / maxHealth;
    }

    public int GetHealCount()
    {
        return currentHeals;
    }
}
