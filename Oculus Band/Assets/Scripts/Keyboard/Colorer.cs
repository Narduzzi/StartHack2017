using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(Flashy());
	}

    private IEnumerator Flashy()
    {
        LogitechGSDK.LogiLedInit();
        LogitechGSDK.LogiLedPulseLighting(255, 0, 0, 1000, 1000);

        yield return new WaitForSeconds(5.0f);

        LogitechGSDK.LogiLedShutdown();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
