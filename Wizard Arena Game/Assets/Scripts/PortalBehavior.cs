using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter
{
    private Camera mainCamera;

    private float xMin;
    private float yMin;
    private float xMax;
    private float yMax;

    

    public Teleporter(Camera cam)// Start is called before the first frame update
    {
        mainCamera = cam;

        float worldHeight = mainCamera.orthographicSize * 3.0F;
        float worldWidth = worldHeight * mainCamera.aspect;

        xMin = -worldWidth / 3.0F;
        yMin = -worldHeight / 3.0F;
        xMax = worldWidth / 3.0F;
        yMax = worldHeight / 3.0F;
    }

    internal void Teleport(GameObject gameObject, GameObject targetObject)
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame




}
