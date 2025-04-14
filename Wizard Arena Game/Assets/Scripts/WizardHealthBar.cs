using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardHealthBar : MonoBehaviour
{
    public Image healthBarImage;
    public Sprite[] healthSprites; // From full health to empty
    private int totalSteps;
    // Start is called before the first frame update
    void Start()
    {
        totalSteps = healthSprites.Length;
        UpdateHealthBar(1f); // Start at full health
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateHealthBar(float healthPercent)
    {
        int spriteIndex = Mathf.Clamp(Mathf.FloorToInt((1f - healthPercent) * (totalSteps - 1)), 0, totalSteps - 1);
        healthBarImage.sprite = healthSprites[spriteIndex];
    }
}
