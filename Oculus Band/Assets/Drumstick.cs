using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drumstick : MonoBehaviour {

    public OVRInput.Controller controller;
    public AudioClip clip;
    private OVRHapticsClip hapticsClip;

    void Start() {
        hapticsClip = new OVRHapticsClip(clip);
    }

	public void DoHaptics () {
        OVRHaptics.OVRHapticsChannel channel = (controller == OVRInput.Controller.LTouch) ? OVRHaptics.LeftChannel : OVRHaptics.RightChannel;
        channel.Preempt(hapticsClip);
	}

}
