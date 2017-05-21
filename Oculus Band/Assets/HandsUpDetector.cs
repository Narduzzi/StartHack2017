﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsUpDetector : MonoBehaviour {
	public AudioSource crowd_source;
	public GameObject Head;
	public GameObject HandL;
	public GameObject HandR;

	public float maxDistance;
	private float maxHeight;
	private float volume;
	private float totalDistance;
	// Use this for initialization
	void Start () {
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
			if (HandL == null) {
				FindLeftHand ();
			}
			if (HandR == null) {
				FindRightHand ();
			}
			float height = CheckHands ();
			if (height <= 0) {
				volume = 0;
			} else if (height > maxHeight) {
				volume = 1;
			} else {
				volume = height / totalDistance;
				if (!crowd_source.isPlaying) {
					crowd_source.Play ();
				}
			}
			crowd_source.volume = volume;
		}
	}


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

	private void FindHands(){
		FindRightHand ();
		FindLeftHand ();
	}

	private void FindRightHand (){
		GameObject HandR_trans = GameObject.Find ("RightHandAnchor");
		if(HandR_trans ==null){
			HandR_trans = GameObject.Find ("RigidRoundHand_R");
		}
		if(HandR_trans ==null){
			HandR_trans = GameObject.Find ("RigidRoundHand_R (Clone)");
		}
		if(HandR_trans != null){
			HandR = HandR_trans.gameObject;
		}
	}

	private void FindLeftHand(){
		GameObject HandL_trans = GameObject.Find ("LeftHandAnchor");
		if (HandL_trans == null) {
			HandL_trans = GameObject.Find ("RigidRoundHand_L");
		}
		if (HandL_trans == null) {
			HandL_trans = GameObject.Find ("RigidRoundHand_L (Clone)");
		}
		if (HandL_trans != null) {
			HandL = HandL_trans.gameObject;
		}
	}
}
