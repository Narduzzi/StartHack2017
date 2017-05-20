using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPressedAnimator : MonoBehaviour {
	bool keyPressed = false;
	public float restoreTime;
	public Vector3 downPosition;
	public Vector3 downRotation;

	private Vector3 originalPosition;
	private Quaternion originalRotation;
	private float releaseNoteAfter = 0.4f;
	private float counter = 0.0f;

	public InstrumentManager instrumentManager;
	public int type = -1;

	// Use this for initialization
	void Start () {
		this.originalPosition = this.transform.localPosition;
		this.originalRotation = this.transform.rotation;

	}
	
	// Update is called once per frame
	void Update () {
		counter = counter + Time.deltaTime;
		if (keyPressed && counter > releaseNoteAfter) {
			ReleaseKey ();
			counter = 0.0f;
		}
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "bone3"){
			PressKey ();
		}
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "bone3") {
			ReleaseKey ();
		}
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "bone3") {
			PressKey ();
		}
	}

	void OnCollisionStay(Collision col){
		if (col.gameObject.tag == "bone3") {
			counter = 0.0f;
			Debug.Log ("Pressed");
		}
	}


	void OnCollisionExit(Collision col){
		if (col.gameObject.tag == "bone3") {
			ReleaseKey ();
		}
	}

	void PressKey(){
		if (keyPressed == false) {
			keyPressed = true;
			instrumentManager.PressKey(type);
			AnimateDown ();
		}
	}

	void ReleaseKey(){
		if (keyPressed) {
			keyPressed = false;
			instrumentManager.UnpressKey(type);
			AnimateUp ();
		}
	}

	void AnimateDown(){
		this.GetComponent<Collider> ().enabled = false;
		StartCoroutine (RestoreCollider (restoreTime));
		this.transform.localPosition = downPosition;
		this.transform.rotation = Quaternion.Euler (downRotation);
	}

	void AnimateUp(){
		
		this.transform.localPosition = originalPosition;
		this.transform.rotation = originalRotation;
	}

	IEnumerator RestoreCollider(float waitingTime){
		yield return new WaitForSeconds (waitingTime);
		this.GetComponent<Collider> ().enabled = true;
	}
}
