using UnityEngine;

public class bob : MonoBehaviour
{
    float amount = 0.5f;

    // Update is called once per frame
    private void Update()
    {
        var pos = new Vector3(0, -0.7f + amount * Mathf.Sin(Time.time), 0);

        //transform.localPosition = pos;

        transform.localRotation = Quaternion.AngleAxis(Mathf.Sin(Time.time) * 10.0f, transform.forward);
    }
}