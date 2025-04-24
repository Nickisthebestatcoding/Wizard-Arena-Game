using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldspaceHealthBar : MonoBehaviour
{
    public Sprite[] healthSprites; // Full to empty
    public SpriteRenderer spriteRenderer;

    private int totalSteps;

    private void Awake()
    {
        totalSteps = healthSprites.Length;
    }

    public void UpdateHealthBar(float healthPercent)
    {
        int spriteIndex = Mathf.Clamp(Mathf.FloorToInt((1f - healthPercent) * (totalSteps - 1)), 0, totalSteps - 1);
        spriteRenderer.sprite = healthSprites[spriteIndex];
    }
}
