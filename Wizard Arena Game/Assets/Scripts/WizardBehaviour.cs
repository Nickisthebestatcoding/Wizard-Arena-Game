using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WizardScript : MonoBehaviour
{
    // speed of wizard
    public float speed = 4.5f;

    // world boundaries
    float WORLD_MIN_X = -120.0f;
    float WORLD_MIN_Y = -120.0f;
    float WORLD_MAX_X = 120.0f;
    float WORLD_MAX_Y = 120.0f;
    public bool canMove = true;

    public TextMeshProUGUI CompletedText;
    // utility objects to limit the positions
    PositionClamp spriteClamp;
    PositionClamp cameraClamp;
    private bool gameOver;
    public bool GameOver
    {
        get { return gameOver;
        }
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        Renderer r = GetComponent<Renderer>();
        spriteClamp = new PositionClamp(WORLD_MIN_X, WORLD_MIN_Y, WORLD_MAX_X, WORLD_MAX_Y, r);// set up PositionClamp to limit sprite position within world boundaries

        Camera c = GetComponent<Camera>();
        cameraClamp = new PositionClamp(WORLD_MIN_X, WORLD_MIN_Y, WORLD_MAX_X, WORLD_MAX_Y, Camera.main);
       

        // set up PositionClamp to limit camera position within world boundaries
    }

    // Update is called once per frame
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
}
