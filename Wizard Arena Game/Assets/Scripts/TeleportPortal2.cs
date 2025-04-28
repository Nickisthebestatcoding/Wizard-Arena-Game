using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportPortal2: MonoBehaviour
{
    public string Level3;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wizard"))
        {
            SceneManager.LoadScene(Level3);
        }
    }
}