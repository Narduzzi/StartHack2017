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


        public void SaveSong() {
            var fpath = GetAssetFromSongName(name);
            Save(fpath);
        }

        public static Song Load(string fpath) {
            TextAsset songAsset = Resources.Load(fpath) as TextAsset;
            return Load(songAsset);
        }

        public static Song Load(TextAsset songAsset) {
            if (songAsset == null)
                throw new System.Exception("Song does not exist!");

            var serializer = new XmlSerializer(typeof(Song));
            using (Stream ms = new MemoryStream(songAsset.bytes)) {
                var stream = new StreamReader(ms, System.Text.Encoding.UTF8, true);
                return serializer.Deserialize(stream) as Song;
            }
        }

        public void Save() {
            TextAsset songAsset = GetAssetFromSongName(name);
			if (songAsset == null) {
				Debug.LogError ("The asset \"" + name + "\" does not exist");
			}
            Save(songAsset);
        }

        public void Save(TextAsset songAsset) {
            if (songAsset == null)
                throw new System.Exception("Song does not exist!");

            var serializer = new XmlSerializer(typeof(Song));
			try{
	            using (Stream ms = new MemoryStream(songAsset.bytes)) {
	                var stream = new StreamWriter(ms, System.Text.Encoding.UTF8);
	                serializer.Serialize(stream, this);
	            }
			}catch(Exception e){
				Debug.LogError ("Could not save song. Error in MemoryStream");
			}
        }

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
