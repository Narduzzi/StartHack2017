using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsUpDetector : MonoBehaviour {
	public AudioSource crowd_source;
	public GameObject Head;
	public GameObject HandL;
	public GameObject HandR;

    public NetworkParameters parameters;
    public SyncManager syncManager;

	public float maxDistance;
	private float maxHeight;
	private float volume;
	private float totalDistance;

    private float startTime = -1.0f;

	// Use this for initialization
	void Start () {
        if (parameters == null) {
            parameters = GameObject.Find("Params").GetComponent<NetworkParameters>() ;
        }
        if (parameters!= null) {
            Debug.Log("Found parameters");
            Debug.Log(parameters.getHands());
            if (parameters.getHands().Equals("LeapMotion")) {
                Debug.Log("Found Parameters(Leap Motion)");
                maxDistance = maxDistance / 2.0f;
            }
        }
		if (crowd_source == null) {
			Debug.Log ("No source found for crowd");
			this.gameObject.SetActive (false);
		}
		if (Head == null) {
			Head = GameObject.FindGameObjectWithTag ("MainCamera");
			if (Head == null) {
				Debug.LogError ("No Head found");
			}
			Vector3 posHead = Head.transform.position;
			maxHeight = posHead.y + maxDistance;
		}
		FindHands ();

		totalDistance = maxHeight - Head.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if (Head != null) {
            if (HandL == null || HandR == null) {
                FindHands();
            }
			float height = CheckHands ();
			if (height <= 0) {
				volume = 0;

                startTime = -1.0f;
            } else if (height > maxHeight) {
				volume = 1;
			} else {
				volume = height / totalDistance;
				if (!crowd_source.isPlaying) {
					crowd_source.Play ();
				}

                if (!syncManager.play && volume > 0.5f) {
                    if (startTime < 0.0f) {
                        Debug.Log("Settings start");
                        startTime = Time.time;
                    } else {
                        float deltaTime = Time.time - startTime;
                        if (deltaTime > 3.0f) {
                            Debug.Log("YOU DID IT !");
                            syncManager.play = true;
                        }
                    }
                }
            }
			crowd_source.volume = volume;
		}
	}

	/// <summary>
	/// Checks if the hands are present.
	/// </summary>
	/// <returns>The hands.</returns>
	public float CheckHands(){
		float distanceR = 0;
		float distanceL = 0;
		Vector3 posHead = Head.transform.position;

		if (HandR != null) {
			Vector3 posHandsR = HandR.transform.position;
			distanceR = posHandsR.y - posHead.y;
		}
		if (HandL != null) {
			Vector3 posHandsL = HandL.transform.position;
			distanceL = posHandsL.y - posHead.y;
		}
			
		float distance = Mathf.Max (distanceL, distanceR);
		return distance;
	}

	/// <summary>
	/// Finds the hands.
	/// </summary>
	private void FindHands(){
		FindRightHand ();
		FindLeftHand ();
	}

	/// <summary>
	/// Looks for the right hand (Touch or LeapMotion).
	/// </summary>
	private void FindRightHand (){
		GameObject HandR_trans = GameObject.Find("RigidRoundHand_R");
		if(HandR_trans ==null){
            HandR_trans = GameObject.Find ("RigidRoundHand_R (Clone)");
		}
        if (HandR_trans == null && parameters.getHands() == "Touch") {
            HandR_trans = GameObject.Find("RightHandAnchor");
        }
        if (HandR_trans != null){
			HandR = HandR_trans.gameObject;
		}
	}

	/// <summary>
	/// Looks for the left hand (Touch or LeapMotion).
	/// </summary>
	private void FindLeftHand(){
		GameObject HandL_trans = GameObject.Find("RigidRoundHand_L");

        if (HandL_trans == null) {
			HandL_trans = GameObject.Find ("RigidRoundHand_L (Clone)");
		}

        if (HandL_trans == null && parameters.getHands() == "Touch") {
            HandL_trans = GameObject.Find("LeftHandAnchor");
        }

        if (HandL_trans != null) {
			HandL = HandL_trans.gameObject;
		}
	}
}
