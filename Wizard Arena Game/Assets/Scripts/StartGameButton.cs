using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
   
    public string gameStartSceneName = "TutorialWorld"; // Change to your actual game scene name

    public void OnStartButtonClicked()
    {
        // Find and reset the timer if it exists
        GameTimer timer = FindObjectOfType<GameTimer>();
        if (timer != null)
        {
            timer.ResetTimer();
        }
        

        // Load the tutorial/game scene
        SceneManager.LoadScene(gameStartSceneName);
    }
}
