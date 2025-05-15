using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportPortal1 : MonoBehaviour
{
    public string Level2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wizard"))
        {
            SceneManager.LoadScene(Level2);
        }
    }
}