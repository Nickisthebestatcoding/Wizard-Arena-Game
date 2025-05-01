using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WizardScript : MonoBehaviour
{
    public float speed = 4.5f;
    private float originalSpeed;

    float WORLD_MIN_X = -120.0f;
    float WORLD_MIN_Y = -120.0f;
    float WORLD_MAX_X = 120.0f;
    float WORLD_MAX_Y = 120.0f;

    public bool canMove = true;
    public TextMeshProUGUI CompletedText;

    PositionClamp spriteClamp;
    PositionClamp cameraClamp;

    private bool gameOver;
    public bool GameOver { get { return gameOver; } }

    private Rigidbody2D rb;

    void Start()
    {
        originalSpeed = speed;
        rb = GetComponent<Rigidbody2D>();

        Renderer r = GetComponent<Renderer>();
        spriteClamp = new PositionClamp(WORLD_MIN_X, WORLD_MIN_Y, WORLD_MAX_X, WORLD_MAX_Y, r);

        Camera c = GetComponent<Camera>();
        cameraClamp = new PositionClamp(WORLD_MIN_X, WORLD_MIN_Y, WORLD_MAX_X, WORLD_MAX_Y, Camera.main);
    }

    void Update()
    {
        if (!gameOver && canMove)
        {
            transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * speed,
                                Input.GetAxis("Vertical") * Time.deltaTime * speed, 0);
        }

        spriteClamp.limitMovement(transform.position, transform);
        cameraClamp.limitMovement(transform.position, Camera.main.transform);
    }

    public void ResetSliding()
    {
        StartCoroutine(ResetSpeed());
    }

    IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(2f);
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
        speed = originalSpeed;
        canMove = true;
    }
}
