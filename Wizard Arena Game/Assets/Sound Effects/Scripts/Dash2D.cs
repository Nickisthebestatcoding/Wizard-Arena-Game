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

    // Audio-related variables
    public AudioSource dashAudioSource;  // The AudioSource component attached to the GameObject
    public AudioClip dashStartSound;     // Sound to play when the dash starts
    public AudioClip dashEndSound;       // Sound to play when the dash ends

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Optionally ensure AudioSource is attached and initialized
        if (dashAudioSource == null)
        {
            dashAudioSource = GetComponent<AudioSource>();
        }
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

        // Play dash start sound
        if (dashStartSound != null)
        {
            dashAudioSource.PlayOneShot(dashStartSound);
        }

        // Dash upward instead of right
        Vector2 dashDirection = transform.up.normalized;
        rb.velocity = dashDirection * dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        // Stop dash movement and play dash end sound
        rb.velocity = Vector2.zero;
        if (dashEndSound != null)
        {
            dashAudioSource.PlayOneShot(dashEndSound);
        }

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
