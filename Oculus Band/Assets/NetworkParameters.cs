using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkParameters : MonoBehaviour {

	public enum Instrument{Guitar,Piano,Drums};
	public enum HeadSet{Desktop,Oculus};
	public enum Hands{LeapMotion,Touch};

	public Hands hands = Hands.LeapMotion;
	public HeadSet headSet = HeadSet.Desktop;
	public Instrument instrument = Instrument.Piano;

	void Awake(){
		DontDestroyOnLoad (this.gameObject);
	}

	public string getInstrument(){
		return instrument.ToString ();
	}
	public string getHeadset(){
		return headSet.ToString ();
	}
	public string getHands(){
		return hands.ToString ();
	}

	public void setInstrument(Instrument instrument){
		this.instrument = instrument;
	}

	public void setHands(Hands hands){
		this.hands = hands;
	}

	public void setHeadset(HeadSet headSet){
		this.headSet = headSet;
	}

}
