using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMLDataModel;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public MeshRenderer cubeRenderer;
    public float toleranceTime = 0.2f;
    public List<KeyCode> keys;
	public bool play = false;
    public TextAsset songAsset;

    private List<Note> notes;
    private List<List<Note>> notesArray = new List<List<Note>>();
	private float startTime;
	private bool valid = true;
	
	public string instrument;
	public AudioManager audioManager;
	private ChannelsManager channelManager;
	private float treatmentTime = 0.3f;
	public InstrumentManager instruManager;
	public int score;
	public Text text;

	// Use this for initialization
	void Awake () {
		Song song = Song.Load (this.songAsset);
		notes = song.notes;
		if (notes == null || notes.Count < 2) {
			throw new MissingComponentException ("No song found");
		}
		channelManager = this.GetComponent<ChannelsManager> ();
		if (channelManager == null) {
			Debug.LogError ("No ChannelManager attached in the GameObject " + this.gameObject);
		}

		notesArray = new List<List<Note>> ();
		notesArray = StepReader.GenerateListByChannel(notesArray, keys, notes);

        startTime = Time.unscaledTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (!play) {
			channelManager.RestoreAllChannels ();
		}
		if (play) {
			if (instruManager != null) {
				for (int i = 0; i < keys.Count; i++) {
					if (Input.GetKeyDown (keys [i]) || instruManager.ReadKeyPressed (i)) {
						valid = channelManager.CheckChannel (i);
						if (valid) { //False press
							score = score + 100;
							channelManager.RestoreAllChannels ();
						} else {
							//score = score - 50;
							//break;
						}
					} else {
						valid = channelManager.CheckAllChannels ();
					}
				}
			}
			if (text != null) {
				text.text = "Score\n" + score;
			}
			//UpdateBox ();
			UpdateAudioManager ();
		}
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
