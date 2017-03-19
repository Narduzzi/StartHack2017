using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMLDataModel;

public class GameManager : MonoBehaviour {
    public MeshRenderer cubeRenderer;
    public float toleranceTime = 0.2f;
    public List<KeyCode> keys;

    public string songName;
    private List<Note> notes;
    private List<List<Note>> notesArray = new List<List<Note>>();
	private float startTime;
	private bool valid = true;
	
	public string instrument;
	public AudioManager audioManager;
	private float treatmentTime = 0.3f;
	public InstrumentManager instruManager;


	// Use this for initialization
	void Start () {
		Song song = Song.Load (songName);
		notes = song.notes;
		if (notes == null || notes.Count < 2) {
			throw new MissingComponentException ("No song found");
		}

		notesArray = new List<List<Note>> ();
		notesArray = StepReader.GenerateListByChannel(notesArray, keys, notes);

        startTime = Time.unscaledTime;
	}
	
	// Update is called once per frame
	void Update () {
        float currentTime = Time.unscaledTime - startTime;

        int i = 0;
        foreach (var nl in notesArray)
        {
            if (nl.Count > 0)
            {
                Note currentNote = nl[0];

                if (currentNote.time + toleranceTime < currentTime)
                {
                    valid = false;
                    nl.Remove(currentNote);
                }
                else
                {
                    //if (Input.GetKeyDown(keys[i]))
					if(instruManager.KeyPressed(i))
                    {
                        Debug.Log("Key pressed");

                        if (Mathf.Abs(currentNote.time - currentTime) < toleranceTime)
                        {
                            valid = true;
                        }
                        else
                        {
                            valid = false;
                        }

                        nl.Remove(currentNote);
                    }
                }
            }
            i++;
        }

        UpdateBox();
        UpdateAudioManager();
	}

    public void UpdateAudioManager()
    {
        if (valid)
        {
            audioManager.Success(instrument);
        }
        else
        {
            audioManager.Failed(instrument);
        }
    }

    private void UpdateBox()
    {
        if (valid)
        {
            cubeRenderer.material.color = Color.white;
        }
        else
        {
            cubeRenderer.material.color = Color.red;
        }
    }

}
