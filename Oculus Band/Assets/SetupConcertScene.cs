using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupConcertScene : MonoBehaviour {

	public Transform PianoPlayerTransform;
	public Transform DrumsPlayerTransform;
	public Transform GuitarPlayerTransform;
	public GameObject CameraPlayer;
	public AudioManager audioManager;
	private NetworkParameters parameters;
	private string instrument;
	private string headset;
	private string hands;
	private bool isMultiplayer = true;
	// Use this for initialization
	void Awake () {
		GameObject ParamsGO = GameObject.Find ("Params");
		if (ParamsGO == null) {
			Debug.LogError ("No \"Params\" GameObject found in scene");
		}

		parameters = ParamsGO.GetComponent<NetworkParameters> ();
		if (parameters == null) {
			Debug.LogError ("No script \"NetworkParameters\" attached to GameObject \"Params\"");
		}

		//Checking transforms
		if (PianoPlayerTransform == null) {
			Debug.LogError ("PianoPlayerTransform is null");
		}
		if (GuitarPlayerTransform == null) {
			Debug.LogError ("GuitarPlayerTransform is null");
		}
		if (DrumsPlayerTransform == null) {
			Debug.LogError ("DrumsPlayerTransform is null");
		}
		if (CameraPlayer == null) {
			Debug.LogError ("CameraPlayer is null");
		}
		if (audioManager == null) {
			Debug.LogError ("AudioManager is null");
		}

		instrument = parameters.getInstrument ();
		headset = parameters.getHeadset ();
		hands = parameters.getHands ();
		Setup (instrument, headset, hands);
	}



	void Setup(string instrument, string headset, string hands){
		audioManager.myInstrument = instrument;
		this.GetComponent<GameInitializer> ().gameLogic = GameObject.Find ("GameLogicGlow" + instrument);
		if (instrument == "Piano") {
			SetupPlayerLocation (PianoPlayerTransform);
		} else if (instrument == "Guitar") {
			SetupPlayerLocation (GuitarPlayerTransform);
		} else if (instrument == "Drums") {
			SetupPlayerLocation (DrumsPlayerTransform);
		} else {
			Debug.LogError ("Instrument \"" + instrument + "\" was not setup in game. No such instrument exists.");
		}


		//Parameters for headset and hands
	}
		
	void SetupPlayerLocation(Transform transform){
		CameraPlayer.transform.position = transform.position;
		CameraPlayer.transform.rotation = transform.rotation;
		CameraPlayer.transform.parent = transform.parent;
	}
		
	void SetupHeadSet(){
		Debug.LogWarning ("SetupHeadSet method is not implemented");
	}

	void SetupHands(){
		Debug.LogWarning ("SetupHands method is not implemented");
	}
}
