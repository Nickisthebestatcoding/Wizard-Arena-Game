using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WizardScript : MonoBehaviour
{
    // speed of wizard
    float speed = 3.0f;

    // world boundaries
    float WORLD_MIN_X = -100.0f;
    float WORLD_MIN_Y = -100.0f;
    float WORLD_MAX_X = 100.0f;
    float WORLD_MAX_Y = 100.0f;

    // utility objects to limit the positions
    PositionClamp spriteClamp;
    PositionClamp cameraClamp;
    public Teleporter porter;
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
        // move the wizard in the current direciton at the current speed
        transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * speed,
                            Input.GetAxis("Vertical") * Time.deltaTime * speed, 0);

        // Now that the position has been updated, limit
        // the X and Y coordinates and make sure they
        // do not go beyond certain boundaries
        spriteClamp.limitMovement(transform.position, transform);

        // update new camera position (X and Y changes only)
        // to match new sprite position
        cameraClamp.limitMovement(transform.position, Camera.main.transform);
    }
}
