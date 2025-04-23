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

        Animator animator = GetComponent<Animator>();

        if (CompareTag("Wizard"))
        {
            gameObject.SetActive(false);
            FindObjectOfType<LevelManager>().ResetLevel();
        }
        else if (CompareTag("Boss"))
        {
            if (animator != null)
            {
                animator.SetTrigger("Die");
                StartCoroutine(DisableAfterDelay(2f));
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            if (animator != null)
            {
                animator.SetTrigger("Die");
                StartCoroutine(DisableAfterDelay(1.5f));
            }
            else
            {
                gameObject.SetActive(false);
            }
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
