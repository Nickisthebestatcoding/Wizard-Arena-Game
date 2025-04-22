using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager1 : MonoBehaviour
{
    public GameObject CompletedText;
    public float displayTime = 3f;

    public void ShowGameOver()
    {
        CompletedText.SetActive(true);
    }
    void HandleEnemyDefeated()
    {
        StartCoroutine(ShowCompletedTextForSeconds(displayTime));
    }

    IEnumerator ShowCompletedTextForSeconds(float seconds)
    {
        CompletedText.SetActive(true);
        yield return new WaitForSeconds(3);
        CompletedText.SetActive(false);
    }

}
