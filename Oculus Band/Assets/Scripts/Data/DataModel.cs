using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

using UnityEngine;
using System;

namespace XMLDataModel {
    [XmlRoot("Song")]
    public class Song {
        [XmlAttribute("name")]
        public string name;

        [XmlAttribute("duration")]
        public float duration;

        [XmlArray("notes")]
        [XmlArrayItem("Note")]
        public List<Note> notes = new List<Note>();

        public Song() { }

        public Song(string name, List<Note> notes) {
            this.name = name;
            this.notes = new List<Note>(notes);
        }


        private TextAsset GetAssetFromSongName(string songName) {
            if (songName.EndsWith(".xml"))
                throw new System.Exception("The asset should not have a file extension at the end!");

            string fpath = Path.Combine("Tracks/", name);
            return Resources.Load<TextAsset>(fpath);
        }

        public Song LoadSong(string songName) {
            var fpath = GetAssetFromSongName(name);
            return Load(fpath);
        }

        public static Song Load(string fpath) {
            TextAsset songAsset = Resources.Load(fpath) as TextAsset;

			var serializer = new XmlSerializer(typeof(Song));
			var stream = new FileStream(fpath, FileMode.Open);
			Song theSong = serializer.Deserialize(stream) as Song;
			//return Load(songAsset);
			return theSong; 
        }

        public static Song Load(TextAsset songAsset) {
            if (songAsset == null)
                throw new System.Exception("Song does not exist!");

            var serializer = new XmlSerializer(typeof(Song));

			//New code

            using (Stream ms = new MemoryStream(songAsset.bytes)) {
                var stream = new StreamReader(ms, System.Text.Encoding.UTF8, true);
                return serializer.Deserialize(stream) as Song;
            }
        }

        public void Save() {
            //var fpath = GetAssetFromSongName(name);
            string filepath = Path.Combine(Application.dataPath, "Resources/Tracks/" + name + ".xml");
            Save(filepath);
        }

        public void Save(string fpath) {
            /*
            TextAsset songAsset = GetAssetFromSongName(name);
			if (songAsset == null) {
				Debug.LogError ("The asset \"" + name + "\" does not exist");
			}
            */

            var serializer = new XmlSerializer(typeof(Song));
            var stream = new FileStream(fpath, FileMode.Create);
            serializer.Serialize(stream, this);
            stream.Close();

            Debug.Log("Saved file to : " + Path.GetFullPath(fpath));
        }

        /*
        public void Save(TextAsset songAsset) {
            if (songAsset == null)
                throw new System.Exception("Song does not exist!");

            var serializer = new XmlSerializer(typeof(Song));
			try{
	            using (Stream ms = new MemoryStream(songAsset.bytes)) {
                    if (!ms.CanWrite) {
                        Debug.LogError("Can not write to stream");
                    }

	                var stream = new StreamWriter(ms, System.Text.Encoding.UTF8);
	                serializer.Serialize(stream, this);
	            }
			} catch(Exception e) {
				Debug.LogError ("Exception occured: " + e.ToString());
			}
        }
        */

    }

    public class Note {
        public Note() { }

        public Note(float time, uint type) : this(time, type, false) { }

        public Note(float time, uint type, bool bonus) {
            this.time = time;
            this.type = type;
            this.bonus = bonus;
        }

        [XmlAttribute("time")]
        public float time;

        [XmlAttribute("type")]
        public uint type;

        [XmlAttribute("bonus")]
        public bool bonus;
    }

}
