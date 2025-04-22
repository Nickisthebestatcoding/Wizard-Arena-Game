using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager1 : MonoBehaviour
{
    public GameObject CompletedText;
    public float displayTime = 3.0f;
    public GameObject gameOver; // Drag your Game Over UI object here

    public void ShowGameOver()
    {
        gameOver.SetActive(true);
        // You can also pause the game, play music, etc. here
    }
    private void Start()
    {
        // Make sure it's hidden initially
        CompletedText.SetActive(false);

        // Subscribe to the enemy defeated event
        Enemy.OnEnemyDefeated += HandleEnemyDefeated;
    }

    void HandleEnemyDefeated()
    {
        StartCoroutine(ShowCompletedTextForSeconds(displayTime));
    }

    IEnumerator ShowCompletedTextForSeconds(float seconds)
    {
        CompletedText.SetActive(true);
        yield return new WaitForSeconds(seconds);
        CompletedText.SetActive(false);
    }

    private void OnDestroy()
    {
        Enemy.OnEnemyDefeated -= HandleEnemyDefeated;
    }

}
