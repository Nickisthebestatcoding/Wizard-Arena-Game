using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAnim : MonoBehaviour
{
    Animator myAnim;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();  
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.D))
        {
            myAnim.SetBool("iswalking", true);
        }
       else
        {
            myAnim.SetBool("iswalking" , false);
        }
        
    }
}
