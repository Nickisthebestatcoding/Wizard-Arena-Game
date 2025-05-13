using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalActivator : MonoBehaviour
{
    public GameObject necromancer;         // Assign boss GameObject in Inspector
    public GameObject portalSource1; // The actual portal to show (child or separate)

    private bool portalActive = false;

    void Start()
    {
        if (portalSource1 != null)
            portalSource1.SetActive(false);
    }

    void Update()
    {
        if (!portalActive && necromancer == null) // Boss destroyed
        {
            ActivatePortal();
        }
    }

    void ActivatePortal()
    {
        portalActive = true;
        if (portalSource1 != null)
            portalSource1.SetActive(true);

        // Add animation/sound/etc here
    }
}