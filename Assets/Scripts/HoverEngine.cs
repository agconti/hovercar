using UnityEngine;
using System.Collections;

public class HoverEngine : MonoBehaviour {

	[SerializeField] Rigidbody car;
	[SerializeField] float speed;
	[SerializeField] float turnSpeed;
	[SerializeField] float hoverHeight = 2f;
	[SerializeField] float hoverForce = 50f;
	[SerializeField] CardboardHead userPerspective;

	bool accelerating;
	Quaternion currentRotation;
	const float rotationSpeed = 0.1f;

   	
	void Awake () {
		car = GetComponent<Rigidbody> ();
	}	

	void Update () {
		accelerating = Input.GetButton ("Fire1");
	}

	void Accelerate () {
		if (accelerating) {
			car.AddRelativeForce (new Vector3(0f, 0f, speed));
		}
	}

	void Turn () {
		Vector3 perspectiveRotation = userPerspective.transform.rotation.eulerAngles;
		// scale perspective rotation from possible with domain [-90, 90] to the rotation's range of -1, 1.
		// add result as torque to car RB
		// y = mx + b
		// y = (1/360)x + 0 

		float rotation = (1.0f / 90.0f) * perspectiveRotation.y * turnSpeed;
		float torque = Mathf.Clamp (rotation, -1, 1);
		Debug.LogFormat ("T: {0}, Y: {1}", torque, perspectiveRotation.y);
			
		car.AddTorque (new Vector3(0f, torque, 0f));
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
