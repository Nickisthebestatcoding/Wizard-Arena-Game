using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecroController : MonoBehaviour
{
    public GameObject portalPrefab; // Assign in Inspector
    public Transform portalSpawnPoint; // Where the portal should appear
    private int health = 100;

    void Update()
    {
        if (health <= 0)
        {
            SpawnPortal();
            Destroy(gameObject); // Optional: Remove boss
        }
    }

    void TakeDamage(int amount)
    {
        health -= amount;
    }

    private void SpawnPortal()
    {
        Instantiate(portalPrefab, portalSpawnPoint.position, Quaternion.identity);
    }
}