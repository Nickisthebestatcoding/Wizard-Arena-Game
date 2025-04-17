using UnityEngine;
using UnityEngine.UI;

public class GameManager1 : MonoBehaviour
{
    public GameObject CompleteTutorial;

    public void ShowGameOver()
    {
        CompleteTutorial.SetActive(true);
    }

}
