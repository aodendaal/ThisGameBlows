using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class VictoryPoint : MonoBehaviour
{
    public Transform destinationPoint;

    public GameObject sourceMarker;

    public GameObject destinationMarker;


    public event Action<VictoryPoint> ShipDocked;

    private bool hasDocked;

    void Awake()
    {
        DeactivateMarkers();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Ship"))
        {
            if (!hasDocked)
            {
                other.GetComponent<ShipController>().MoveToPoint(destinationPoint);

                if(ShipDocked != null)
                {
                    ShipDocked(this);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ship"))
        {
            if(hasDocked)
            {
                hasDocked = false;
            }
        }
    }

    public void ActivateMarker(bool isDestination)
    {
        destinationMarker.SetActive(isDestination);
        sourceMarker.SetActive(!isDestination);
    }

    public void DeactivateMarkers()
    {
        sourceMarker.SetActive(false);
        destinationMarker.SetActive(false);
    }


}
