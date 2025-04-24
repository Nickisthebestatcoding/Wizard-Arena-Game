using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Image healthBarImage;         // The Image component to update
    public Sprite[] healthSprites;       // Array from full to empty (10 total)
    private int totalSteps;

    void Start()
    {
        totalSteps = healthSprites.Length;
        UpdateHealthBar(1f); // Start full
    }

    public void UpdateHealthBar(float healthPercent)
    {
        int spriteIndex = Mathf.Clamp(Mathf.FloorToInt((1f - healthPercent) * (totalSteps - 1)), 0, totalSteps - 1);
        healthBarImage.sprite = healthSprites[spriteIndex];
    }
}
