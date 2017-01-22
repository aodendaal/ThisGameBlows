using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryPoint : MonoBehaviour
{
    public Transform destinationPoint;

    private Collider collider;

    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Ship"))
        {
            other.GetComponent<ShipController>().MoveToPoint(destinationPoint);
            collider.enabled = false;
        }
    }


}
