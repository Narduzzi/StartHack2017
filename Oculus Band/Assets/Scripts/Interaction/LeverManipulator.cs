using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using Leap;

/// <summary>
/// Attach this to an object with a collider to be able to manipulate a lever
/// using Oculus Touch or Leap Motion.
/// </summary>
public class LeverManipulator : MonoBehaviour {

    public enum HandEnum {
        LTOUCH,
        RTOUCH,
        LeapMotionL,
        LeapMotionR
    }

    [SerializeField]
    private HandEnum m_hand;

    [SerializeField]
    private LeapHandController lmController;

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

        bool buttonPressed = false;
        if (m_hand == HandEnum.LTOUCH) {
            buttonPressed = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.5f;
        } else if (m_hand == HandEnum.RTOUCH) {
            buttonPressed = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.5f;
        } else if (m_hand == HandEnum.LeapMotionL) {
            foreach(Hand h in lmController.provider.CurrentFrame.Hands) {
                if (h.IsLeft) {
                    buttonPressed = h.GrabStrength > 0.7f;
                }
            }
        } else if (m_hand == HandEnum.LeapMotionR) {
            foreach (Hand h in lmController.provider.CurrentFrame.Hands) {
                if (h.IsRight) {
                    buttonPressed = h.GrabStrength > 0.7f;
                }
            }
        }


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
