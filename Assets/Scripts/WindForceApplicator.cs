using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindForceApplicator : MonoBehaviour
{
    private List<Rigidbody> shipList;

    public float maxHalfAngle = 30f;

    private void Awake()
    {
        shipList = new List<Rigidbody>();
    }

    public void ApplyWind(Vector3 basePoint, float power)
    {
        for (int i = 0; i < shipList.Count; i++)
        {
            if(shipList[i].GetComponent<ShipController>().LockedForWind)
            {
                continue;
            }

            Vector3 directionVector = shipList[i].position - basePoint;

            Vector3 windVector = transform.position - basePoint;

            float direction = Mathf.Atan2(directionVector.z, directionVector.x) * Mathf.Rad2Deg;

            float windDirection = Mathf.Atan2(windVector.z, windVector.x) * Mathf.Rad2Deg;

            float absAngle = Mathf.Abs(windDirection - direction);

            if(absAngle > 300f)
            {
                absAngle = Mathf.Abs(absAngle - 360f);
            }

            if ( absAngle <= maxHalfAngle)
            {
                
                Quaternion forceDirection = Quaternion.Euler(0, -direction, 0);
                shipList[i].AddForce(forceDirection * Vector3.right * power, ForceMode.Acceleration);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ship"))
        {
            shipList.Add(other.GetComponent<Rigidbody>());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Ship"))
        {
            shipList.Remove(other.GetComponent<Rigidbody>());
        }
    }

	void OnDisable()
    {
        shipList.Clear();
    }
}
