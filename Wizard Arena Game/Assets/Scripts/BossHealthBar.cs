using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Image[] healthSegments;

    // Call this to update the health bar display
    public void UpdateHealthBar(float healthPercent)
    {
        int activeSegments = Mathf.CeilToInt(healthPercent * healthSegments.Length);
        for (int i = 0; i < healthSegments.Length; i++)
        {
            healthSegments[i].enabled = i < activeSegments;
        }
    }
}
