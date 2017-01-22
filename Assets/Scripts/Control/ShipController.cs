using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

    public Transform childToRotate;

    private Rigidbody myBodeh;

    public bool LockedForWind;

    private bool docking;

    private Transform destination;

    public RigidbodyConstraints worldFallConstraints;

    public GameObject particleThingum;

    private AudioSource bellsource;

    private AudioSource kaboomSource;

	protected virtual void Awake ()
    {
        myBodeh = GetComponent<Rigidbody>();
        particleThingum.SetActive(false);
        bellsource = GetComponent<AudioSource>();
        kaboomSource = particleThingum.GetComponent<AudioSource>();
	}

    public void MoveToPoint(Transform destination)
    {
        myBodeh.isKinematic = true;
        LockedForWind = true;
        this.destination = destination;
        docking = true;

        bellsource.Play();
    }

    void OnDestroy()
    {
        HUDController.Instance.ShowDeathPrompt();
    }
	
	protected virtual void Update ()
    {
        if (docking)
        {
            myBodeh.MovePosition(Vector3.Lerp(myBodeh.position, destination.position, Time.deltaTime * 2f));
            myBodeh.MoveRotation(Quaternion.Slerp(myBodeh.rotation, destination.rotation, Time.deltaTime * 2f));
            childToRotate.rotation = Quaternion.Slerp(childToRotate.rotation, Quaternion.LookRotation(destination.right), Time.deltaTime * 10f);

            if ((myBodeh.position - destination.position).magnitude <= 1f)
            {
                //if (Quaternion.Dot(myBodeh.rotation, destination.rotation) >= 0.95f)
                {
                    docking = false;
                    LockedForWind = false;
                    destination = null;
                    myBodeh.isKinematic = false;
                }
            }
        }
        else if (!LockedForWind)
        {
            if (myBodeh.velocity.sqrMagnitude > Mathf.Epsilon)
            {
                Quaternion targetDirection = Quaternion.LookRotation(myBodeh.velocity.normalized);

                Quaternion finalDirection = Quaternion.Slerp(childToRotate.rotation, targetDirection, Time.deltaTime * 2f);

                childToRotate.rotation = finalDirection;
            }
        }
	}

    void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.layer == LayerMask.NameToLayer("LevelBounds"))
        {
            KillConstraints();
            Destroy(gameObject, 5f);
            kaboomSource.PlayDelayed(3f);
        }
    }

    void OnCollisionEnter(Collision ow)
    {       
        if (ow.gameObject.layer == LayerMask.NameToLayer("Ship"))
        {
            particleThingum.SetActive(true);
            LockedForWind = true;
            KillConstraints();
            kaboomSource.Play();
            Destroy(gameObject,5f);
        }
    }

    private void KillConstraints()
    {
        myBodeh.useGravity = true;
        myBodeh.constraints = worldFallConstraints;     
    }
}
