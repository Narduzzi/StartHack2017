using System;
using System.Collections;

using UnityEngine;
using UnityEngine.Events;

public class HandSelectable : MonoBehaviour {

    public bool scaleOnTouch = true;

    [Range(1.0f, 2.0f)]
    public float scaleOnTouchRatio = 1.0f;

    [Range(0.0f, 3.0f)]
    public float scaleSpeed = 1.0f;

    public UnityEvent OnSelect;

    private Coroutine anim;
    private Coroutine deanim;
    private float currentScale = 1.0f;
    private Vector3 initialScale;
    private GameObject currentlyEntered;

    // Use this for initialization
	void Start () {
        initialScale = transform.localScale;
	}

    void OnTriggerEnter(Collider other) {
        if (currentlyEntered != null)
            return;

        HandSelector hs = other.GetComponent<HandSelector>();
        if (hs != null && anim == null) {
            currentlyEntered = other.gameObject;

            if (anim == null) {
                anim = StartCoroutine(Animate());
            }
        }
    }

    void OnTriggerStay(Collider other) {
        if (currentlyEntered != null) {
            HandSelector hs = currentlyEntered.GetComponent<HandSelector>();
            OVRInput.Axis1D axis = (hs.Hand == HandSelector.HandEnum.LEFT) ? OVRInput.Axis1D.PrimaryHandTrigger : OVRInput.Axis1D.SecondaryHandTrigger;

            if (OVRInput.Get(axis) > 0.95f) {
                Debug.Log("SELECTED");
                OnSelect.Invoke();
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if (currentlyEntered != null) {
            currentlyEntered = null;

            StopCoroutine(anim);
            anim = null;
            deanim = StartCoroutine(Deanimate());
        }
    }

    private IEnumerator Animate() {
        while(deanim != null) {
            yield return null;
        }

        float startTime = Time.time;
        while (true) {
            float deltaTime = Time.time - startTime;

            float scale = (scaleOnTouchRatio - 1.0f) * Mathf.Sin(scaleSpeed * deltaTime * 3.1415f - 3.1415f / 2.0f) + scaleOnTouchRatio;
            SetScale(scale);

            yield return null;
        }
    }

    private IEnumerator Deanimate() {
        while(currentScale > 1.0f) {
            float scale = currentScale - Time.deltaTime;
            SetScale(scale);
            yield return null;
        }

        SetScale(1.0f);
        deanim = null;
        yield return null;
    }

    private void SetScale(float scale) {
        currentScale = scale;
        transform.localScale = initialScale * scale;
    }

}
