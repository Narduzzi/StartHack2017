using System.Collections;
using UnityEngine;
using UnityEditor;

public class AudioManager : MonoBehaviour {
	
	public AudioSource drums_source;
	public AudioSource piano_source;
	public AudioSource voice_source;
	public AudioSource error_source;
	public bool PlayOnAwake;
	public bool waitedSec = true;
	private bool[] failedInstruments = new bool[4];

	void Start ()   
	{
		drums_source.playOnAwake = PlayOnAwake;
		piano_source.playOnAwake = PlayOnAwake;
		voice_source.playOnAwake = PlayOnAwake;
	}        
		

	public void Failed(string instrument){
		failedInstruments [getNumberInstrument (instrument)] = true;
		error_source.Play ();
		AudioSource source = GetAudioSource(instrument);
		source.volume = 0;
		Debug.Log ("Set volume of " + instrument + " to 0");
	}
	public void Success(string instrument){
		failedInstruments [getNumberInstrument (instrument)] = false;
		StartCoroutine("RestoreSound",instrument);
	}

	IEnumerator RestoreSound (string instrument){
		waitedSec = true;
		yield return new WaitForSeconds(0.5f);
		if (waitedSec) {
			SetLevelMax (instrument);
		}
	}

	private void SetLevelMax(string instrument){
		Debug.Log ("Setting level max..");
		AudioSource source = GetAudioSource(instrument);
		if (source == null) {
			Debug.LogError ("Source is null");
		} else {
			Debug.Log ("Set volume of " + instrument + " to 1.0");
			source.volume = 1.0f;
		}
	}

	private int getNumberInstrument(string instrument){
		if (instrument == "Voice") {
			return 1;
		} else if (instrument == "Drums") {
			return 2;
		} else if (instrument == "Piano") {
			return 3;
		} else{
			return 0;
		}
	}
	private AudioSource GetAudioSource(string instrument){
		if (instrument == "Voice") {
			return voice_source;
		} else if (instrument == "Drums") {
			return drums_source;
		} else if (instrument == "Piano") {
			return piano_source;
		} else
			return null;
	}
		

	// Make sure that deathzone has a collider, box, or mesh.. ect..,
	// Make sure to turn "off" collider trigger for your deathzone Area;
	// Make sure That anything that collides into deathzone, is rigidbody;
}
