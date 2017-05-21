using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour {

    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private float bobSpeed;
    [SerializeField]
    private float bobRange;

    [SerializeField]
    private bool locked = false;

    private Vector3 initialPosition;
    private float initialTime;

    private Vector3 diff = Vector3.zero;

    // Use this for initialization
    void Start() {
        initialPosition = transform.localPosition;

        initialTime = Time.time;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!locked) {
            Quaternion rot = Quaternion.AngleAxis(rotationSpeed * Time.fixedDeltaTime, Vector3.up);
            transform.localRotation *= rot;

            float timeDiff = Time.time - initialTime;
            diff.y = bobRange * Mathf.Sin(bobSpeed * timeDiff / (2f * 3.1415f));
            transform.localPosition = initialPosition + diff;
        }
	}
}
