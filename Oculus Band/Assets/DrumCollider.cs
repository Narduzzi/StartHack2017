using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumCollider : MonoBehaviour {

    public InstrumentManager instrumentManager;
    public int type = -1;

    private Collider m_collider;
    private Color backupColor;

    // Use this for initialization
    void Start()
    {
        m_collider = gameObject.GetComponent<Collider>();
        if(m_collider == null)
        {
            throw new MissingComponentException("No collider attached to drum");
        }

        var mr = gameObject.GetComponent<MeshRenderer>();
        if (mr != null)
        {
            backupColor = mr.material.color;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DrumStick")
        {
            instrumentManager.PressKey(type);
            StartCoroutine("Release");

            var mr = gameObject.GetComponent<MeshRenderer>();
            if (mr != null)
            {
                mr.material.color = backupColor;
            }
        }
    }

    IEnumerator Release()
    {
        yield return new WaitForSeconds(0.1f);
        instrumentManager.UnpressKey(type);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "DrumStick")
        {
            instrumentManager.UnpressKey(type);

            var mr = gameObject.GetComponent<MeshRenderer>();
            if(mr != null)
            {
                mr.material.color = backupColor;
            }
        }
    }
}
