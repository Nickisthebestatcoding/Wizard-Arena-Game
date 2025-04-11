using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToScene : MonoBehaviour
{
    public string sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wizard")) // Make sure your player has the "Player" tag
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}