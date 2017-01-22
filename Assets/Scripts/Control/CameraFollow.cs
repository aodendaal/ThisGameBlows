using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform followObject;

    public float dampingFactor = 10f;
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Vector3.Slerp(transform.position, followObject.position, Time.deltaTime * dampingFactor);	
	}
}
