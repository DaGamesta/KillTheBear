using UnityEngine;
using System.Collections;

public class BearUpdate : MonoBehaviour {

	public float speed = 20, patrolArea = 400;
	private Animator _animator;
	private bool moveLeft = true;
	private float startX, startY;

	// Use this for initialization
	void Awake () {
		
		_animator = GetComponent<Animator>();
		startX = this.transform.position.x;
		startY = this.transform.position.y;

	}
	
	// Update is called once per frame
	void Update () {

		if (moveLeft) {

			this.transform.position = new Vector3(this.transform.position.x - speed, this.transform.position.y, this.transform.position.z);
			if (startX - this.transform.position.x > patrolArea / 2) {

				moveLeft = false;
				if( transform.localScale.x > 0f )
					transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );

			}

		} else {

			this.transform.position = new Vector3(this.transform.position.x + speed, this.transform.position.y, this.transform.position.z);
			if (startX - this.transform.position.x < -patrolArea / 2) {
				
				moveLeft = true;
				if( transform.localScale.x < 0f )
					transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );
				
			}

		}

	}
}
