using UnityEngine;
using TMPro;
using System;


public class Enemy : MonoBehaviour
{
    
    public GameObject CompletedText;  // Assign the UI text in the Inspector

    public void Defeat()
    {
        // Your enemy defeat logic here (e.g., play animation, destroy, etc.)
        ShowCompletedText();
        Destroy(gameObject); // or deactivate, etc.
    }

    private void ShowCompletedText()
    {
        if (CompletedText != null)
        {
            CompletedText.SetActive(true);
        }
    }
}
    