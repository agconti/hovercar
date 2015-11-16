using UnityEngine;
using System.Collections;

public class HoverEngine : MonoBehaviour {

	[SerializeField] Rigidbody carRigidBody;
	[SerializeField] float speed;
	[SerializeField] float turnSpeed;
	[SerializeField] float hoverHeight = 2f;
	[SerializeField] float hoverForce = 50f;
	[SerializeField] CardboardHead userPerspective;

	bool accelerating;

   	
	void Awake () {
		carRigidBody = GetComponent<Rigidbody> ();
	}	

	void Update () {
		accelerating = Input.GetButton ("Fire1");
	}

	void Accelerate () {
		if (accelerating) {
			carRigidBody.AddForce (transform.forward * speed);
		}
	}

	void Turn () {
		/* scale perspective rotation from possible with domain of the user's 360 degree, [0, 360],
		 * range of [ -1, 1 ] to add to the car's rigidbody as torque on its y axis .
		 * y = mx + b
		 * y = (1/90)x + 0 
		 */
		const float m = (1.0f / 360.0f);
		float perspectiveRotationY = userPerspective.transform.rotation.eulerAngles.y;
		float rotation = perspectiveRotationY  * m * turnSpeed;
		float torque = Mathf.Clamp (rotation, -1, 1);
		Debug.Log (perspectiveRotationY);

		carRigidBody.AddTorque (transform.up * torque, ForceMode.VelocityChange);
	}

	void Hover () {
		Ray ray = new Ray (transform.position, -transform.up);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, hoverHeight)) {
			float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
			Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce;

			carRigidBody.AddForce(appliedHoverForce, ForceMode.Acceleration);
		}
		
	}
	
	void FixedUpdate () {
		Hover ();
		Accelerate ();
		Turn ();
	}
}
