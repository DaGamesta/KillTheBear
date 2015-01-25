using UnityEngine;
using System.Collections;

public class UpdatePlayer : MonoBehaviour
{
	// movement config
	public float gravity = -25f;
	public float runSpeed = 8f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	public float inAirDamping = 5f;
	public float jumpHeight = 3f;
	public int attackTimer = 0;
	
	[HideInInspector]
	private float normalizedHorizontalSpeed = 0;
	
	private CharacterController2D _controller;
	private Animator _animator;
	private RaycastHit2D _lastControllerColliderHit;
	private Vector3 _velocity;
	private bool holdingAxe = false;
	private bool attacking = false;
	public GameObject theBear;
	
	
	
	void Awake()
	{
		_animator = GetComponent<Animator>();
		_controller = GetComponent<CharacterController2D>();
		
		// listen to some events for illustration purposes
		_controller.onControllerCollidedEvent += onControllerCollider;
		_controller.onTriggerEnterEvent += onTriggerEnterEvent;
		_controller.onTriggerExitEvent += onTriggerExitEvent;
	}
	
	
	#region Event Listeners
	
	void onControllerCollider( RaycastHit2D hit )
	{
		// bail out on plain old ground hits cause they arent very interesting
		if( hit.normal.y == 1f )
			return;
		
		// logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
		//Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
	}
	
	
	void onTriggerEnterEvent( Collider2D col )
	{
		
		Debug.Log( "onTriggerEnterEvent: " + col.gameObject.name );
		if (col.name == "Axe") {

			Destroy (col.gameObject);
			holdingAxe = true;

		}
		if (col.name == "TrapFront") {

			LogController.destroyLog();
			
		}
		if (col.name == "Bear") {

			if (!BearUpdate.getFallen()) {

				Destroy (this.gameObject);
				BearUpdate.setMaul();

			} else if (attacking) {

				BearUpdate.kill();

			}

		}

	}
	
	
	void onTriggerExitEvent( Collider2D col )
	{
		//Debug.Log( "onTriggerExitEvent: " + col.gameObject.name );
	}
	
	#endregion
	
	
	// the Update loop contains a very simple example of moving the character around and controlling the animation
	void Update()
	{
		// grab our current _velocity to use as a base for all calculations
		_velocity = _controller.velocity;
		
		if( _controller.isGrounded )
			_velocity.y = 0;
			
		if (Input.GetKey (KeyCode.Space) && holdingAxe) {

			attacking = true;
			attackTimer = 30;

		}

		if (!attacking) {

			if( Input.GetKey( KeyCode.RightArrow ) ||  Input.GetKey( KeyCode.D ))
			{
				normalizedHorizontalSpeed = 1;
				if( transform.localScale.x < 0f )
					transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );
				
				if( _controller.isGrounded )
					if (!holdingAxe)
					_animator.Play( Animator.StringToHash( "PlayerRunning" ) );
					else
					_animator.Play( Animator.StringToHash( "PlayerRunningWithAxe" ) );
			}
			else if( Input.GetKey( KeyCode.LeftArrow ) ||  Input.GetKey( KeyCode.A ) )
			{
				normalizedHorizontalSpeed = -1;
				if( transform.localScale.x > 0f )
					transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );
				
				if( _controller.isGrounded )
					if (!holdingAxe)
					_animator.Play( Animator.StringToHash( "PlayerRunning" ) );
					else
					_animator.Play( Animator.StringToHash( "PlayerRunningWithAxe" ) );
			}
			else
			{
				normalizedHorizontalSpeed = 0;
				
				if( _controller.isGrounded )
					if(!holdingAxe)
					_animator.Play( Animator.StringToHash( "PlayerIdle" ) );
					else
					_animator.Play( Animator.StringToHash( "PlayerIdleAxe" ) );
			}

		} else {

			_animator.Play ( Animator.StringToHash("PlayerAttacking"));
			attackTimer--;
			if (attackTimer <= 0)
				attacking = false;

		}

		if (attacking && Mathf.Abs (this.transform.position.x - theBear.transform.position.x) < 50 && BearUpdate.getFallen ()) {

			BearUpdate.kill ();

		}

		// we can only jump whilst grounded
		if (!attacking)
		if( _controller.isGrounded && (Input.GetKeyDown( KeyCode.UpArrow ) ||  Input.GetKey( KeyCode.W )) )
		{
			_velocity.y = Mathf.Sqrt( 2f * jumpHeight * -gravity );
			//_animator.Play( Animator.StringToHash( "Jump" ) );
		}
		
		
		// apply horizontal speed smoothing it
		var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
		_velocity.x = Mathf.Lerp( _velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor );
		
		// apply gravity before moving
		_velocity.y += gravity * Time.deltaTime;
		
		_controller.move( _velocity * Time.deltaTime );
	}
	
}
