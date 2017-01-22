using UnityEngine;

public class ExplodyClicker : MonoBehaviour
{
    Rigidbody rigidBody;

    // Use this for initialization
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, 500, 1 << 4))
            {
                Debug.Log("bang");
                rigidBody.AddExplosionForce(1000, hitInfo.point, 50f);
            }
        }
    }
}