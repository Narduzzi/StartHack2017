using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardManager : MonoBehaviour {

    public enum ScanCode
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

    
    private static ScanCode[][] zones = new ScanCode[5][] {
        new ScanCode[] {
            ScanCode.TILDE,
            ScanCode.TAB,
            ScanCode.CAPS_LOCK,
            ScanCode.LEFT_SHIFT,
            ScanCode.LEFT_CONTROL,
            ScanCode.ONE,
            ScanCode.Q,
            ScanCode.A,
            ScanCode.BACKSLASH,
            ScanCode.LEFT_WINDOWS,
            ScanCode.TWO,
            ScanCode.W,
            ScanCode.S,
            ScanCode.Y
        },
        new ScanCode[] {
            ScanCode.THREE,
            ScanCode.E,
            ScanCode.D,
            ScanCode.X,
            ScanCode.C,
            ScanCode.LEFT_ALT,
            ScanCode.FOUR,
            ScanCode.R,
            ScanCode.F,
            ScanCode.V,
            ScanCode.FIVE,
            ScanCode.T,
            ScanCode.G
        },
        new ScanCode[] {
            ScanCode.SIX,
            ScanCode.Z,
            ScanCode.H,
            ScanCode.B,
            ScanCode.SEVEN,
            ScanCode.U,
            ScanCode.J,
            ScanCode.N,
            ScanCode.EIGHT,
            ScanCode.I,
            ScanCode.K,
            ScanCode.M
        },
        new ScanCode[] {
            ScanCode.NINE,
            ScanCode.O,
            ScanCode.L,
            ScanCode.COMMA,
            ScanCode.ZERO,
            ScanCode.P,
            ScanCode.SEMICOLON,
            ScanCode.QUOTE,
            ScanCode.PERIOD,
            ScanCode.RIGHT_ALT,
            ScanCode.RIGHT_WINDOWS,
            ScanCode.MINUS,
            ScanCode.OPEN_BRACKET,
            ScanCode.FORWARD_SLASH
        },
        new ScanCode[]
        {
            ScanCode.CLOSE_BRACKET,
            ScanCode.EQUALS,
            ScanCode.BACKSPACE,
            ScanCode.ENTER,
            ScanCode.HASHTAG,
            ScanCode.RIGHT_SHIFT,
            ScanCode.APPLICATION_SELECT,
            ScanCode.RIGHT_CONTROL
        }
    };
    

    [SerializeField]
    private Color[] colors = new Color[5]
    {
        Color.white,
        Color.red,
        Color.blue,
        Color.green,
        Color.magenta
    };



	void Start () {
        if(!LogitechGSDK.LogiLedInit())
        {
            Debug.LogError("Logitech Gaming Software reported a problem. Is the keyboard connected?");
        }

        for(int i=0; i<zones.Length; ++i)
        {
            ZoneColor(i, colors[i]);
        }

    }

    private void OnDestroy()
    {
        LogitechGSDK.LogiLedShutdown();
    }

    /*
    private void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            Debug.Log(e.keyCode);
        }
    }
    */

    private IEnumerator WaitAndSwitch(float time, int zone, Color c)
    {
        yield return new WaitForSeconds(time);

        ZoneColor(zone, c);
    }

    void Update () {
        HandleInput();
	}

    /// <summary>
    /// Helper function that handles keyboard input.
    /// </summary>
    private void HandleInput()
    {
        for(int i=0; i<zones.Length; i++)
        {
            if (IsKeyDownFromZone(i))
            {
                ZoneColor(i, Color.black);
                StartCoroutine(WaitAndSwitch(1.0f, i, colors[i]));
            }
        }
    }

    /// <summary>
    /// Returns true if there is a key currently down in the zone.
    /// Works only in the update method.
    /// </summary>
    private bool IsKeyDownFromZone(int zone)
    {
        foreach(var key in zones[zone])
        {
            if(Input.GetKeyDown(GetKeyCodeFromScanCode(key)))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Colors a zone into the color given.
    /// </summary>
    public void ZoneColor(int zone, Color c)
    {
        int r = (int)Mathf.Round(c.r * 100);
        int g = (int)Mathf.Round(c.g * 100);
        int b = (int)Mathf.Round(c.b * 100);

        foreach (var key in zones[zone])
        {
            LogitechGSDK.LogiLedSetLightingForKeyWithScanCode((int)key, r, g, b);
        }
    }


    /// <summary>
    /// Transforms a KeyboardManager.ScanCode to a UnityEngine.KeyCode.
    /// </summary>
    public KeyCode GetKeyCodeFromScanCode(ScanCode code)
    {
        switch(code)
        {
            case ScanCode.ESC:
                return KeyCode.Escape;
            case ScanCode.A:
                return KeyCode.A;
            case ScanCode.B:
                return KeyCode.B;
            case ScanCode.C:
                return KeyCode.C;
            case ScanCode.D:
                return KeyCode.D;
            case ScanCode.E:
                return KeyCode.E;
            case ScanCode.F:
                return KeyCode.F;
            case ScanCode.G:
                return KeyCode.G;
            case ScanCode.H:
                return KeyCode.H;
            case ScanCode.I:
                return KeyCode.I;
            case ScanCode.J:
                return KeyCode.J;
            case ScanCode.K:
                return KeyCode.K;
            case ScanCode.L:
                return KeyCode.L;
            case ScanCode.M:
                return KeyCode.M;
            case ScanCode.N:
                return KeyCode.N;
            case ScanCode.O:
                return KeyCode.O;
            case ScanCode.P:
                return KeyCode.P;
            case ScanCode.Q:
                return KeyCode.Q;
            case ScanCode.R:
                return KeyCode.R;
            case ScanCode.S:
                return KeyCode.S;
            case ScanCode.T:
                return KeyCode.T;
            case ScanCode.U:
                return KeyCode.U;
            case ScanCode.V:
                return KeyCode.V;
            case ScanCode.W:
                return KeyCode.W;
            case ScanCode.X:
                return KeyCode.X;
            case ScanCode.Y:
                return KeyCode.Y;
            case ScanCode.Z:
                return KeyCode.Z;

            case ScanCode.ONE:
                return KeyCode.Alpha1;
            case ScanCode.TWO:
                return KeyCode.Alpha2;
            case ScanCode.THREE:
                return KeyCode.Alpha3;
            case ScanCode.FOUR:
                return KeyCode.Alpha4;
            case ScanCode.FIVE:
                return KeyCode.Alpha5;
            case ScanCode.SIX:
                return KeyCode.Alpha6;
            case ScanCode.SEVEN:
                return KeyCode.Alpha7;
            case ScanCode.EIGHT:
                return KeyCode.Alpha8;
            case ScanCode.NINE:
                return KeyCode.Alpha9;
            case ScanCode.ZERO:
                return KeyCode.Alpha0;

            case ScanCode.BACKSPACE:
                return KeyCode.Backspace;
            case ScanCode.ENTER:
                return KeyCode.Return;
            case ScanCode.SPACE:
                return KeyCode.Space;
            case ScanCode.COMMA:
                return KeyCode.Comma;
            case ScanCode.PERIOD:
                return KeyCode.Period;
            case ScanCode.SEMICOLON:
                return KeyCode.Quote;
            case ScanCode.MINUS:
                return KeyCode.LeftBracket;
            case ScanCode.EQUALS:
                return KeyCode.RightBracket;
            case ScanCode.FORWARD_SLASH:
                return KeyCode.Minus;
            case ScanCode.HASHTAG:
                return KeyCode.BackQuote;
            case ScanCode.CLOSE_BRACKET:
                return KeyCode.Plus;
            case ScanCode.TILDE:
                return KeyCode.Slash;

            case ScanCode.LEFT_SHIFT:
                return KeyCode.LeftShift;
            case ScanCode.RIGHT_SHIFT:
                return KeyCode.RightShift;
            case ScanCode.LEFT_ALT:
                return KeyCode.LeftAlt;
            case ScanCode.RIGHT_ALT:
                return KeyCode.RightAlt;
            case ScanCode.LEFT_CONTROL:
                return KeyCode.LeftControl;
            case ScanCode.RIGHT_CONTROL:
                return KeyCode.RightControl;
            case ScanCode.LEFT_WINDOWS:
                return KeyCode.LeftWindows;
            case ScanCode.RIGHT_WINDOWS:
                return KeyCode.RightWindows;
            case ScanCode.APPLICATION_SELECT:
                return KeyCode.Menu;

            default:
                return KeyCode.None;
        }
    }

}
