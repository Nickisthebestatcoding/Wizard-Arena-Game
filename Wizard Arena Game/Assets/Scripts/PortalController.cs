using System.Collections;

using UnityEngine;

public class PortalController : MonoBehaviour
{
    public GameObject ball;

    float portalRadius;

    bool scored;
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer r = GetComponent<SpriteRenderer>();
        portalRadius = r.bounds.size.x / 3;

        scored = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (scored == true)
            
        {
            
        }
    }
}
