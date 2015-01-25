using UnityEngine;
using System.Collections;

public class BeeUpdate : MonoBehaviour {

	public AudioClip soundBuzz;
	private static bool isSwarm = false;
	public GameObject bear;
	public float chaseSpeed = 5;
	private AudioSource _audio;

	// Use this for initialization
	void Awake () {

		_audio = GetComponent<AudioSource> ();
		isSwarm = false;

	}
	
	// Update is called once per frame
	void Update () {

		if (isSwarm) {

			if (!_audio.isPlaying) {

				_audio.Play ();

			}
			if (Mathf.Sqrt(Mathf.Pow(this.transform.position.x - bear.transform.position.x, 2) + Mathf.Pow(this.transform.position.y - bear.transform.position.y, 2)) < chaseSpeed) {

				this.transform.position = bear.transform.position;

			} else {

				float rotation = Mathf.Atan2(this.transform.position.y - bear.transform.position.y, this.transform.position.x - bear.transform.position.x);
				this.transform.position = new Vector3(this.transform.position.x - Mathf.Cos(rotation) * chaseSpeed, this.transform.position.y - Mathf.Sin (rotation) * chaseSpeed, this.transform.position.z);

			}
			if (Mathf.Sqrt(Mathf.Pow(this.transform.position.x - bear.transform.position.x, 2) + Mathf.Pow(this.transform.position.y - bear.transform.position.y, 2)) < 50) {
				
				BearUpdate.kill ();
				
			}

		}

	}

	public static void swarm() {

		isSwarm = true;

	}

}
