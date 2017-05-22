﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using OBNet;

public class NetworkParameters : MonoBehaviour {

	public enum Instrument{Guitar,Piano,Drums};
	public enum HeadSet{Desktop,Oculus};
	public enum Hands{LeapMotion,Touch};
	public int numberOfPlayers;
	public OBNetworkDiscovery discoveryClient;
    public OBNetworkDiscovery discoveryServer;
    public NetworkManager manager;

	public Hands hands = Hands.LeapMotion;
	public HeadSet headSet = HeadSet.Desktop;
	public Instrument instrument = Instrument.Piano;
	private bool tryingToConnect = false;
	void Awake(){
		DontDestroyOnLoad (this.gameObject);
	}

	public string getInstrument(){
		return instrument.ToString();
	}
	public string getHeadset(){
		return headSet.ToString();
	}
	public string getHands(){
		return hands.ToString();
	}
	public int getNumberOfPlayers(){
		return numberOfPlayers;	
	}
	public void SetNumberOfPlayers(int numberOfPlayers){
		this.numberOfPlayers = numberOfPlayers;
	}
	private void setInstrument(Instrument instrument){
		this.instrument = instrument;

        StartCoroutine(StartDiscovery());
    }

    private IEnumerator StartDiscovery() {
        OVRScreenFade osf = GameObject.FindObjectOfType<OVRScreenFade>();
        if (osf != null) {
            Debug.Log("Fading out..");
            yield return osf.FadeOut();
        }

        discoveryClient.DiscoverGames(ConnectToFirstRoom);
        yield return null;
    }

    private void ConnectToFirstRoom(List<NetworkGame> games){
		if (tryingToConnect == false) {
			if (games.Count == 0) {
				manager.networkAddress = "localhost";
				manager.networkPort = 7777;
				discoveryServer.Initialize ();
				discoveryServer.StartAsServer ();
				manager.StartHost ();
			} else {
				NetworkGame first = games [0];
				manager.networkAddress = first.addr;
				manager.networkPort = Convert.ToInt32 (first.port);
				manager.StartClient ();
			}
			tryingToConnect = true;
		}

	}

	private void setHands(Hands hands){
		this.hands = hands;
	}

	private void setHeadset(HeadSet headSet){
		this.headSet = headSet;
	}

	public void SetInstrument(string instrument){
		foreach(Instrument instru in Enum.GetValues(typeof(Instrument))){
			if (instru.ToString () == instrument) {
				setInstrument (instru);
			}
		}
	}

	public void SetHeadSet(string hs){
		foreach(HeadSet h in Enum.GetValues(typeof(HeadSet))){
			if (h.ToString () == hs) {
				setHeadset (h);
			}
		}
	}

	public void SetHands(string hands){
		foreach(Hands h in Enum.GetValues(typeof(Hands))){
			if (h.ToString () == hands) {
				setHands (h);
			}
		}
	}

	public void SetMode(LeverSelector.SelectedMode mode){
		if(mode == LeverSelector.SelectedMode.MULTIPLAYER){
			SetNumberOfPlayers(2);
		}
		if(mode == LeverSelector.SelectedMode.SINGLEPLAYER){
			SetNumberOfPlayers(1);
		}
	}
}