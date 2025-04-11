using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizardshooting : MonoBehaviour
{
    public GameObject fireballPrefab;  // The fireball prefab
    public float fireballSpeed = 10f;  // Speed of the fireball
    public Transform fireballSpawnPoint;  // Point where the fireball will spawn (e.g., in front of the wizard)
    // Start is called before the first frame update
    public float fireCooldown = 1f;

    private float lastFireTime = -Mathf.Infinity;
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        // If the wizard is tagged "Wizard" and the player clicks the mouse button (left-click)
        if (this.CompareTag("Wizard") && Input.GetMouseButtonDown(0) && Time.time >= lastFireTime + fireCooldown) // 0 is left-click
        {
            ShootFireball();
            lastFireTime = Time.time;
        }
    }


    private void ShootFireball()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure the z-axis is 0 (since we're working in 2D)

        // Calculate the direction from the wizard to the mouse position
        Vector2 direction = (mousePosition - fireballSpawnPoint.position).normalized;

        // Instantiate the fireball at the spawn point
        GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, Quaternion.identity);

        // Get the Rigidbody2D component of the fireball
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Set the fireball's velocity towards the mouse position
            rb.velocity = direction * fireballSpeed;
        }


        // Optionally, you can rotate the fireball to face the direction it's moving
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        fireball.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

    }
}
