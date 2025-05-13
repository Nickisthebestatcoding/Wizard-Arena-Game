using UnityEngine;

public class NecromancerController : MonoBehaviour
{
    public int health = 100;               // Necromancer's health
    public GameObject objectToDeactivate;  // The GameObject to deactivate
           // Optional: death sound

   

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        
        // Deactivate the object (could be the Necromancer or any other object)
        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(false);
        }

        // Optionally, destroy the Necromancer itself (if you want to remove him from the scene)
        Destroy(gameObject);
    }
}

