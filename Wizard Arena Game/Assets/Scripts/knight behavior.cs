using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knightbehavior : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float stoppingDistance = 1.0f;
    public float rotationSpeed = 5.0f;

    private Transform wizard;
    // Start is called before the first frame update
    void Start()
    {
        // Find the wizard in the scene by its tag
        wizard = GameObject.FindGameObjectWithTag("Wizard").transform;

    }

    // Update is called once per frame
    void Update()
    {

        if (wizard != null)
        {
            // Calculate the direction towards the wizard
            Vector3 direction = wizard.position - transform.position;

            // Check if the knight is far enough from the wizard to continue chasing
            if (direction.magnitude > stoppingDistance)
            {
                // Rotate smoothly to face the wizard
                Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

                // Move the knight towards the wizard
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            }
        }


    }
}
