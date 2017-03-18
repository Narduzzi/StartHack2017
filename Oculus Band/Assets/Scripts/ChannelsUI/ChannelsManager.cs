using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChannelsManager : MonoBehaviour {
	public int numberOfchannels;
	private List<KeyCode> keys ;
	public GameObject originalChannel;
	private List<GameObject> channelsGO;
	// Use this for initialization
	void Start () {
		StepRecorder rec = this.GetComponent < StepRecorder> ();
		if (rec != null) {
			keys = rec.listKeys;
		}
		StepReader reader = this.GetComponent<StepReader> ();
		if (reader != null) {
			keys = reader.listKeys;
			numberOfchannels = reader.listKeys.Count;
		}

		channelsGO = new List<GameObject> ();
		createChannels (keys);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void createChannels(List<KeyCode> keys){
		for (int i = 0; i < numberOfchannels; i++) {
			GameObject channel = GameObject.Instantiate (originalChannel);
			channel.GetComponent<StepDisplayer> ().SetKeycode (keys [i]);
			Vector3 pos = channel.transform.position;
			pos.x = pos.x + 2*(float)i;
			channel.transform.position = pos;
			channel.transform.parent = this.transform;
			channel.SetActive (true);
			channelsGO.Add (channel);
		}
	}


	public void PushNote(int index,float offset){
		channelsGO [index].GetComponent<StepDisplayer> ().PushNote (offset);
	}
}
