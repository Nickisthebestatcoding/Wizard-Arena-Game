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
            // Regular enemies
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
        // Wait one frame so that LevelManager.ResetLevel can do everything
        yield return null;

        FindObjectOfType<LevelManager>().ResetLevel();

        yield return null;

        gameObject.SetActive(false); // hide wizard only AFTER reset happens
    }
    IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}

