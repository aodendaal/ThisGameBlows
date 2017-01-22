using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterUVWibble : MonoBehaviour
{
    private Material water;

    private float wibbleOffset1;

    private float wibbleOffset2;

    private float cycle;
    private float cycle2;

    private float twoPi;
	
    // Use this for initialization
	void Awake ()
    {
        water = GetComponent<Renderer>().sharedMaterial;
        twoPi = 2f * Mathf.PI;	
	}
	
	// Update is called once per frame
	void Update ()
    {
        cycle += Mathf.PI * Time.deltaTime/5f;
        cycle2 += Mathf.PI * Time.deltaTime / 6f;

        if(cycle > (twoPi))
        {
            cycle -= twoPi;
        }

        if (cycle2 > (twoPi))
        {
            cycle2 -= twoPi;
        }

        wibbleOffset1 = 0.08f * Mathf.Sin(cycle);

        wibbleOffset2 = 0.08f * Mathf.Cos(cycle2);

        water.SetTextureOffset("_DetailAlbedoMap", new Vector2(wibbleOffset1, wibbleOffset1));
        water.SetTextureOffset("_MainTex", new Vector2(wibbleOffset2, wibbleOffset2));
	}
}
