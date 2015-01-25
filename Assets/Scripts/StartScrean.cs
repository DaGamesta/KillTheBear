using UnityEngine;
using System.Collections;

public class StartScrean : MonoBehaviour 
{
	int timer = 4 * 60;

	void Update() {

		timer--;
		if (timer <= 0 || Input.GetKey( KeyCode.Return ))
			Application.LoadLevel("Level1");

	}

}
