using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab;
    public Transform bossSpawnPoint;
    public GameObject gateToActivate;

    private bool bossSpawned = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (bossSpawned) return;

        if (other.CompareTag("Wizard")) // Or "Player", depending on your tag
        {
            if (bossPrefab != null && bossSpawnPoint != null)
            {
                Instantiate(bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);
            }

            if (gateToActivate != null)
            {
                gateToActivate.SetActive(true);
            }

            bossSpawned = true;
            gameObject.SetActive(false); // Disable trigger to prevent re-use
        }
    }
}
