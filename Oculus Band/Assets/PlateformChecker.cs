using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using Leap;

public class PlateformChecker : MonoBehaviour {

	public GameObject OculusParent;
	public GameObject DesktopParent;
    public GameObject LeapMotionVR;
	public GameObject SelectInstrument;

	private GameObject Guitar_pos;
	private GameObject Piano_pos;
	private GameObject Drums_pos;
	public NetworkParameters parameters;

	void Awake(){
		if (VRDevice.isPresent) {
			Debug.Log ("PRESENT");
			OculusParent.SetActive (true);
			DesktopParent.SetActive (false);
			parameters.SetHeadSet ("Oculus");
         
		} else {
			Debug.Log ("NO VR");
			OculusParent.SetActive (false);
			DesktopParent.SetActive (true);
			FindPositions ();
			LocateSelectInstrument ();
			parameters.SetHeadSet ("Desktop");
			parameters.SetHands ("LeapMotion");
		}
	}
	// Use this for initialization
	void Start () {
        Controller controller = new Controller();
        if (controller.IsConnected && VRDevice.isPresent) {
            print("Leap motion is connected");
            parameters.SetHands("LeapMotion");
            SetOculusLeapMotion(true);
        }else {
            SetOculusLeapMotion(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	void FindPositions(){
		Guitar_pos = GameObject.Find ("Guitar_pos");
		Piano_pos =  GameObject.Find ("Piano_pos");
		Drums_pos =  GameObject.Find ("Drums_pos");
	}

	void LocateSelectInstrument(){
		SelectInstrument.transform.FindChild ("Drums").position = Drums_pos.transform.position;
		SelectInstrument.transform.FindChild ("Piano").position = Piano_pos.transform.position;
		SelectInstrument.transform.FindChild ("Guitar").position = Guitar_pos.transform.position;
	}

    private void SetOculusLeapMotion(bool val) {
        if (val) {
            LeapMotionVR.SetActive(true);
            parameters.SetHands("LeapMotion");
        }
    }
}
