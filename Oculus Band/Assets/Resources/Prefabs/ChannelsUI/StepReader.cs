using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMLDataModel;

public class StepReader : MonoBehaviour {

	public string name;
	private List<Note> notes;
	private float time;
	private List<List<Note>> notesArray = new List<List<Note>>();

	public List<KeyCode> listKeys;	
	public float offset = 0.2f;
	public float time_offset = 4.0f;
	// Use this for initialization
	void Start () {
		Song song = Song.Load (name);
		notes = song.notes;
		time = time_offset;
		notesArray = new List<List<Note>> ();
		notesArray = GenerateListByChannel(notesArray,listKeys,notes);
		Debug.Log (this.transform.parent.transform.localRotation);
		this.transform.localRotation = this.transform.parent.transform.localRotation;
		this.transform.localScale = this.transform.parent.localScale;
	}

	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		for (int i = 0; i < listKeys.Count; i++) {
			Note note = PopAndRemove (notesArray [i], time);
			if (note != null) {
				this.GetComponent<ChannelsManager> ().PushNote (i, offset);;
			}
		}
	}


	public static List<List<Note>> GenerateListByChannel(List<List<Note>> notesArray, List<KeyCode> listKeys, List<Note> notes){
		for (int i = 0; i < listKeys.Count; i++) {
			notesArray.Add (new List<Note> ());
		}
		for (int i = 0; i < notes.Count; i++) {
			Note noteI = notes[i];
			notesArray[(int)noteI.type].Add (noteI);
		}
		return notesArray;
	}

	Note PopAndRemove(List<Note> list,float time){
		if (list.Count != 0) {
			int length = list.Count;
			Note note = list [0];
			if (note.time < time) {
				list.Remove (note);
				if (length <= list.Count) {
					throw new UnityException ("item not removed from list");
				}
				return note;
			}
		}
		return null;
	}
}
