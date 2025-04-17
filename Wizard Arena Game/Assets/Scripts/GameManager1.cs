using UnityEngine;
using UnityEngine.UI;

public class GameManager1 : MonoBehaviour
{
    public GameObject GameOverText;

    public void ShowGameOver()
    {
        GameOverText.SetActive(true);
    }
}
