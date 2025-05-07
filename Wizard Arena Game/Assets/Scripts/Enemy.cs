using UnityEngine;
using TMPro;
using System;


public class Enemy : MonoBehaviour
{
    public int health = 10;
    public static event System.Action OnEnemyDefeated;
    public delegate void EnemyDefeatedHandler();

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
   

    void Die()
    {
        // Trigger the event
        OnEnemyDefeated?.Invoke();
        Destroy(gameObject);
    }

}
    