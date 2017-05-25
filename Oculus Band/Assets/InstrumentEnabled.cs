using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class InstrumentEnabled : MonoBehaviour {

    public Leap.Controller lm_controller = new Leap.Controller();
    public Renderer m_renderer;
    public PointUsable ps;
    public NetworkParameters netparams;
    public NetworkParameters.Instrument instrument;

	// Update is called once per frame
	void Update () {
        string hand = netparams.getHands();
        string device = netparams.getHeadset();

        bool oculusTouch = VRDevice.isPresent && (OVRInput.IsControllerConnected(OVRInput.Controller.LTouch) || OVRInput.IsControllerConnected(OVRInput.Controller.RTouch));
        bool leapMotion = false; // lm_controller.IsConnected;

        if (instrument == NetworkParameters.Instrument.Drums) {
            InstruEnable(oculusTouch);
        } else if(instrument == NetworkParameters.Instrument.Guitar) {
            InstruEnable(oculusTouch);
        } else {
            InstruEnable(leapMotion);
        }
	}

    private void InstruEnable(bool enable) {
        ps.SetUsable(enable);
        if (enable) {
            m_renderer.material.color = Color.white;
        } else {
            m_renderer.material.color = Color.red;
        }
    }
}
