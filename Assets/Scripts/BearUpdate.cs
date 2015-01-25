using UnityEngine;
using System.Collections;

public class BearUpdate : MonoBehaviour {

	public static GameObject localBear;
	public float speed = 20, patrolArea = 400;

	public AudioClip soundBearGrowling;
	public AudioClip soundBear;
	public AudioClip soundBearBite;

	public static AudioSource _audio;

	private Animator _animator;
	private bool moveLeft = true;
	private static bool maul = false, end = false;
	private float startX, startY;
	private int endTimer = 150;
	private static bool fallenDown = false;
	private static bool dead = false;
	private bool angry = false;
	public GameObject player;
	private static int stuckTimer = 300;

	public static void setMaul() {

		maul = true;
		localBear.transform.position = new Vector3 (localBear.transform.position.x, localBear.transform.position.y + 50, localBear.transform.position.z);

	}

	public static void fallDown() {

		if (stuckTimer > 0) {

			fallenDown = true;

		}

	}

	public static void kill() {

		if (!dead) {

			dead = true;
			localBear.transform.position = new Vector3 (localBear.transform.position.x, localBear.transform.position.y + 50, localBear.transform.position.z);

		}

	}

	public static bool getFallen() {

		return fallenDown;

	}

	// Use this for initialization
	void Awake () {
		
		_animator = GetComponent<Animator>();
		_audio = GetComponent<AudioSource>();
		startX = this.transform.position.x;
		startY = this.transform.position.y;
		localBear = this.gameObject;
		maul = false;
		fallenDown = false;
		dead = false;
		end = false;
		stuckTimer = 300;

	}
	
	// Update is called once per frame
	void Update () {

		if (!dead) {

			if (!maul && !fallenDown) {

				if (player.transform.position.y < -200 && !angry){
				
					angry = true;
					UpdatePlayer._audio.PlayOneShot (soundBearGrowling);
					}

				if (!angry) {

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

				} else {

					if (player.transform.position.x < this.transform.position.x) {
						
						this.transform.position = new Vector3(this.transform.position.x - speed, this.transform.position.y, this.transform.position.z);
						moveLeft = true;
						if( transform.localScale.x < 0f )
							transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );
						
					} else {
						
						this.transform.position = new Vector3(this.transform.position.x + speed, this.transform.position.y, this.transform.position.z);

						moveLeft = false;
						if( transform.localScale.x > 0f )
							transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );
						
					}

				}

				if (this.transform.position.x < -300) {
					
					LogController.bearWalkOver();
					
				}

			} else if (maul) {

				if (!end) {

					_animator.Play (Animator.StringToHash("BearMauling"));
					end = true;

				} else if (end) {

					endTimer--;
					if (endTimer == 30) {
						
						UpdatePlayer._audio.PlayOneShot (soundBearBite);
						
					}
					if (endTimer <= 0) {

						Application.LoadLevel("Level1");

					}

				}

			} else if (fallenDown) {

				_animator.Play (Animator.StringToHash ("BearClimbing"));
				if(!_audio.isPlaying)
					_audio.Play();
				stuckTimer--;
				if (stuckTimer <= 0) {
					
					fallenDown = false;
					_animator.Play (Animator.StringToHash("BearWalking"));
					
				}

			}

		} else {

			_animator.Play (Animator.StringToHash ("BearDying"));
			endTimer--;
			if (endTimer <= 0) {

				Application.LoadLevel ("Level1");

			}

		}

	}

}
