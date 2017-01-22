using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAToContinue : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonUp("AButton"))
        {
            Application.LoadLevel(1);
        }
	}
}
