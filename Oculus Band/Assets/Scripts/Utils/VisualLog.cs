using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OBUtils {
    public sealed class VisualLog : MonoBehaviour {

        [SerializeField]
        private int m_numberLines = 3;

        [SerializeField]
        private Text m_text;

        private static int numberLines;
        public static int NumberLines {
            get { return numberLines; }
        }

        public static VisualLog singleton;

        private static Text text;

        private static List<string> lines;

        // Use this for initialization
        void Start() {
            if (singleton != null && singleton != this) {
                Debug.LogError("Singleton for VisualLog already initialized");
                GameObject.DestroyObject(this);
            }

            if (singleton == null) {
                singleton = this;
                numberLines = m_numberLines;
                text = m_text;
                lines = new List<string>();
            }
        }

        public static void Write(string s) {
            lines.Add(s);

            if (lines.Count > numberLines) {
                lines.RemoveAt(0);
            }

            Redraw();
        }

        private static void Redraw() {
            string newValue = "";

            foreach (string s in lines) {
                newValue += s + "\n";
            }

            text.text = newValue;
        }

    }
}
