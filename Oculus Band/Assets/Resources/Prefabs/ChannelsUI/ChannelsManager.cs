using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMLDataModel;

public class ChannelsManager : MonoBehaviour {
	public int numberOfchannels;
	public float spacing = 0.02f;
	private List<KeyCode> keys ;
	public GameObject originalChannel;
	private List<GameObject> channelsGO;

	// Use this for initialization
	void Start () {
		StepRecorder rec = this.GetComponent < StepRecorder> ();
		if (rec != null) {
			keys = rec.listKeys;
		}
		StepReader reader = this.GetComponent<StepReader> ();
		if (reader != null) {
			keys = reader.listKeys;
			numberOfchannels = reader.listKeys.Count;
		}
	
		channelsGO = new List<GameObject> ();
		createChannels (keys);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void createChannels(List<KeyCode> keys){

		float alternate_pos = 0.0f;
		float spacing_dec = 0.0f;

		for (int i = 0; i < numberOfchannels; i++) {
			Debug.Log ("CREATING " + i + "CHANNELS");
			GameObject channel = GameObject.Instantiate (originalChannel);
			channel.GetComponent<StepDisplayer> ().SetKeycode (keys [i]);

			// TO REWRITE
			Vector3 pos = this.transform.position;
		
			if (i % 2 == 1) {
				spacing_dec = spacing_dec + spacing * 2;
				alternate_pos = -1;
			} else {
				alternate_pos = 1;
			}

			pos.x = pos.x + alternate_pos * spacing_dec;
			channel.transform.localPosition = pos;

			Debug.Log (i + " : " + pos);
			//channel.transform.localScale = this.transform.localScale;

			channel.transform.parent = this.transform;
			channel.transform.gameObject.name = "Channel_" + i;
			channel.SetActive (true);
			channelsGO.Add (channel);
		}
		originalChannel.SetActive (false);
	}

	public bool CheckChannel (int index){
		NoteEvaluator noteEvaluator = channelsGO[index].GetComponentInChildren<NoteEvaluator>();
		if (noteEvaluator == null) {
			Debug.LogError ("No NoteEvalutator found in children");
		}
		//Check if note in OK collider
		bool valid = noteEvaluator.entered;
		//if yes
		if (valid) {
			//Animate colliding gameObject
			noteEvaluator.RestoreState ();
			noteEvaluator.AnimateCollidingObject ();
		
			return true;

		} else {
			return false;
		}
	}
	public void RestoreAllChannels(){
		foreach (GameObject channel in channelsGO) {
			NoteEvaluator noteEvaluator = channel.GetComponentInChildren<NoteEvaluator> ();
			noteEvaluator.RestoreState ();
		}
	}

	public bool CheckAllChannels(){
		bool valid = true;
		//check all ErrorColliders
		foreach(GameObject channel in channelsGO){
			NoteEvaluator noteEvaluator = channel.GetComponentInChildren<NoteEvaluator>();
			if (noteEvaluator == null) {
				Debug.LogError ("No NoteEvalutator found in children");
			}

			//if one is triggered 
			if (noteEvaluator.failed) {
				valid = false;
			}
		}
		return valid;
	}

	public void PushNote(int index, float offset){
		channelsGO [index].GetComponent<StepDisplayer> ().PushNote (offset);
	}
}
