using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GuitarTracker : MonoBehaviour {

    [SerializeField]
    private Transform anchor;

    [SerializeField]
    private Vector3 anchorOffset = new Vector3(0.0f, 0.0f, 0.0f);

    [SerializeField]
    private Transform directionalAnchor;

    private Quaternion initialRotation;
    private Vector3 initialPosition;

    // Use this for initialization
    void Start() {
        CheckComponents();

        initialRotation = this.transform.localRotation;
        initialPosition = this.transform.localPosition;
    }

    private void CheckComponents() {
        if (anchor == null) throw new MissingReferenceException("No tracking hand defined");
        if (directionalAnchor == null) throw new MissingReferenceException("No direction hand defined");
    }

    // Update is called once per frame
    void Update() {

        Vector3 direction = (directionalAnchor.position - anchor.position - anchorOffset).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.forward);

        this.transform.rotation = rotation * initialRotation;
        this.transform.position = initialPosition + anchor.position + anchorOffset;

    }
}
