using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour {

	public AudioManager audioManager;
	public GameObject gameLogic;
	private StepReader stepReader;
	private GameManager gameManager;
	public int RequiredPlayers = -1;
	private bool play = false;
	public SyncManager syncManager;

	// Use this for initialization
	void Start () {
		if (audioManager == null) {
			Debug.LogError ("AudioManager is null");
		}
		if (gameLogic == null) {
			Debug.LogError ("GameManager is null");
		}

		stepReader = gameLogic.GetComponentInChildren<StepReader> ();
		gameManager = gameLogic.GetComponentInChildren<GameManager> ();

		if (stepReader == null) {
			Debug.LogError ("No stepReader found in children of " + gameLogic);
		}
		if (gameManager == null) {
			Debug.LogError ("No gameManager found in children of " + gameManager);
		}

		if (syncManager == null) {
			Debug.LogError ("No syncManager found");
		}

		if (RequiredPlayers < 1) {
			Debug.LogError ("Number of required players must be positive");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!play) {
			int players = CountPlayers ();
			bool play = GetPlay ();
			StartCoroutine(Sync(GetDelay()));
			if (players == RequiredPlayers && play) {
				audioManager.play = true;
				gameManager.play = true;
				stepReader.play = true;
			}
		}
	}

	private IEnumerator Sync(float delay){
		yield return new WaitForSeconds (delay);
	}

	private int CountPlayers(){
		GameObject[] objects = GameObject.FindGameObjectsWithTag ("Player");
		return objects.Length;
	}

	private bool GetPlay(){
		return syncManager.play;
	}

	private float GetDelay(){
		return syncManager.GetDelay();
	}

}
