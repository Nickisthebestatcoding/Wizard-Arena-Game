using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Image healthImage;
    public Sprite[] healthSprites; // Assign your 10 sprites in order of full to empty

    public void UpdateBossHealthBar(float normalizedHealth)
    {
        int index = Mathf.Clamp(Mathf.FloorToInt(normalizedHealth * (healthSprites.Length - 1)), 0, healthSprites.Length - 1);
        healthImage.sprite = healthSprites[index];
    }
}
