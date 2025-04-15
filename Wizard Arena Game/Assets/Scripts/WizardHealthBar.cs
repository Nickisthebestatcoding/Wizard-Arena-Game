using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Just drop to half health for testing
            UpdateHealthBar(0.5f);
        }
    }
    public void UpdateHealthBar(float healthPercent)
    {
        int spriteIndex = Mathf.Clamp(Mathf.FloorToInt((1f - healthPercent) * (totalSteps - 1)), 0, totalSteps - 1);
        Debug.Log("Sprite Index: " + spriteIndex);
        healthBarImage.sprite = healthSprites[spriteIndex];
    }
}
