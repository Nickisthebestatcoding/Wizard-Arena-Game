using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsMouse : MonoBehaviour

{
    public float rotationSpeed = 10f;
    public float minAngle = -90;
    public float maxAngle = 90f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get the mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Set the Z-component of the mouse position to match the camera's Z position
        mousePosition.z = transform.position.z;  // Ensure the mouse position is at the same Z as the object


        // Calculate the direction vector from the sprite to the mouse
        Vector3 direction = mousePosition - transform.position;

        // Calculate the angle to rotate
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // Smoothly rotate the sprite using slerp
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
