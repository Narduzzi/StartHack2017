using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentManager : MonoBehaviour {

    public int keys = 3;
    private bool[] pressedKeys;

    private void Start()
    {
        pressedKeys = new bool[keys];
    }

    public int Count()
    {
        return keys;
    }

	public bool KeyPressed(int index)
    {
        return pressedKeys[index];
    }

    public void PressKey(int index)
    {
        pressedKeys[index] = true;
    }

    public void UnpressKey(int index)
    {
        pressedKeys[index] = false;
    }

    private void Update()
    {
        for(int i=0; i<keys; i++)
        {
            //pressedKeys[i] = false;
        }
    }

}
