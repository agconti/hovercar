using UnityEngine;
using System.Collections;

public class HoverEngine : MonoBehaviour {

	public float speed;
	public float turnSpeed;
	public Rigidbody car; 

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

	void FixedUpdate () {

		Accelerate ();
		Turn ();
	}
}
