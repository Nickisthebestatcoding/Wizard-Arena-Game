using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Image healthBarImage;  // The image representing the health bar
    public Sprite[] healthSprites;  // Array of sprites from full health to empty
    private int totalSteps;

    void Start()
    {
        totalSteps = healthSprites.Length;
    }

    // Update the health bar based on the health percentage
    public void UpdateHealthBar(float healthPercent)
    {
        int spriteIndex = Mathf.Clamp(Mathf.FloorToInt((1f - healthPercent) * (totalSteps - 1)), 0, totalSteps - 1);
        healthBarImage.sprite = healthSprites[spriteIndex];
    }

    public void SetHealthBarImage(Image barImage)
    {
        healthBarImage = barImage;  // Set the health bar image dynamically
    }
}
