using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteEvaluator : MonoBehaviour {

	public bool entered = false;
	public bool played = false;
	public bool failed = false;
	private GameObject colliding;

	void OnTriggerEnter(Collider collider){
		Debug.Log ("Trigger Entered");
		colliding = collider.gameObject;
		entered = true;
	}

	void OnTriggerExit(Collider collider){
		//Missed GameObject
		if (colliding != null) {
			colliding.GetComponent<Renderer> ().material.color = Color.red;
		}

		entered = false;
		failed = true;
		colliding = null;
	}

	public void RestoreState(){
		entered = false;
		failed = false;
	}
	public void PlayNote(){
		played = true;
	}

	public void AnimateCollidingObject(){
		//change to green 
		//deactivate collider
		//(and animate destroy)
		colliding.GetComponent<Renderer> ().material.color = Color.cyan;
		colliding.GetComponent<Collider> ().enabled = false;

	}
}
