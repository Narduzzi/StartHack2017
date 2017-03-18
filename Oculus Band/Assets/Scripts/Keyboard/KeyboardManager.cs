using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardManager : MonoBehaviour {

    private static LogitechGSDK.keyboardNames[][] zones = new LogitechGSDK.keyboardNames[5][] {
        new LogitechGSDK.keyboardNames[] {
            LogitechGSDK.keyboardNames.TILDE,
            LogitechGSDK.keyboardNames.TAB,
            LogitechGSDK.keyboardNames.CAPS_LOCK,
            LogitechGSDK.keyboardNames.LEFT_SHIFT,
            LogitechGSDK.keyboardNames.LEFT_CONTROL,
            LogitechGSDK.keyboardNames.ONE,
            LogitechGSDK.keyboardNames.Q,
            LogitechGSDK.keyboardNames.A,
            LogitechGSDK.keyboardNames.BACKSLASH,
            LogitechGSDK.keyboardNames.LEFT_WINDOWS,
            LogitechGSDK.keyboardNames.TWO,
            LogitechGSDK.keyboardNames.W,
            LogitechGSDK.keyboardNames.S,
            LogitechGSDK.keyboardNames.Z
        },
        new LogitechGSDK.keyboardNames[] {
            LogitechGSDK.keyboardNames.THREE,
            LogitechGSDK.keyboardNames.E,
            LogitechGSDK.keyboardNames.D,
            LogitechGSDK.keyboardNames.X,
            LogitechGSDK.keyboardNames.C,
            LogitechGSDK.keyboardNames.LEFT_ALT,
            LogitechGSDK.keyboardNames.FOUR,
            LogitechGSDK.keyboardNames.R,
            LogitechGSDK.keyboardNames.F,
            LogitechGSDK.keyboardNames.V,
            LogitechGSDK.keyboardNames.FIVE,
            LogitechGSDK.keyboardNames.T,
            LogitechGSDK.keyboardNames.G
        },
        new LogitechGSDK.keyboardNames[] {
            LogitechGSDK.keyboardNames.SIX,
            LogitechGSDK.keyboardNames.Y,
            LogitechGSDK.keyboardNames.H,
            LogitechGSDK.keyboardNames.B,
            LogitechGSDK.keyboardNames.SEVEN,
            LogitechGSDK.keyboardNames.U,
            LogitechGSDK.keyboardNames.J,
            LogitechGSDK.keyboardNames.N,
            LogitechGSDK.keyboardNames.EIGHT,
            LogitechGSDK.keyboardNames.I,
            LogitechGSDK.keyboardNames.K,
            LogitechGSDK.keyboardNames.M
        },
        new LogitechGSDK.keyboardNames[] {
            LogitechGSDK.keyboardNames.NINE,
            LogitechGSDK.keyboardNames.O,
            LogitechGSDK.keyboardNames.L,
            LogitechGSDK.keyboardNames.COMMA,
            LogitechGSDK.keyboardNames.ZERO,
            LogitechGSDK.keyboardNames.P,
            LogitechGSDK.keyboardNames.SEMICOLON,
            LogitechGSDK.keyboardNames.PERIOD,
            LogitechGSDK.keyboardNames.RIGHT_ALT,
            LogitechGSDK.keyboardNames.RIGHT_WINDOWS,
            LogitechGSDK.keyboardNames.MINUS,
            LogitechGSDK.keyboardNames.OPEN_BRACKET,
            LogitechGSDK.keyboardNames.APOSTROPHE,
            LogitechGSDK.keyboardNames.FORWARD_SLASH
        },
        new LogitechGSDK.keyboardNames[]
        {
            //LogitechGSDK.keyboardNames.CLOSE_BRACKET,
            LogitechGSDK.keyboardNames.BACKSPACE,
            LogitechGSDK.keyboardNames.ENTER,
            LogitechGSDK.keyboardNames.RIGHT_SHIFT,
            LogitechGSDK.keyboardNames.APPLICATION_SELECT,
            LogitechGSDK.keyboardNames.RIGHT_CONTROL
        }
    };

	// Use this for initialization
	void Start () {
        LogitechGSDK.LogiLedInit();

		foreach(var key in zones[0])
        {
            LogitechGSDK.LogiLedSetLightingForKeyWithKeyName(key, 0, 100, 100);
        }

        foreach (var key in zones[1])
        {
            LogitechGSDK.LogiLedSetLightingForKeyWithKeyName(key, 0, 0, 100);
        }

        foreach (var key in zones[2])
        {
            LogitechGSDK.LogiLedSetLightingForKeyWithKeyName(key, 100, 0, 100);
        }

        foreach (var key in zones[3])
        {
            LogitechGSDK.LogiLedSetLightingForKeyWithKeyName(key, 0, 100, 0);
        }

        foreach (var key in zones[4])
        {
            LogitechGSDK.LogiLedSetLightingForKeyWithKeyName(key, 100, 100, 100);
        }

    }

    private void OnDestroy()
    {
        LogitechGSDK.LogiLedShutdown();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
