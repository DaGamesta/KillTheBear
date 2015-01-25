using UnityEngine;
using System.Collections;

public class LogController : MonoBehaviour {

	public static GameObject[] logsStatic;
	public GameObject[] logs;
	private static int logsLeft = 3;
	public AudioClip soundBashLogs;
	public AudioClip soundBreakLogs;
	public static AudioClip soundBashLogsStatic;
	public static AudioClip soundBreakLogsStatic;

	void Awake() {

		logsStatic = logs;
		logsLeft = 3;
		(logsStatic [2].GetComponent<Animator> ()).StopPlayback ();
		(logsStatic [1].GetComponent<Animator> ()).StopPlayback ();
		(logsStatic [0].GetComponent<Animator> ()).StopPlayback ();
		soundBashLogsStatic = soundBashLogs;
		soundBreakLogsStatic = soundBreakLogs;

	}

	public static void destroyLog() {

		logsLeft--;
		if (logsLeft >= 0) {

			(logsStatic [logsLeft].GetComponent<Animator> ()).Play (Animator.StringToHash ("SaggingLog"));
			UpdatePlayer._audio.PlayOneShot(soundBashLogsStatic);

		}

	}

	public static void bearWalkOver() {

		if (logsLeft <= 0 && !AxeLogUpdate.getActive()) {

			Destroy (logsStatic [0]);
			Destroy (logsStatic [1]);
			Destroy (logsStatic [2]);
			BearUpdate.fallDown();
			AxeLogUpdate.activate();
			UpdatePlayer._audio.PlayOneShot(soundBreakLogsStatic);

		}

	}

}
