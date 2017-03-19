using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMLDataModel;

public class StepDisplayer : MonoBehaviour {

	//public List<Note> notes;
	// Use this for initialization

	private Vector3 initialPos;
	private Quaternion initialRot;
	public KeyCode keycode;
	public GameObject originalStep;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (keycode)) {
			print ("Pressed " + keycode);
			PushNote (null, 0);
		}
	}

	public void PushNote(Note note, float offset){
		//GameObject newstep = Instantiate(Resources.Load("Prefabs/Step")) as GameObject;
		GameObject newstep = Instantiate(originalStep) as GameObject;
		note.instance = newstep;
		newstep.GetComponent<StepFaller> ().original = false;
		newstep.transform.parent = this.transform;
		newstep.GetComponent<StepFaller> ().SetParent (this.transform);
	}

	public void SetKeycode(KeyCode k){
		this.keycode = k;
	}
}
