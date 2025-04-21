using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportPortal : MonoBehaviour
{
    public string Level1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wizard"))
        {
            SceneManager.LoadScene(Level1);
        }
    }
}