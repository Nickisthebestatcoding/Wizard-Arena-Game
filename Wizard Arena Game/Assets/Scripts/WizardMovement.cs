using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isBeingPushed = false;
    private float pushStopTime = 0.5f; // How long the push effect lasts
    private float pushTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeingPushed)
        {
            pushTimer -= Time.deltaTime;
            if (pushTimer <= 0f)
            {
                isBeingPushed = false;
                rb.velocity = Vector2.zero; // Stop sliding
            }
        }
    }

    public void ApplyPush(Vector2 force, float duration)
    {
        isBeingPushed = true;
        pushTimer = duration;
        rb.AddForce(force, ForceMode2D.Impulse);
    }
}
