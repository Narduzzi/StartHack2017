﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Attach this to an object which should enable PointUsable.
/// </summary>
public class PointUse : MonoBehaviour {

    public enum HandEnum {
        LTouch,
        RTouch,
        LeapMotion
    }

    [SerializeField]
    private HandEnum controller;

	// Update is called once per frame
	public bool IsPointing () {
		if (controller == HandEnum.LTouch) {
            return !OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger) && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.7f;
        } else if (controller == HandEnum.RTouch) {
            return !OVRInput.Get(OVRInput.Touch.SecondaryIndexTrigger) && OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.7f;
        } else if (controller == HandEnum.LeapMotion) {
            return true;
        } else {
            return false;
        }
	}

}
