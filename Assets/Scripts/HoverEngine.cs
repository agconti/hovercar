using UnityEngine;
using System.Collections;

public class HoverEngine : MonoBehaviour {

	[SerializeField] float speed;
	[SerializeField] float turnSpeed;
	[SerializeField] Rigidbody car;
	[SerializeField] float hoverHeight = 2f;
	[SerializeField] float hoverForce = 50f;
	[SerializeField] CardboardHead userPerspective;
	
	float turningSpeed;
	Quaternion currentRotation;
	bool accelerating;
   	
	void Awake () {
		car = GetComponent<Rigidbody> ();
	}	

	void Update () {
		accelerating = Input.GetButton ("Fire1");
//		acceleration = Input.GetAxis ("Vertical") * speed;
//		turningSpeed = Input.GetAxis ("Horizontal") * turnSpeed;
	}

	void Accelerate () {
		Debug.Log (accelerating);
		if (accelerating) {
			Debug.Log ("Accellerating!!");
			car.AddRelativeForce (new Vector3(0f, 0f, speed));
		}

	}

	void Turn () {
		Quaternion currentRotation = transform.rotation;
		Quaternion perspectiveRotation = userPerspective.transform.rotation;
		Quaternion newRotation = Quaternion.Slerp(currentRotation, perspectiveRotation, Time.deltaTime);

		car.AddRelativeTorque (newRotation.eulerAngles);
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
