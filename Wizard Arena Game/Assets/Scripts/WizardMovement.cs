using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    
    private bool isBeingPushed = false;


    private float pushTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void ApplyPush(Vector2 force, float duration)
    {
        if (isBeingPushed) return;

        StartCoroutine(PushCoroutine(force, duration));
    }


    private IEnumerator PushCoroutine(Vector2 force, float duration)
    {
        isBeingPushed = true;
        rb.velocity = force;

        yield return new WaitForSeconds(duration);

        rb.velocity = Vector2.zero;
        isBeingPushed = false;
    }
}


