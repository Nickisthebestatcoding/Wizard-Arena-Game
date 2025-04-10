using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knightbehavior : MonoBehaviour
{
    public float moveSpeed = 2.1f;
    public float chaseRange = 15.0f;           // Maximum distance to start chasing
    private Transform wizardTransform;   // Wizard position reference



    
    // Start is called before the first frame update
    private void Start()
    {
        GameObject wizard = GameObject.FindGameObjectWithTag("Wizard");

        if (wizard != null)
        {
            wizardTransform = wizard.transform;
        }
        else
        {
            Debug.LogError("Wizard not found in scene. Make sure it's tagged as 'Wizard'.");
        }

    }

    // Update is called once per frame
    void Update()
    {


        if (wizardTransform == null) return;

        // Move toward wizard
        Vector3 direction = wizardTransform.position - transform.position;
        direction.z = 0f; // ensure only X and Y movement

        float distanceToWizard = direction.magnitude;

        // Only chase and rotate if within chaseRange
        if (distanceToWizard <= chaseRange)
        {
            // Rotate to face the wizard
            if (direction.sqrMagnitude > 0.01f)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 270;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }

            // Move toward the wizard
            Vector3 movement = direction.normalized * moveSpeed * Time.deltaTime;
            transform.position += movement;
        }
    }
}
