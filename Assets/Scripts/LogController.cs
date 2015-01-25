using UnityEngine;
using System.Collections;

public class LogController : MonoBehaviour {

	public static GameObject[] logs;
	private static int logsLeft = 3;

	public static void destroyLog() {
		
		logsLeft--;
		(logs [logsLeft].GetComponent<Animator> ()).speed = 1;

	}

}
