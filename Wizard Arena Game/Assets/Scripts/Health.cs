using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    public float maxHealth = 10f;
    private float currentHealth;
    public WizardHealthBar healthBarUI;
    public GameManager1 gameOverManager;
    public GameObject[] borders;  // Reference to the border objects (assign in Inspector)
    private Animator animator;

    // Property to access currentHealth
    public float CurrentHealth
    {
        get { return currentHealth; }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();

        // Ensure health bar updates on start
        if (healthBarUI != null)
        {
            healthBarUI.UpdateHealthBar(currentHealth / maxHealth);
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log(gameObject.name + " took " + amount + " damage. Remaining health: " + currentHealth);

        // Update health bar UI
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

        // Deactivate borders when boss dies
        if (CompareTag("Boss"))
        {
            SetBordersActive(false); // Hide borders
        }

        if (CompareTag("Wizard"))
        {
            gameOverManager.ShowGameOver();
            gameObject.SetActive(false);
            FindObjectOfType<LevelManager>().ResetLevel();
        }
        else if (CompareTag("Boss"))
        {
            if (animator != null)
            {
                animator.SetTrigger("Die");
                StartCoroutine(DisableAfterDelay(2f)); // Wait 2 seconds for animation to finish
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            HandleDeath();
        }
    }

    // Reusable method to handle death for regular enemies
    void HandleDeath()
    {
        if (animator != null)
        {
            animator.SetTrigger("Die");
            StartCoroutine(DisableAfterDelay(1.5f)); // Wait for animation to finish
        }
        else
        {
            gameObject.SetActive(false); // Directly disable the game object if no animator
        }
    }

    // Set borders active or inactive
    private void SetBordersActive(bool isActive)
    {
        foreach (GameObject border in borders)
        {
            if (border != null)
            {
                border.SetActive(isActive); // Activate or deactivate each border
            }
        }
    }

    IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}