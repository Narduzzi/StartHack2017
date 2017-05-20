using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class AudioManager : NetworkBehaviour {

	[SyncVar(hook = "DrumsVar")]
	private bool drums_enabled = true;
	[SyncVar(hook = "PianoVar")]
	private bool piano_enabled = true;
	[SyncVar(hook = "GuitarVar")]
	private bool guitar_enabled = true;

	public AudioSource drums_source;
	public AudioSource piano_source;
	public AudioSource guitar_source;
	public AudioSource voice_source;
	public AudioSource error_source;
	public string myInstrument;
	public bool PlayOnAwake;
	public float delay;
	public bool waitedSec = true;
	private bool[] failedInstruments = new bool[5];

	public bool play = false;
	private bool launched = false;


	void Start ()   
	{
		drums_source.playOnAwake = PlayOnAwake;
		piano_source.playOnAwake = PlayOnAwake;
		voice_source.playOnAwake = PlayOnAwake;
		//StartCoroutine ("Launch");
	}        

	IEnumerator Launch(){
		yield return new WaitForSeconds(delay);
		drums_source.Play ();
		piano_source.Play();
		voice_source.Play ();
	}

	void Update(){
		if (play && !launched) {
			launched = true;
			StartCoroutine ("Launch");
		}
	}

	[Command]
	public void CmdFailedDistant(string instrument){
		if (instrument == "Guitar") {
			guitar_enabled = false;
		}
		if (instrument == "Piano") {
			piano_enabled = false;
		}
		if (instrument == "Drums") {
			drums_enabled = false;
		}
	}

	public void FailedLocal(string instrument){
		if (failedInstruments [getNumberInstrument (instrument)] == false) {
			failedInstruments [getNumberInstrument (instrument)] = true;
			error_source.Play ();
			AudioSource source = GetAudioSource (instrument);
			source.volume = 0;
			Debug.Log ("Set volume of " + instrument + " to 0");
		}
	}

	public void FailedByDistantInstrument(string instrument){
		if (instrument == myInstrument) {
			return;
		}
		if (failedInstruments [getNumberInstrument (instrument)] == false) {
			failedInstruments [getNumberInstrument (instrument)] = true;

			AudioSource source = GetAudioSource (instrument);
			source.volume = 0;
			Debug.Log ("Set volume of " + instrument + " to 0");
		}
	}

	public void Failed(string instrument){
		CmdFailedDistant (instrument);
		FailedLocal (instrument);
	}

	public void Success(string instrument){
		CmdSuccessDistant (instrument);
		SuccessLocal (instrument);
	}

	[Command]
	public void CmdSuccessDistant(string instrument){
		if (instrument == "Guitar") {
			guitar_enabled = true;
		}
		if (instrument == "Piano") {
			piano_enabled = true;
		}
		if (instrument == "Drums") {
			drums_enabled = true;
		}
	}

	public void SuccessLocal(string instrument){
		failedInstruments [getNumberInstrument (instrument)] = false;
		StartCoroutine("RestoreSound",instrument);
	}

	public void SuccessByDistanceInstrument(string instrument){
		if (instrument == myInstrument) {
			return;
		}
		failedInstruments [getNumberInstrument (instrument)] = false;
		StartCoroutine("RestoreSound",instrument);
	}

	IEnumerator RestoreSound (string instrument){
		waitedSec = true;
		yield return new WaitForSeconds(0.3f);
		if (waitedSec) {
			SetLevelMax (instrument);
		}
	}

	private void GuitarVar(bool enabled){
		if (enabled) {
			SuccessByDistanceInstrument ("Guitar");
		} else {
			FailedByDistantInstrument ("Guitar");
		}
	}

	private void PianoVar(bool enabled){
		if (enabled) {
			SuccessByDistanceInstrument ("Piano");
		} else {
			FailedByDistantInstrument ("Piano");
		}
	}

	private void DrumsVar(bool enabled){
		if (enabled) {
			SuccessByDistanceInstrument ("Drums");
		} else {
			FailedByDistantInstrument ("Drums");
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

		failedInstruments [getNumberInstrument (instrument)] = false;
	}


	private int getNumberInstrument(string instrument){
		if (instrument == "Voice") {
			return 1;
		} else if (instrument == "Drums") {
			return 2;
		} else if (instrument == "Piano") {
			return 3;
		} else if (instrument == "Guitar") {
			return 4;
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
		} else if (instrument == "Guitar") {
			return piano_source;
		} else
			return null;
	}
		

	// Make sure that deathzone has a collider, box, or mesh.. ect..,
	// Make sure to turn "off" collider trigger for your deathzone Area;
	// Make sure That anything that collides into deathzone, is rigidbody;
}
