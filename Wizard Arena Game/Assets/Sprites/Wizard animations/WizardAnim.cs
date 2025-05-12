using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAnim : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isMoving = Input.GetKey(KeyCode.W) ||
                        Input.GetKey(KeyCode.A) ||
                        Input.GetKey(KeyCode.S) ||
                        Input.GetKey(KeyCode.D);

        bool isFiring = Input.GetButton("Fire1"); // Default mapped to left mouse button

        // Update Animator parameters
        animator.SetBool("isWalking", isMoving);
        animator.SetBool("isFiring", isFiring);
    }
}
