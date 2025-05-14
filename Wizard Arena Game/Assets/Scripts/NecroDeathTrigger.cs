using UnityEngine;

public class NecroDeathTrigger : MonoBehaviour
{
    public GameObject necromancer;    // Assign your boss GameObject in the Inspector
    public GameObject square;   // Assign your fence GameObject in the Inspector

    private Health necroHealth;

    void Start()
    {
        necroHealth = necromancer.GetComponent<Health>(); // Or whatever script handles boss HP
    }

    void Update()
    {
        if (necroHealth != null && necroHealth.currentHealth <= 0)
        {
            Destroy(square); // Option 1: Permanently remove the fence
            // fence.SetActive(false); // Option 2: Just disable it
            enabled = false; // Stop checking after fence is removed
        }
    }
}
