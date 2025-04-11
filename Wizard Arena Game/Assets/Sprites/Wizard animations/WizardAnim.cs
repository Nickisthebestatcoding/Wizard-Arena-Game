using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAnim : MonoBehaviour
{
    Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from WASD keys
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Check if player is moving
        bool isMoving = horizontal != 0 || vertical != 0;

        // Set the "isWalking" parameter in the Animator
        animator.SetBool("isWalking", isMoving);
    }


}

