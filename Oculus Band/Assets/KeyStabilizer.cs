using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyStabilizer : MonoBehaviour {
	public float MinRotX = -3.5f;
	public float MaxRotX = 0.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		checkRotation ();
	}

	public void checkRotation(){
		Quaternion rotation = this.transform.rotation;
		Vector3 rot3 = rotation.eulerAngles;
		if (360 - rot3.x >= MaxRotX) {
			rot3.x = MaxRotX;
		}
		if (360 - rot3.x <= MinRotX) {
			rot3.x = MinRotX;
		}
	}
}
