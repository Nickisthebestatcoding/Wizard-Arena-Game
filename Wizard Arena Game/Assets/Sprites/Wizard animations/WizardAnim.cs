using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAnim : MonoBehaviour
{
    Animator myAnim;
    
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            myAnim.SetInteger("State", 1);
            Debug.Log("Walking");
        }
       else 
        {
            myAnim.SetInteger("State" , 0);
            Debug.Log("Not");
        }

        

    }
}
