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
        SpawnNecromancer();
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
        DeactivateBoss(); // Disable current boss if needed
        necromancerSpawned = false;
        SpawnNecromancer();
        Debug.Log("Necromancer respawned after wizard death!");
        ZoomCamera(zoomedOutSize);
    }

    public void ResetBossState()
    {
        DeactivateBoss();
        necromancerSpawned = false;
        SpawnNecromancer();
        ZoomCamera(zoomedOutSize);
    }

    public void DeactivateBoss()
    {
        if (necromancer != null)
        {
            necromancer.SetActive(false);
        }
    }

    void ZoomCamera(float targetSize)
    {
        if (mainCamera != null)
        {
            mainCamera.orthographicSize = targetSize;
        }
    }

    public void EndBossBattle()
    {
        ZoomCamera(zoomedInSize);
    }
}
