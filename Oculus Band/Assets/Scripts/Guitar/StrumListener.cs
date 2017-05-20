using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrumListener : MonoBehaviour {

    [SerializeField]
    private GuitarTracker guitarTracker;

    void Start() {
        if (guitarTracker == null) throw new MissingReferenceException("No guitar tracker reference defined");
    }

    public void OnTriggerEnter(Collider other) {
        guitarTracker.OnStrumEnter(other);
    }

    public void OnTriggerExit(Collider other) {
        guitarTracker.OnStrumExit(other);
    }

}
