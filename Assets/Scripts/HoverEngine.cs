using UnityEngine;
using System.Collections;

public class HoverEngine : MonoBehaviour {

	[SerializeField] float speed;
	[SerializeField] float turnSpeed;
	[SerializeField] Rigidbody car;
	[SerializeField] float hoverHeight = 2f;
	[SerializeField] float hoverForce = 50f; 

	float acceleration;
	float turningSpeed;
   	
	void Awake () {
		car = GetComponent<Rigidbody> ();
	}	

	void Update () {
		acceleration = Input.GetAxis ("Vertical") * speed;
		turningSpeed = Input.GetAxis ("Horizontal") * turnSpeed;
	}

	void Accelerate () {
		car.AddRelativeForce (new Vector3(0f, 0f, acceleration));
	}

	void Turn () { 
		car.AddRelativeTorque (new Vector3 (0f, turningSpeed, 0f));
	}

	void Hover () {
		Ray ray = new Ray (transform.position, -transform.up);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, hoverHeight)) {
			float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
			Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce;

			car.AddForce(appliedHoverForce, ForceMode.Acceleration);
		}
		
	}
	
	void FixedUpdate () {
		Hover ();
		Accelerate ();
		Turn ();
	}
}
