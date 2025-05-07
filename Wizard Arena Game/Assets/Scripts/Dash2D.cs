using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash2D : MonoBehaviour
{
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    public KeyCode dashKey = KeyCode.LeftShift;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private bool isDashing = false;
    private bool canDash = true;

    


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(dashKey) && canDash && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    private System.Collections.IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;

        // Dash upward instead of right
        Vector2 dashDirection = transform.up.normalized;
        rb.velocity = dashDirection * dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        rb.velocity = Vector2.zero;
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    public void ResetDash()
    {
        StopAllCoroutines();
        isDashing = false;
        canDash = true;
        rb.velocity = Vector2.zero;
    }

}
