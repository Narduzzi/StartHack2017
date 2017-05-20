using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMLDataModel;

public class StepDisplayer : MonoBehaviour {

	//public List<Note> notes;
	// Use this for initialization

	public KeyCode keycode;
	public GameObject originalStep;

	public StepReader stepReader;
	private GameObject original_parent;
	public bool recording_mode = false;

	void Start () {
		if (stepReader == null) {
			throw new MissingComponentException ("Parent is null");
		}
		original_parent = stepReader.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (keycode)) {
			print ("Pressed " + keycode);
			if(recording_mode){
				PushNote (0);
			}
		}
	}

	public void PushNote(float offset){
		//GameObject newstep = Instantiate(Resources.Load("Prefabs/Step")) as GameObject;
		GameObject newstep = Instantiate(originalStep) as GameObject;
		Transform StepReader = this.transform.parent;

		Vector3 parent_scale = original_parent.transform.localScale;
		Vector3 parent_rotation = original_parent.transform.localRotation.eulerAngles;


		newstep.transform.localScale = new Vector3 (2.0f*parent_scale.x/100.0f, 0.1f*parent_scale.y/100.0f, 2.0f*parent_scale.z/100.0f);
		newstep.transform.localRotation = Quaternion.Euler (new Vector3 (parent_rotation.x + 90, parent_rotation.y, parent_rotation.z));
		newstep.transform.parent = this.transform;
		newstep.transform.localPosition = new Vector3 (0.0f, offset, 0.0f);
		//newstep.transform.lossyScale = this.transform.lossyScale;
		//print (newstep.transform.position);
		//newstep.transform.parent = this.transform;
		//newstep.transform.localPosition = this.transform.position;
		/*
		Vector3 pos = newstep.transform.position;
		pos.x -= offset;
		newstep.transform.position = pos;
		newstep.transform.rotation = parent.transform.rotation;

        newstep.GetComponent<StepFaller> ().original = false;
		newstep.transform.parent = parent.transform;
		newstep.GetComponent<StepFaller> ().SetPosition (newstep.transform.position);
		*/
    }

    public void SetKeycode(KeyCode k){
		this.keycode = k;
	}
}
