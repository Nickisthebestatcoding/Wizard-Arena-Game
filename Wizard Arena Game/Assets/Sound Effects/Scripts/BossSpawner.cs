using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject necromancerPrefab; // Reference to the Necromancer prefab
    public Transform spawnLocation; // Location to spawn the Necromancer
    private GameObject necromancer; // Reference to the spawned Necromancer

    private bool necromancerSpawned = false;

    [Header("Camera Settings")]
    public Camera mainCamera; // The main camera
    public float zoomedInSize = 8f; // Normal camera size
    public float zoomedOutSize = 12f; // Zoomed out size for boss battle

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (necromancerSpawned) return;

        if (other.CompareTag("Wizard"))
        {
            Debug.Log("Wizard entered boss spawner trigger.");
            SpawnNecromancer();
        }
    }

    void SpawnNecromancer()
    {
        if (necromancerSpawned) return;

        if (necromancerPrefab != null && spawnLocation != null)
        {
            necromancer = Instantiate(necromancerPrefab, spawnLocation.position, Quaternion.identity);
            necromancerSpawned = true;
            Debug.Log("Necromancer Spawned!");

            ZoomCamera(zoomedOutSize);
        }
        else
        {
            Debug.LogWarning("Necromancer prefab or spawn location not set!");
        }
    }

    public void OnWizardDeath()
    {
        if (necromancer != null)
        {
            Destroy(necromancer);
            necromancerSpawned = false;
            SpawnNecromancer();

            Debug.Log("Necromancer has been reset!");

            ZoomCamera(zoomedOutSize);
        }
    }

    public void ResetBossState()
    {
        if (necromancer != null)
        {
            Destroy(necromancer);
            necromancerSpawned = false;
        }

        SpawnNecromancer();
        ZoomCamera(zoomedOutSize);
    }

    public void DeactivateBoss()
    {
        if (necromancer != null)
        {
            necromancer.SetActive(false);
            necromancerSpawned = false;
        }
    }

    public void EndBossBattle()
    {
        ZoomCamera(zoomedInSize);
    }

    void ZoomCamera(float targetSize)
    {
        if (mainCamera != null)
        {
            mainCamera.orthographicSize = targetSize;
        }
    }
}
