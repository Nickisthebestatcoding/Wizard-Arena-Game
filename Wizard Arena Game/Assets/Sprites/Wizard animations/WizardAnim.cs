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


        // Update Animator parameter
        animator.SetBool("isWalking", isMoving);
    }

   
    
}

