using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Attach this to an object which you want to be able to interact with.
/// Fires OnPoint event on use.
/// </summary>
public class PointUsable : MonoBehaviour {
    [SerializeField]
    private bool usable = true;

    public UnityEvent OnPoint;

	void OnTriggerEnter(Collider other) {
        if (!usable)
            return;

        PointUse ps = other.gameObject.GetComponent<PointUse>();
        if(ps != null) {
            if (ps.IsPointing()) {
                Debug.Log("Pointed on  " + gameObject.name);
                OnPoint.Invoke();

                SetUsable(false);
            }
        }
    }

    public void SetUsable(bool enabled) {
        usable = false;
    }
}
