using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SyncManager : NetworkBehaviour {
	[SyncVar]
	public bool play = false;
	public float WaitForSyncDelay = 0.05f;
	private float delay = 0.0f;

	void Start(){
		if (isServer) {
			delay = WaitForSyncDelay;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Space)) {
            Debug.Log("PLAY!");
			play = true;
		}
	}

	public float GetDelay(){
		return delay;
	}
}
