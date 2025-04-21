using UnityEngine;
using UnityEngine.UI;

public class GameManager1 : MonoBehaviour
{
    public GameObject CompleteText;

    public void ShowGameOver()
    {
        CompleteText.SetActive(true);
    }

}
