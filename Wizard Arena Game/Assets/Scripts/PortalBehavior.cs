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

        
    }

    internal void Teleport(GameObject gameObject, GameObject targetObject)
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame




}
