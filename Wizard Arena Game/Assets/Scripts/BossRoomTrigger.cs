using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject skeletonBossPrefab;
    public Transform spawnPoint;
    private bool hasSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasSpawned) return;

        if (other.CompareTag("Wizard"))
        {
            hasSpawned = true;
            SpawnBoss();
        }
    }
    void SpawnBoss()
    {
        if (skeletonBossPrefab != null && spawnPoint != null)
        {
            Instantiate(skeletonBossPrefab, spawnPoint.position, spawnPoint.rotation);
            Debug.Log("Skeleton Boss Spawned!");
        }
        else
        {
            Debug.LogWarning("SpawnBoss failed: Prefab or Spawn Point missing.");
        }
    }
}
