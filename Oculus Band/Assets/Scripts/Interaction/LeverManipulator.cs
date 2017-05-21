using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverManipulator : MonoBehaviour {

    public enum HandEnum {
        LTOUCH,
        RTOUCH
    }

    [SerializeField]
    private HandEnum m_hand;

    private LeverSelector lever;
    private bool holdingButton = false;

    void OnTriggerEnter(Collider other) {
        Debug.Log("Trigger enter by " + other.gameObject.name);

        if (lever != null)
            return;

        LeverSelector ls = other.gameObject.GetComponent<LeverSelector>();
        if (ls != null) {
            lever = ls;
        }
    }

    void OnTriggerExit(Collider other) {
        if (holdingButton)
            return;

        LeverSelector ls = other.GetComponent<LeverSelector>();
        if (ls == null || lever != ls)
            return;

        lever = null;
    }

    void Update() {
        if (lever == null)
            return;

        bool buttonPressed = OVRInput.Get(
                (m_hand == HandEnum.LTOUCH)
                ? OVRInput.Axis1D.PrimaryHandTrigger
                : OVRInput.Axis1D.SecondaryHandTrigger
            ) > 0.5f;

        if (!holdingButton && buttonPressed) {
            lever.RegisterHold(this.gameObject);
            holdingButton = true;
        } else if (holdingButton && !buttonPressed) {
            lever.UnregisterHold(this.gameObject);
            lever = null;
            holdingButton = false;
        }
    }

}
