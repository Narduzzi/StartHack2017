using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointUse : MonoBehaviour {

    public enum HandEnum {
        LTouch,
        RTouch
    }

    [SerializeField]
    private HandEnum controller;

	// Update is called once per frame
	public bool IsPointing () {
		if (controller == HandEnum.LTouch) {
            return !OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger) && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.7f;
        } else {
            return !OVRInput.Get(OVRInput.Touch.SecondaryIndexTrigger) && OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.7f;
        }
	}

}
