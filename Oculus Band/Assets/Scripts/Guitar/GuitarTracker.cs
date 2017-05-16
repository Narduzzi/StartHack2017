using System.Collections;
using System.Collections.Generic;

using OBUtils;

using UnityEngine;


/// <summary>
/// Moves the guitar based on an anchor.
/// </summary>
/// \author Robin Weiskopf
public class GuitarTracker : MonoBehaviour {

    public MeshRenderer testRenderer;

    [SerializeField]
    private int numberNotes = 3;

    [SerializeField]
    private Color[] noteColors;

    /// <summary>
    /// Returns the number of notes the instrument has.
    /// </summary>
    public int NumberNotes {
        get {
            return numberNotes;
        }
    }

    /// <summary>
    /// Empty containing the notes objects as direct children.
    /// </summary>
    [SerializeField]
    private GameObject notesContainer;

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
        InitializeNotes();

        initialRotation = this.transform.localRotation;
        initialPosition = this.transform.localPosition;
    }

    private void InitializeNotes() {
        for (int i= 0; i<noteColors.Length; i++) {
            GameObject noteObject = notesContainer.transform.GetChild(i).gameObject;
            Renderer mr = noteObject.GetComponent<Renderer>();
            mr.material.color = noteColors[i];
        }
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
        // Update inRange and calculate some useful values
        Vector3 currentPosition = this.transform.position;
        float distance = (directionalAnchor.position - currentPosition).magnitude;
        bool nowInRange = Utils.InRange(distance, noteRangeStart * guitarLength, noteRangeEnd * guitarLength);
        float rangePerc = (distance / guitarLength - noteRangeStart) / (noteRangeEnd - noteRangeStart);

        if (!inRange && nowInRange) {
            Debug.Log("In range !");
            testRenderer.material.color = Color.green;
            inRange = true;
        } else if (inRange && !nowInRange) {
            Debug.Log("Out of range");
            testRenderer.material.color = Color.red;
            inRange = false;
        }

        if (inRange) {
            int note = Mathf.FloorToInt(rangePerc * numberNotes);

            VisualLog.Write("Note: " + note);
        }
    }

    private void CheckComponents() {
        // Check parameters not null
        //if (instruManager == null) throw new MissingReferenceException("No instrument manager defined");
        if (anchor == null) throw new MissingReferenceException("No tracking hand defined");
        if (directionalAnchor == null) throw new MissingReferenceException("No direction hand defined");
        if (notesContainer == null) throw new MissingReferenceException("Notes container not defined");

        // Check notes
        if (numberNotes <= 0) throw new System.Exception("There must be at least 1 note");
        if (numberNotes != notesContainer.transform.childCount)
            throw new System.Exception("The number of notes in notesContainer is not correct");
        if (noteColors.Length > 1 && numberNotes != noteColors.Length)
            throw new System.Exception("The number of colors is not correct");

        // Check note range start and end
        if (!Utils.InRange(noteRangeStart, 0.0f, 1.0f))
            throw new System.Exception("Invalid value for noteRangeStart. Must be in range [0.f, 1.f]");
        if (!Utils.InRange(noteRangeEnd, 0.0f, 1.0f))
            throw new System.Exception("Invalid value for noteRangeEnd. Must be in range [0.f, 1.f]");
        if (noteRangeStart > noteRangeEnd)
            Utils.Swap(ref noteRangeStart, ref noteRangeEnd);

    }

}
