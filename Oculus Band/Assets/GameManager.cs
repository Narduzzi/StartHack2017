using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMLDataModel;

public class GameManager : MonoBehaviour {

	public string name;
	private List<List<Note>> notesArray = new List<List<Note>>();
	private float time;
	public List<KeyCode> keys;
	public float toleranceBeforeMs = 0.15f;
	public float toleranceAfterMs = 0.15f;
	private bool valid = true;
	private List<Note> notes;
	public string instrument;
	public AudioManager audioManager;
	// Use this for initialization
	void Start () {
		Song song = Song.Load (name);
		notes = song.notes;
		if (notes == null || notes.Count < 2) {
			throw new MissingComponentException ("No song found");
		}
		notesArray = new List<List<Note>> ();
		notesArray = StepReader.GenerateListByChannel(notesArray,keys,notes);
		time = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		for (int i = 0; i < keys.Count; i++){
			if (Input.GetKeyDown (keys [i])) {
				float perfectTime = notesArray[i][0].time;
				if (time <= perfectTime + toleranceAfterMs && time >= perfectTime - toleranceBeforeMs) {
					valid = true;
				}
			}

			if (notesArray [i] [0].time < time) {
				valid = false;
			}
		}
		if (!valid) {
			audioManager.Failed (instrument);
		} else {
			audioManager.Success (instrument);
			Debug.Log ("Success! " + instrument);
		}

		notesArray = ReSyncList (notesArray,time);
	}

	private List<List<Note>> ReSyncList(List<List<Note>> notesArray,float time){
		for (int i = 0; i < notesArray.Count; i++) {
			for (int j = 0; j < notesArray [i].Count; j++) {
				Note note = notesArray [i] [j];
				if (note.time < time)
					notesArray [i].Remove (note);
				}
			}
		return notesArray;
	}
}
