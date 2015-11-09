using UnityEngine;
using System.Collections;

public class HoverEngine : MonoBehaviour {

	public float speed;
	public float turnSpeed;
	public Rigidbody car;
	public float hoverHeight = 2f;
	public float hoverForce = 50f; 

	private float acceleration;
	private float turningSpeed;
   	
	void Start () {
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
