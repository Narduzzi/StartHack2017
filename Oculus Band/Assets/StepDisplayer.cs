using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMLDataModel;

public class StepDisplayer : MonoBehaviour {

	//public List<Note> notes;
	// Use this for initialization
	public GameObject step;
	private Vector3 initialPos;
	private Quaternion initialRot;
	void Start () {
		initialPos = step.transform.position;
		initialRot = step.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			PushNote (0);
		}
	}

	void PushNote(int offset){
		GameObject newstep = GameObject.Instantiate (step,initialPos,initialRot);
		newstep.transform.parent = this.transform;
	}
}
