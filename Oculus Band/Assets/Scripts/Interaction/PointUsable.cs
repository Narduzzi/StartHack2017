using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PointUsable : MonoBehaviour {

    public UnityEvent OnPoint;

	void OnTriggerEnter(Collider other) {
        PointUse ps = other.gameObject.GetComponent<PointUse>();
        if(ps != null) {
            if (ps.IsPointing()) {
                Debug.Log("Pointed on  " + gameObject.name);
                OnPoint.Invoke();
            }
        }
    }
}
