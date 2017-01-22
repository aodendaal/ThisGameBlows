using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudInputAndMovement : MonoBehaviour
{
    public float movementSpeed = 10f;
    public float maxForce = 100f;
    public Transform aimQuad;

    WindForceApplicator windApplicator;

    Vector3 inputVector;
    float aimRotation;

    float quadDistance = 25f;

    float epsilonSquared;

    public ParticleSystem blowParticles;

    void Awake()
    {
        inputVector = new Vector3();
        epsilonSquared = Mathf.Epsilon * Mathf.Epsilon;

        windApplicator = aimQuad.GetComponent<WindForceApplicator>();
    }

	void Update ()
    {
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.z = Input.GetAxis("Vertical");

         inputVector.Normalize();

        transform.position += inputVector * movementSpeed * Time.deltaTime;

        Vector2 aimVector = new Vector2(Input.GetAxis("horizontal2"), Input.GetAxis("vertical2"));

        float aimStrength = aimVector.sqrMagnitude;

        if (aimVector.sqrMagnitude > epsilonSquared)
        {
            aimQuad.gameObject.SetActive(true);

            aimRotation = Mathf.Rad2Deg * Mathf.Atan2(aimVector.y, aimVector.x);

            Quaternion qRotation = (Quaternion.Euler(0f, aimRotation, 0f));

            aimQuad.position = transform.position + qRotation * (Vector3.right * quadDistance);
            aimQuad.localEulerAngles = new Vector3(aimQuad.localEulerAngles.x, aimRotation, aimQuad.localEulerAngles.z);

            float absolutePower = Input.GetAxis("righttrigger");

            if (absolutePower > Mathf.Epsilon)
            {
                if (!blowParticles.isPlaying)
                {
                    blowParticles.Play();
                }
                windApplicator.ApplyWind(transform.position, absolutePower * maxForce);
            }
            else
            {
               blowParticles.Stop();
            }
        }
        else
        {
            aimQuad.gameObject.SetActive(false);
        }
	}
}
