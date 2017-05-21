using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSelector : MonoBehaviour {

    public enum HandEnum {
        LEFT,
        RIGHT
    }

    [SerializeField]
    private HandEnum m_hand;
    public HandEnum Hand { get { return m_hand; } }

}
