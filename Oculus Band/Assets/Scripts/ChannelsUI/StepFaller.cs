using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepFaller : MonoBehaviour {
	public float speed = 2.0f;
	private float createdTime = 0f;
	public float MaxTime = 30.0f;
	// Use this for initialization
	void Start () {
		createdTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		float newY = this.transform.localPosition.y - speed * Time.deltaTime;
		Vector3 pos = this.transform.localPosition;
		pos.y = newY;
		this.transform.localPosition = pos;
		createdTime += Time.deltaTime;
		if (createdTime > MaxTime) {
			destroySelf ();
		}
	}

	void destroySelf(){
		GameObject.Destroy (this.transform.gameObject);
	}

	public void SetParent(Transform parent){
		Vector3 posParent = parent.transform.position;
		Vector3 thisPos = this.transform.position;
		this.transform.position = posParent;
		this.transform.parent = parent;
	}
}
