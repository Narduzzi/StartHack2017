using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class LeverSelector : MonoBehaviour {

    public float opAngle;
	public NetworkParameters parameters;
    public enum SelectedMode {
        SINGLEPLAYER,
        MULTIPLAYER
    }

    public SelectedMode selectedMode;
	public UnityEvent<SelectedMode> OnModeChange;

    public bool dontUpdate = false;
    private float currentAngle {
        get {
            float angle = transform.localRotation.eulerAngles.z;
            if (angle > 180f) {
                angle -= 360f;
            }
            return angle;
        }
        set {
            dontUpdate = false;
            transform.localRotation = Quaternion.Euler(0f, 0f, value);
        }
    }
    private GameObject holder;
    private bool holding { get { return holder != null; } }

	// Use this for initialization
	void Start () {
        opAngle = Mathf.Abs(opAngle);
        selectedMode = GetMode();
	}

    public SelectedMode GetMode() {
         return (currentAngle >= 0.0f) ? SelectedMode.SINGLEPLAYER : SelectedMode.MULTIPLAYER;
    }

    public void RegisterHold(GameObject holder) {
        this.holder = holder;
        dontUpdate = false;
    }

    public void UnregisterHold(GameObject holder) {
        if (holder == this.holder) {
            this.holder = null;
        }
    }
	
	void Update () {
        if (dontUpdate)
            return;

        // Let the lever fall back
        if (!holding) {
            float newAngle = currentAngle * (1f + Time.deltaTime);
            if (Mathf.Abs(newAngle) < 0.1f) {
                newAngle = Random.Range(-0.5f, 0.5f);
            }

            if (Mathf.Abs(newAngle) > opAngle) {
                currentAngle = Mathf.Clamp(currentAngle, -opAngle, opAngle);
                dontUpdate = true;
            } else {
                currentAngle = newAngle;
            }
        // An object is holding the lever and manipulating it
        } else {
            if (holder == null)
                return;

            Vector3 handPos = holder.transform.position;
            Vector3 diff = (handPos - this.transform.position).normalized;
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, new Vector3(diff.x, diff.y, 0f));

            transform.localRotation = rotation;
            if (Mathf.Abs(currentAngle) > opAngle) {
                currentAngle = Mathf.Clamp(currentAngle, -opAngle, opAngle);
            }
        }

        SelectedMode newMode = GetMode();
        if (newMode != selectedMode) {
            Debug.Log("Mode changed!");
            selectedMode = newMode;
			parameters.SetMode (selectedMode);
            OnModeChange.Invoke();
        }
    }
}
