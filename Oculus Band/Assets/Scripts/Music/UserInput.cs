using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using XMLDataModel;

public class UserInput : MonoBehaviour {

    /// <summary>
    /// Maximum error allowed by the user.
    /// </summary>
    public float maxLatency = 0.15f;

    // TODO: add the list of notes correctly
    [SerializeField]
	private List<Note> notes;

    private List<Note> remainingNotes;


    private void Start()
    {
        if (notes == null) throw new MissingReferenceException("No note list");
        
        remainingNotes = new List<Note>(notes);
        remainingNotes.Sort(new CompareNote());
    }

    public uint PlayNote(float time, uint type)
    {
        uint found = 0;

        foreach(var n in remainingNotes)
        {
            if (Mathf.Abs(time - n.time) <= maxLatency)
            {
                found += 1;
            }
        }

        if (found > 0)
        {
            remainingNotes.RemoveAll(n => Mathf.Abs(time - n.time) <= maxLatency);
        }
        return found;
    }

    private class CompareNote : IComparer<Note>
    {
        int IComparer<Note>.Compare(Note x, Note y)
        {
            if (x.time < y.time)
            {
                return -1;
            }
            else if (x.time == y.time)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    };
}
