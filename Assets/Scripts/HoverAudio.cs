using UnityEngine;
using System.Collections;

public class HoverAudio : MonoBehaviour {

	public AudioSource jetSound;

	const float LowPitch = 0.1f; 
	const float HighPitch = 2.0f;
	const float SpeedToRevs = 0.01f;

	float jetPitch;
	Vector3 myVelocity;
	Rigidbody carRigidBody;
	
	void Awake () {
		carRigidBody = GetComponent<Rigidbody> ();
	}

	void FixedUpdate () {
		myVelocity = carRigidBody.velocity;
		float forwardSpeed = transform.InverseTransformDirection (myVelocity).z;
		float engineRevs = Mathf.Abs (forwardSpeed) * SpeedToRevs;
		jetSound.pitch = Mathf.Clamp (engineRevs, LowPitch, HighPitch);
	}
}
