using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Simple script to change lever text. 
/// </summary>
public class SPMPSwitch : MonoBehaviour {

    [SerializeField]
    private LeverSelector m_lever;

    [SerializeField]
    private Text m_text;

    public void UpdateText() {
        LeverSelector.SelectedMode mode = m_lever.GetMode();

        if (mode == LeverSelector.SelectedMode.SINGLEPLAYER) {
            m_text.text = "SINGLEPLAYER";
        } else {
            m_text.text = "MULTIPLAYER";
        }
    }

}
