using UnityEngine;
using UnityEngine.UI;

public class GameManager1 : MonoBehaviour
{
    public GameObject CompletedText;

    public void ShowGameOver()
    {
        CompletedText.SetActive(true);
    }

}
