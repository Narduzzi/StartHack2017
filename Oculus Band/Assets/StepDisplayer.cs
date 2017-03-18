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
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (keycode)) {
			print ("Pressed " + keycode);
			PushNote (0);
		}
	}

	void PushNote(int offset){
		GameObject newstep = Instantiate(Resources.Load("Prefabs/Step")) as GameObject; 
		if (newstep == null) {
			Debug.Log ("NO PREFAB");
		}
		newstep.transform.parent = this.transform;
		newstep.GetComponent<StepFaller> ().SetParent (this.transform);
	}

	public void SetKeycode(KeyCode k){
		this.keycode = k;
	}
}
