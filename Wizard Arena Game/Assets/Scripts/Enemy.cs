using UnityEngine;
using TMPro;
using System;

public class Enemy : MonoBehaviour
{
    public GameObject completedText;  // Assign the UI text in the Inspector

    public void Defeat()
    {
        // Your enemy defeat logic here (e.g., play animation, destroy, etc.)
        ShowCompletedText();
        Destroy(gameObject); // or deactivate, etc.
    }

    private void ShowCompletedText()
    {
        throw new NotImplementedException();
    }

    private void ShowDefeatedText()
    {
        if (completedText != null)
        {
            completedText.SetActive(true);
        }
    }
}