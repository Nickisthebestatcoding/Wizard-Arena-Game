using UnityEngine;
using TMPro;
using System;
public class GamePrefab
{
    internal void SetActive(bool v)
    {
        throw new NotImplementedException();
    }
}

public class Enemy : MonoBehaviour
{
    
    public GamePrefab completedText;  // Assign the UI text in the Inspector

    public void Defeat()
    {
        // Your enemy defeat logic here (e.g., play animation, destroy, etc.)
        ShowCompletedText();
        Destroy(gameObject); // or deactivate, etc.
    }

    private void ShowCompletedText()
    {
        if (completedText != null)
        {
            completedText.SetActive(true);
        }
    }
}
    