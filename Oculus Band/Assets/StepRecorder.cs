using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMLDataModel;

public class StepRecorder : MonoBehaviour {

	public AudioSource source;
	public string name;
	private Song song;
	private List<Note> notes;
	private float time;

	public List<KeyCode> listKeys;	

	// Use this for initialization
	void Start () {
		notes = new List<Note> ();
		time = source.time;
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < listKeys.Count; i++) {
			if (Input.GetKeyDown (listKeys[i])) {
				Note newNote = new Note (source.time, 1);
				notes.Add (newNote);
			}
		}

		if (source.time > source.clip.length/10.0) {
			Debug.Log ("Finished!");
			song = new Song (name, notes);
			//song.Save ();
		}
	}
}
