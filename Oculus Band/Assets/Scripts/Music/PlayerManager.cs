using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
	public AudioManager audiomanager;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.V)) {
			Debug.Log ("Failed voice");
			audiomanager.Failed ("Voice");
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			Debug.Log ("Failed drums");
			audiomanager.Failed ("Drums");
		}
		if (Input.GetKeyDown (KeyCode.L)) {
			Debug.Log ("Failed piano");
			audiomanager.Failed ("Piano");
		}
	}
}
