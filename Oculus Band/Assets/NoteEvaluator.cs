using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteEvaluator : MonoBehaviour {

	public bool entered = false;
	public bool failed = false;
	public GameObject Rails;
	public Color normalColor;
	public Color errorColor;
	private GameObject colliding;
	private GameObject RailL;
	private GameObject RailR;
	private Material RailMaterialL;
	private Material RailMaterialR;

	void Awake(){
		if (Rails == null) {
			Debug.LogError ("No Rails object passed");
		} 
		if (normalColor == null) {
			Debug.LogError ("No normalColor found");
		}
		if (errorColor == null) {
			Debug.LogError ("No errorColor found");
		}

		RailL = Rails.transform.FindChild ("RailLeft").gameObject;
		RailR = Rails.transform.FindChild ("RailRight").gameObject;
		RailMaterialL = RailL.GetComponent<Renderer> ().material;
        if (RailMaterialL == null) {
            Debug.LogError("RailMaterialL is null");
        }
		RailMaterialR = RailR.GetComponent<Renderer> ().material;
        if (RailMaterialR == null) {
            Debug.LogError("RailMaterialR is null");
        }
        RestoreRailColor ();
	}

	void OnTriggerEnter(Collider collider){
        if (collider.gameObject.tag == "Step") {
            colliding = collider.gameObject;
        }
		entered = true;
	}

	void OnTriggerExit(Collider collider){
		//Missed GameObject
		if (colliding != null) {
			FailObject ();
		}

		entered = false;
		failed = true;
		colliding = null;
	}

	public void RestoreState(){
		RestoreRailColor ();
		entered = false;
		failed = false;
	}

	private void RestoreRailColor(){
		StartCoroutine(FadeColor(normalColor,errorColor,1.0f));
	}

	private IEnumerator FadeColor(Color start, Color end, float time){
		float elapsedTime = 0;
		RailMaterialL.color = start;
		RailMaterialR.color = start;
        while (elapsedTime < time){
			RailMaterialL.color = Color.Lerp(start, end, (elapsedTime / time));
			RailMaterialR.color = Color.Lerp(start, end, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
	}

	public void AnimateCollidingObject(){
        //change to green 
        //deactivate collider
        //(and animate destroy)
        if (colliding != null) {
            colliding.GetComponent<StepFaller>().AnimateWin();
            colliding.GetComponent<Collider>().enabled = false;
        }

	}

	public void FailObject(){
        if (colliding != null) {
            colliding.GetComponent<StepFaller>().AnimateFail();
        }
	}
}
