using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	private Rigidbody rb; 
	[SerializeField] public float thrustSpeed = 1000f;
	[SerializeField] public float rotateSpeed = 50f;
	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		ProcessThrust();
		ProcessRotate();
	}
	private void ProcessThrust() {
		if (Input.GetKey(KeyCode.Space)) {
			rb.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
		}

	}
	private void ProcessRotate() {
		if (Input.GetKey(KeyCode.A)) {
            ApplyRotation(1);
        }else if (Input.GetKey(KeyCode.D)) {
			ApplyRotation(-1);
		}
	}

    private void ApplyRotation(float direction)
    {
		rb.freezeRotation = true; 
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime * direction);
		rb.freezeRotation = false;
    }
}
