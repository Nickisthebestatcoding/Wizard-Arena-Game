using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Wait for the scene to load, then teleport player
        GameObject spawnPoint = GameObject.Find("PlayerSpawnPoint");
        GameObject wizard = GameObject.FindWithTag("Wizard");

        if (spawnPoint != null && wizard != null)
        {
            // Teleport the player to the spawn point position
            wizard.transform.position = spawnPoint.transform.position;
        }
    }
}