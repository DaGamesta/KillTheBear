using UnityEngine;
using System.Collections;

public class RespondToInput : MonoBehaviour {

	public float acceleration, friction, maxSpeed, hSpeed = 0, vSpeed = 0;

	// Update is called once per frame
	void Update () {

		//add speed according to which key you're pressing.
		if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {

			vSpeed += acceleration;

		}
		if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) {

			vSpeed -= acceleration;

		}
		if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
			
			hSpeed -= acceleration;
			
		}
		if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
			
			hSpeed += acceleration;
			
		}

		//make sure you aren't going over the maximum speed.
		if (vSpeed > maxSpeed) {
			
			vSpeed = maxSpeed;
			
		} else if (vSpeed < -maxSpeed) {
			
			vSpeed = -maxSpeed;
			
		}
		if (hSpeed > maxSpeed) {

			hSpeed = maxSpeed;

		} else if (hSpeed < -maxSpeed) {

			hSpeed = -maxSpeed;

		}

		//apply friction
		if (Mathf.Abs(hSpeed) - friction < 0) {

			hSpeed = 0;

		} else {

			hSpeed -= (Mathf.Abs (hSpeed) / hSpeed) * friction;

		}
		if (Mathf.Abs(vSpeed) - friction < 0) {

			vSpeed = 0;

		} else {

			vSpeed -= (Mathf.Abs (vSpeed) / vSpeed) * friction;

		}

		//change position based on speed after calculations.
		this.transform.Translate (new Vector3 (hSpeed, vSpeed, 0));
		
	}
}
