using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPressedAnimator : MonoBehaviour {
	bool keyPressed = false;
	public float restoreTime = 0.0001f;
	public Vector3 downPosition;
	public Vector3 downRotation;

	private Vector3 originalPosition;
	private Quaternion originalRotation;
	// Use this for initialization
	void Start () {
		this.originalPosition = this.transform.localPosition;
		this.originalRotation = this.transform.rotation;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.transform.parent != this.transform.parent){
			PressKey ();
		}
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.transform.parent != this.transform.parent) {
			ReleaseKey ();
		}
	}

	void PressKey(){
		if (keyPressed == false) {
			Debug.Log ("keypressed");
			keyPressed = true;
			AnimateDown ();
		}
	}

	void ReleaseKey(){
		if (keyPressed) {
			Debug.Log ("keyreleased");
			keyPressed = false;
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
