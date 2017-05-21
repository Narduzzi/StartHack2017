using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepFaller : MonoBehaviour {
	public float speed = 2.0f;
	private float createdTime = 0f;
	public float MaxTime = 20.0f;
	public bool original = true;
	public Color winColor = Color.green;
	public Color failColor = Color.red;
	private GameObject around;
	private bool finished = false;
	// Use this for initialization
	void Start () {
		createdTime = 0.0f;
		around = this.transform.FindChild ("Around").gameObject;
	}
	
	// Update is called once per frame
	void Update () {

		float newY = this.transform.localPosition.y - speed * Time.deltaTime;
		float newZ = this.transform.localPosition.z;

		if (finished) {
			newZ = this.transform.localPosition.z - speed * Time.deltaTime;
		}

		Vector3 pos = this.transform.localPosition;
		pos.y = newY;
		pos.z = newZ;
		this.transform.localPosition = pos;
		createdTime += Time.deltaTime;
		if (createdTime > MaxTime) {
			destroySelf ();
		}
	}

	void destroySelf(){
		if (!original) {
			GameObject.Destroy (this.transform.gameObject);
		}
	}

	public void SetPosition(Vector3 position){
		Vector3 spawn_pos = position;
		Vector3 thisPos = this.transform.position;
		this.transform.position = spawn_pos;
		//this.transform.parent = parent;
	}

	public void AnimateWin(){
		around.GetComponent<Renderer> ().material.color = winColor;
		StartCoroutine (ElevateDestroy ());
	}

	public void AnimateFail(){
		around.GetComponent<Renderer> ().material.color = failColor;
	}

	private IEnumerator ElevateDestroy(){
		finished = true;
		yield return new WaitForSeconds (0.8f);
		GameObject.Destroy(this.gameObject);
	}


}
