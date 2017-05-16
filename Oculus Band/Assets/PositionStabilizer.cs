using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionStabilizer : MonoBehaviour {
	Vector3 initialPosition;
	Quaternion initialRotation;
	Rigidbody rigidbody;

	public Vector3 ForceVector;
	// Use this for initialization
	void Start () {
		this.initialPosition = this.transform.position;
		this.initialRotation = this.transform.rotation;
		rigidbody = this.transform.GetComponent<Rigidbody> ();
		if (rigidbody == null) {
			Debug.LogError ("No rigidbody attached to : " + this.transform.name);
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 actualPosition = this.transform.position;
		//specific to key
		if (actualPosition.y > initialPosition.y) {
			this.transform.position = initialPosition;
		}

		//add return force
		Vector3 forceToApply = computeForce ();
		rigidbody.AddForceAtPosition (forceToApply, this.transform.position);
	

	}

	private Vector3 computeForce(){
		Vector3 actualPosition = this.transform.position;
		Vector3 substracted = initialPosition - actualPosition;
		Vector3 force = new Vector3 (substracted.x * ForceVector.x, substracted.y * ForceVector.y, substracted.z * ForceVector.z);
		return force;
	}
}
