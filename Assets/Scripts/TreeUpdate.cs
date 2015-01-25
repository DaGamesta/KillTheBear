using UnityEngine;
using System.Collections;

public class TreeUpdate : MonoBehaviour {

	public static GameObject localTree;
	private static int shakeTimer = 0;
	private static int hp = 3;

	// Use this for initialization
	void Awake () {

		localTree = this.gameObject;
		shakeTimer = 0;
		hp = 3;

	}
	
	// Update is called once per frame
	void Update () {

		shakeTimer--;
		if (shakeTimer < 0)
			shakeTimer = 0;

	}

	public static void shake() {

		if (shakeTimer <= 0) {

			localTree.GetComponent<Animator> ().Play (Animator.StringToHash ("TreeNothing"));
			localTree.GetComponent<Animator> ().Play (Animator.StringToHash ("TreeShake"), 0, 0);
			shakeTimer = 35;
			hp--;

		}
		if (hp == 0) {

			BeeUpdate.swarm();

		}

	}
}
