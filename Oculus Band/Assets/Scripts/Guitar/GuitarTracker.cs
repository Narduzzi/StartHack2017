using System.Collections;
using System.Collections.Generic;

using OBUtils;

using UnityEngine;


/// <summary>
/// Moves the guitar based on an anchor.
/// </summary>
public class GuitarTracker : MonoBehaviour {

    public MeshRenderer testRenderer;

    [SerializeField]
    private InstrumentManager instruManager;

    [SerializeField]
    private Transform anchor;

    [SerializeField]
    private Vector3 anchorOffset = new Vector3(0.0f, 0.0f, 0.0f);

    [Tooltip("The length of the guitar considering the scale parameter.")]
    [SerializeField]
    private float guitarLength = 1.0f;

    [Range(0.0f, 1.0f)]
    [SerializeField]
    private float noteRangeStart = 0.0f;

    [Range(0.0f, 1.0f)]
    [SerializeField]
    private float noteRangeEnd = 1.0f;

    [SerializeField]
    private Transform directionalAnchor;

    private bool inRange = false;

    private Quaternion initialRotation;
    private Vector3 initialPosition;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start() {
        CheckComponents();

        initialRotation = this.transform.localRotation;
        initialPosition = this.transform.localPosition;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update() {

        UpdatePosition();

        CheckForNote();

    }

    /// <summary>
    /// Updated the guitar's position based on the anchor's position.
    /// </summary>
    private void UpdatePosition() {
        Vector3 direction = (
            directionalAnchor.position
            - anchor.position
            - anchorOffset
          ).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.forward);

        this.transform.rotation = rotation * initialRotation;
        this.transform.position = initialPosition + anchor.position + anchorOffset;
    }

    /// <summary>
    /// Determines the hand's position relative to the guitar and whether 
    /// a note is played.
    /// </summary>
    private void CheckForNote() {
        Vector3 currentPosition = this.transform.position;
        float distance = (directionalAnchor.position - currentPosition).magnitude;
        bool nowInRange = Utils.InRange(distance, noteRangeStart * guitarLength, noteRangeEnd * guitarLength);

        if (!inRange && nowInRange) {
            Debug.Log("In range !");
            testRenderer.material.color = Color.green;
            inRange = true;
        } else if (inRange && !nowInRange) {
            Debug.Log("Out of range");
            testRenderer.material.color = Color.red;
            inRange = false;
        }
    }

    private void CheckComponents() {
        // Check parameters not null
        //if (instruManager == null) throw new MissingReferenceException("No instrument manager defined");
        if (anchor == null) throw new MissingReferenceException("No tracking hand defined");
        if (directionalAnchor == null) throw new MissingReferenceException("No direction hand defined");

        // Check note range start and end
        if (!Utils.InRange(noteRangeStart, 0.0f, 1.0f))
            throw new System.Exception("Invalid value for noteRangeStart. Must be in range [0.f, 1.f]");
        if (!Utils.InRange(noteRangeEnd, 0.0f, 1.0f))
            throw new System.Exception("Invalid value for noteRangeEnd. Must be in range [0.f, 1.f]");
        if (noteRangeStart > noteRangeEnd)
            Utils.Swap(ref noteRangeStart, ref noteRangeEnd);

    }

}
