using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMLDataModel;

public class StepRecorder : MonoBehaviour {

	public AudioSource source;
	public string songName;
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
			if (Input.GetKeyDown (listKeys [i])) {
				Note newNote = new Note (source.time, (uint)i);
				notes.Add (newNote);
			}
		}

		if (Input.GetKeyDown(KeyCode.Space) || source.time > source.clip.length) {
			song = new Song (songName, notes);
			song.Save ();
			Debug.Log ("Song saved : " + songName);
		}
	}
}
