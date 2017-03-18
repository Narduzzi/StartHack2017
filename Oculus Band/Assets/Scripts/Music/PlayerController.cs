using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public GameObject manager;
	private AudioManager audioManager;
	// Use this for initialization
	void Start () {
		if (manager == null) {
			throw new MissingComponentException ("No Audio Manager found");
		} else {
			audioManager = manager.GetComponent<AudioManager> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.V)){
			audioManager.Failed("Voice");
		}
		if(Input.GetKeyDown(KeyCode.D)){
			audioManager.Failed("Drums");
		}
	}
}
