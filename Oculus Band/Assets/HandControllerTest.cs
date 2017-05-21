using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandControllerTest : MonoBehaviour {
	public GameObject Hand;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = Hand.transform.position;
		if (Input.GetKey (KeyCode.UpArrow)) {
			pos.y = pos.y + 0.05f;
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			pos.y = pos.y - 0.05f;
		
		}
		Hand.transform.position = pos;

	}
}
