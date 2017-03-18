using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardManager : MonoBehaviour {

    public enum KeyCode
    {
        ESC = 1,
        ONE = 2,
        TWO = 3,
        THREE = 4,
        FOUR = 5,
        FIVE = 6,
        SIX = 7,
        SEVEN = 8,
        EIGHT = 9,
        NINE = 10,
        ZERO = 11,
        MINUS = 12,
        EQUALS = 13,
        BACKSPACE = 14,
        TAB = 15,
        Q = 16,
        W = 17,
        E = 18,
        R = 19,
        T = 20,
        Z = 21,
        U = 22,
        I = 23,
        O = 24,
        P = 25,
        OPEN_BRACKET = 26,
        CLOSE_BRACKET = 27,
        ENTER = 28,
        LEFT_CONTROL = 29,
        A = 30,
        S = 31,
        D = 32,
        F = 33,
        G = 34,
        H = 35,
        J = 36,
        K = 37,
        L = 38,
        SEMICOLON = 39,
        QUOTE = 40,
        TILDE = 41,
        LEFT_SHIFT = 42,
        BACKSLASH = 86,
        Y = 44,
        X = 45,
        C = 46,
        V = 47,
        B = 48,
        N = 49,
        M = 50,
        COMMA = 51,
        PERIOD = 52,
        FORWARD_SLASH = 53,
        RIGHT_SHIFT = 54,
        PRNT_SCRN = 55,
        LEFT_ALT = 56,
        SPACE = 57,
        CAPS_LOCK = 58,
        HASHTAG = 93,
        RIGHT_CONTROL = 285,
        RIGHT_ALT = 312,
        LEFT_WINDOWS = 347,
        RIGHT_WINDOWS = 348,
        APPLICATION_SELECT = 349
    }

    
    private static KeyCode[][] zones = new KeyCode[5][] {
        new KeyCode[] {
            KeyCode.TILDE,
            KeyCode.TAB,
            KeyCode.CAPS_LOCK,
            KeyCode.LEFT_SHIFT,
            KeyCode.LEFT_CONTROL,
            KeyCode.ONE,
            KeyCode.Q,
            KeyCode.A,
            KeyCode.BACKSLASH,
            KeyCode.LEFT_WINDOWS,
            KeyCode.TWO,
            KeyCode.W,
            KeyCode.S,
            KeyCode.Y
        },
        new KeyCode[] {
            KeyCode.THREE,
            KeyCode.E,
            KeyCode.D,
            KeyCode.X,
            KeyCode.C,
            KeyCode.LEFT_ALT,
            KeyCode.FOUR,
            KeyCode.R,
            KeyCode.F,
            KeyCode.V,
            KeyCode.FIVE,
            KeyCode.T,
            KeyCode.G
        },
        new KeyCode[] {
            KeyCode.SIX,
            KeyCode.Z,
            KeyCode.H,
            KeyCode.B,
            KeyCode.SEVEN,
            KeyCode.U,
            KeyCode.J,
            KeyCode.N,
            KeyCode.EIGHT,
            KeyCode.I,
            KeyCode.K,
            KeyCode.M
        },
        new KeyCode[] {
            KeyCode.NINE,
            KeyCode.O,
            KeyCode.L,
            KeyCode.COMMA,
            KeyCode.ZERO,
            KeyCode.P,
            KeyCode.SEMICOLON,
            KeyCode.QUOTE,
            KeyCode.PERIOD,
            KeyCode.RIGHT_ALT,
            KeyCode.RIGHT_WINDOWS,
            KeyCode.MINUS,
            KeyCode.OPEN_BRACKET,
            KeyCode.FORWARD_SLASH
        },
        new KeyCode[]
        {
            KeyCode.CLOSE_BRACKET,
            KeyCode.EQUALS,
            KeyCode.BACKSPACE,
            KeyCode.ENTER,
            KeyCode.HASHTAG,
            KeyCode.RIGHT_SHIFT,
            KeyCode.APPLICATION_SELECT,
            KeyCode.RIGHT_CONTROL
        }
    };
    

    private Color[] colors = new Color[]
    {
        Color.white,
        Color.red,
        Color.blue,
        Color.green,
        Color.magenta
    };

	// Use this for initialization
	void Start () {
        LogitechGSDK.LogiLedInit();

        /*for(int i=0; i<100; i++)
            LogitechGSDK.LogiLedSetLightingForKeyWithScanCode((int)i, 100, 100, 100);

        return;*/

        for(int i=0; i<zones.Length; ++i)
        {
            Color c = colors[i];

            int r = (int)Mathf.Round(c.r * 100);
            int g = (int)Mathf.Round(c.g * 100);
            int b = (int)Mathf.Round(c.b * 100);

            foreach (var key in zones[i])
            {
                LogitechGSDK.LogiLedSetLightingForKeyWithScanCode((int)key, r, g, b);
            }

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
