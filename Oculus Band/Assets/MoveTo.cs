using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour {
	public Vector3 position = Vector3.zero;
	public Vector3 scale = Vector3.one;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = position;
		this.transform.localScale = scale;
	}
}
