using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	private Rigidbody rb; 
	private AudioSource audioSource;
	[SerializeField] public float thrustSpeed = 1000f;
	[SerializeField] public float rotateSpeed = 50f;
	[SerializeField] AudioClip mainEngine;
	[SerializeField] ParticleSystem mainThrusterParticles;
	[SerializeField] ParticleSystem sideThrusterParticles;
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}

	void Update()
	{
		
		ProcessThrust();
		ProcessRotate();
	}
	private void ProcessThrust() {
		if (Input.GetKey(KeyCode.Space)) {
			rb.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
			if(!audioSource.isPlaying) {
				audioSource.PlayOneShot(mainEngine);
				mainThrusterParticles.Play();
			}
		}else {
			audioSource.Stop();
			mainThrusterParticles.Stop();
		}

	}
	private void ProcessRotate() {
		if (Input.GetKey(KeyCode.A)) {
            ApplyRotation(1);
        }else if (Input.GetKey(KeyCode.D)) {
			ApplyRotation(-1);
		}
	}
	/// <summary>
	/// Applies the rotation transform to the gameobject
	/// </summary>
    private void ApplyRotation(float direction)
    {
		rb.freezeRotation = true; 
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime * direction);
		rb.freezeRotation = false;
    }
}
