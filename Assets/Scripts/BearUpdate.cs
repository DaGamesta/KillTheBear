using UnityEngine;
using System.Collections;

public class BearUpdate : MonoBehaviour {
	
	private Animator _animator;

	// Use this for initialization
	void Awake () {
		
		_animator = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
		
		_animator.Play (Animator.StringToHash ("Run"));

	}
}
