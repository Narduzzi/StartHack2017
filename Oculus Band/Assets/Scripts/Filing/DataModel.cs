using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

using UnityEngine;

namespace XMLDataModel
{
    [XmlRoot("Song")]
    public class Song
    {
        private static string path = Application.dataPath;

        [XmlAttribute("name")]
        public string name;

        [XmlAttribute("duration")]
        public float duration;

        [XmlArray("notes")]
        [XmlArrayItem("Note")]
        public List<Note> notes = new List<Note>();

        public Song() { }

        public Song(string name, List<Note> notes)
        {
            this.name = name;
            this.notes = new List<Note>(notes);
        }



        public void Save()
        {
            string fpath = System.IO.Path.Combine(path, name + ".xml");
            var serializer = new XmlSerializer(typeof(Song));
            var stream = new FileStream(fpath, FileMode.Create);
            serializer.Serialize(stream, this);

            stream.Close();
        }

        public Song Load(string fpath)
        {
            var serializer = new XmlSerializer(typeof(Song));
            var stream = new FileStream(fpath, FileMode.Open);
            var container = serializer.Deserialize(stream) as Song;
            stream.Close();

            return container;
        }
    }

    public class Note
    {
        public Note() { }

        public Note(float time, uint type) : this(time, type, false) { }

        public Note(float time, uint type, bool bonus)
        {
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
