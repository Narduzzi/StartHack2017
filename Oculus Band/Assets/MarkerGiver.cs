using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerGiver : MonoBehaviour {

	List<GameObject> listBones3 = new List<GameObject>();
	// Use this for initialization
	void Awake () {
		AssignTagsToBones3 ();
	}
	
	// Update is called once per frame
	void Update () {
		AssignTagsToBones3 ();
	}

	void AssignTagsToBones3(){
		GameObject[] all_go = FindObjectsOfType<GameObject> ();
		foreach(GameObject go in all_go){
			if (go.name == "bone3") {
				if (go.tag != "bone3") {
					go.tag = "bone3";
					print ("Assigned tag!");
				}
			}
		}
	}
}
