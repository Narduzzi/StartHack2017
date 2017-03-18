﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepFaller : MonoBehaviour {
	public float speed = 10.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float newY = this.transform.localPosition.y - 1.0f * ((1.0f*speed)/100.0f);
		Vector3 pos = this.transform.localPosition;
		pos.y = newY;
		this.transform.localPosition = pos;
		}
}
