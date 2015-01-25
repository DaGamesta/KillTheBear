using UnityEngine;
using System.Collections;

public class AxeLogUpdate : MonoBehaviour {

	private float hSpeed, vSpeed, gravity;
	private static float startX, startY;
	private bool resting = false;
	private static bool active = false;
	private static GameObject localLog;

	public static bool getActive() {

		return active;

	}

	// Use this for initialization
	void Awake () {
		
		startY = this.transform.position.y;
		startX = this.transform.position.x;
		gravity = -.33f;
		hSpeed = 10;
		vSpeed = 10;
		active = false;
		localLog = this.gameObject;
		this.transform.position = new Vector3 (-2000, -2000, 0);
		
	}
	
	// Update is called once per frame
	void Update () {

		if (active) {

			if (!resting) {

				vSpeed += gravity;
				this.transform.position = new Vector3 (this.transform.position.x + hSpeed, this.transform.position.y + vSpeed, this.transform.position.z);
				if (this.transform.position.y < startY) {

					this.transform.position = new Vector3(this.transform.position.x, startY, 0);
					resting = true;

				}

			}

		}
		
	}

	public static void activate() {

		localLog.transform.position = new Vector3 (startX, startY, 0);
		active = true;
		localLog.GetComponent<Animator> ().Play (Animator.StringToHash ("LogNothing"));
		localLog.GetComponent<Animator> ().Play (Animator.StringToHash ("LogSwinging"), 0, 0);

	}

}
