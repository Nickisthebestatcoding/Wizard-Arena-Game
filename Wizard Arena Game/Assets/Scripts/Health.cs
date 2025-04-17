using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    public float maxHealth = 10f;
    private float currentHealth;
    public WizardHealthBar healthBarUI;
    public GameObject GameOverText;
    public float GameOverDisplayTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        GameOverText.SetActive(false);
        currentHealth = maxHealth;
        if (healthBarUI != null)
            healthBarUI.UpdateHealthBar(currentHealth / maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            GameOverText.SetActive(true);
        }
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
            // Don't destroy — instead disable and call LevelManager
            gameObject.SetActive(false);
            FindObjectOfType<LevelManager>().ResetLevel();
        }
        else
        {
            // For enemies: just disable (LevelManager will reset them)
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
        // Wait one frame so that LevelManager.ResetLevel can do everything
        yield return null;

        FindObjectOfType<LevelManager>().ResetLevel();

        yield return null;

        gameObject.SetActive(false); // hide wizard only AFTER reset happens
    }
}

