using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AttachToCamera : NetworkBehaviour {

	private bool created = false;

	// Update is called once per frame
	void Update () {
		if (!created) {
			if (isLocalPlayer) {
				Debug.Log ("Created LocalPlayer");
				GameObject MainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
				this.transform.position = MainCamera.transform.position;
				this.transform.rotation = MainCamera.transform.rotation;
				this.transform.parent = MainCamera.transform;
                created = true;
			} else {
				Debug.Log ("Not local player");
			}
		}
	}
}
