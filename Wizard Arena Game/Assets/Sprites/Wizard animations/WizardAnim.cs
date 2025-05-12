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

        bool isFiring = Input.GetButton("Fire1");

        bool isIdle = !isMoving;

        // Update Animator parameters
        animator.SetBool("isWalking", isMoving);
        animator.SetBool("isFiring", isFiring);
        animator.SetBool("isIdle", isIdle);
    }
}
}
