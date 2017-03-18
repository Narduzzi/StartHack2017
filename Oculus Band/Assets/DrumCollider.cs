using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumCollider : MonoBehaviour {

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

        backupColor = this.GetComponent<MeshRenderer>().material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
        if (other.gameObject.tag == "DrumStick")
        {
            this.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "DrumStick")
        {
            this.gameObject.GetComponent<MeshRenderer>().material.color = backupColor;
        }
    }
}
