using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentTester : MonoBehaviour {

	public AudioManager audioManager;
	// Use this for initialization
	void Start () {
		if (audioManager == null) {
			Debug.LogError ("AudioManager is null");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.G)) {
			audioManager.Failed ("Guitar");
		}
		if (Input.GetKeyDown (KeyCode.P)) {
			audioManager.Failed ("Piano");
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			audioManager.Failed ("Drums");
		}
		if (Input.GetKeyDown (KeyCode.E)) {
			audioManager.Success ("Drums");
		}
		if (Input.GetKeyDown (KeyCode.T)) {
			audioManager.Success ("Guitar");
		}
		if (Input.GetKeyDown (KeyCode.L)) {
			audioManager.Success ("Piano");
		}
	}
}
