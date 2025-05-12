using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAnim : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        bool isMoving = Input.GetKey(KeyCode.W) ||
                        Input.GetKey(KeyCode.A) ||
                        Input.GetKey(KeyCode.S) ||
                        Input.GetKey(KeyCode.D);

        bool isIdle = !isMoving;

        // Fire animation should play once on click
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Fire");
        }

        // Update other Animator parameters
        animator.SetBool("isWalking", isMoving);
        animator.SetBool("isIdle", isIdle);
    }
}

