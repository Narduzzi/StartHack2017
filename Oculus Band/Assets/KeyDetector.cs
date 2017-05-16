using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDetector : MonoBehaviour {

	public string noteName;
	private float waitingTime = 0.3f;
	private Collider collider;
	public GameObject CollidingObject;
	// Use this for initialization
	void Start () {
		if (noteName == null) {
			noteName = ""+Random.Range (100, 200);
		}

		collider = this.transform.GetComponent<Collider> ();
		if (collider == null) {
			Debug.LogError ("Missing collider on gameobject : " + gameObject.name);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter(Collider col){

		if (col.gameObject.name == CollidingObject.transform.name) {
			Debug.Log (col.gameObject.name + " Collide with" + this.transform.name);


			col.enabled = false;
			//col.enabled = false;
			//StartCoroutine (EnableCollider (col,waitingTime));
			StartCoroutine (EnableCollider (col, waitingTime));
		} 
	}

	private IEnumerator EnableCollider(Collider collider, float waitingTime){
		yield return new WaitForSeconds (waitingTime);
		collider.enabled = true;
	}

	public void OnTriggerExit(Collider col){
		col.enabled = true;
	}
}
