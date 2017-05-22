using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupConcertScene : MonoBehaviour {

	public Transform PianoPlayerTransform;

	public Transform DrumsPlayerTransform;
    public GameObject[] DrumsHands;

	public Transform GuitarPlayerTransform;
    public GuitarTracker guitarTracker;
    public GameObject[] GuitarHands;

	public GameObject CameraPlayerOVR;
	public GameObject CameraPlayerDesktop;
	private GameObject CameraPlayer;
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
		if (CameraPlayerOVR == null) {
			Debug.LogError ("CameraPlayer is null");
		}
		if (CameraPlayerDesktop == null) {
			Debug.LogError ("CameraPlayer is null");
		}
		if (audioManager == null) {
			Debug.LogError ("AudioManager is null");
		}

		instrument = parameters.getInstrument ();
		headset = parameters.getHeadset ();
		hands = parameters.getHands ();

		CheckIfDesktop ();

		Setup (instrument, headset, hands);
	}

	private void CheckIfDesktop(){
		if (parameters.getHeadset () == "Desktop") {
			Vector3 piano_pos = PianoPlayerTransform.position;
			piano_pos.y = piano_pos.y + 0.85f;
			PianoPlayerTransform.position = piano_pos;
			CameraPlayerOVR.SetActive (false);
			CameraPlayerDesktop.SetActive (true);
			CameraPlayer = CameraPlayerDesktop;
			CameraPlayer.GetComponent<Camera> ().fieldOfView = 60;
		} else {
			CameraPlayerOVR.SetActive (true);
			CameraPlayerDesktop.SetActive (false);
			CameraPlayer = CameraPlayerOVR;
		}
	}


	void Setup(string instrument, string headset, string hands){
        Debug.Log("Setting up instrument " + instrument);
		audioManager.myInstrument = instrument;
		if (instrument == "Piano") {
			SetupPlayerLocation (PianoPlayerTransform);
		} else if (instrument == "Guitar") {
			SetupPlayerLocation (GuitarPlayerTransform);
            guitarTracker.gameObject.SetActive(true);
            foreach(GameObject go in GuitarHands) {
                go.SetActive(true);
            }
		} else if (instrument == "Drums") {
			SetupPlayerLocation (DrumsPlayerTransform);
            foreach (GameObject go in DrumsHands) {
                go.SetActive(true);
            }
        } else {
			Debug.LogError ("Instrument \"" + instrument + "\" was not setup in game. No such instrument exists.");
		}

        Debug.Log("Searching for : " + "GameLogicGlow" + instrument);
        this.GetComponent<GameInitializer>().gameLogic = GameObject.Find("GameLogicGlow" + instrument);
        this.GetComponent<GameInitializer>().RequiredPlayers = parameters.getNumberOfPlayers();

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
