using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashForward : MonoBehaviour
{
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    public KeyCode dashKey = KeyCode. E;

    private Rigidbody rb;
    private bool isDashing = false;
    private bool canDash = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
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

        // Save current velocity and override with dash
        Vector3 dashDirection = transform.forward.normalized;
        rb.velocity = dashDirection * dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        // Stop dash
        rb.velocity = Vector3.zero;
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
