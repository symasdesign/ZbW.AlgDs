using My.Collections;
using System;
using System.IO;
using System.Text;

namespace MusicDB
{
    [Serializable()]
    public class Discs
    {
        private RedBlackTree<string, Disc> discTitles;
        private string musicFolder;

        public Discs(string musicFolder)
        {
            this.musicFolder = musicFolder;
            this.discTitles = new RedBlackTree<string, Disc>();
        }

        public void Load()
        {
            foreach (var file in Directory.EnumerateFiles(musicFolder, "*.", SearchOption.AllDirectories))
                ReadDiscFile(file);
        }

        public Disc Read(string discId)
        {
            if (discTitles.Contains(discId))
                return discTitles[discId];

            return null;
        }

        private void ReadDiscFile(string file)
        {
            using (StreamReader reader = new StreamReader(file, Encoding.Default))
            {
                string line;
                string discTitle = null, genre = null, discId = null;
                int year = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    if (line[0] != '#')
                    {
                        var index = line.IndexOf('=');
                        var key = line.Substring(0, index);
                        var value = line.Substring(index + 1);

                        if (key == "DISCID")
                            discId = value;
                        else if (key == "DTITLE")
                            discTitle = value;
                        else if (key == "DGENRE")
                            genre = value;
                        else if (key == "DYEAR")
                            int.TryParse(value, out year);
                    }
                }
                if (discId != null)
                    discTitles.Add(discId, new Disc(discId, discTitle, year, genre));
            }
        }
    }

    [Serializable()]
    public class Disc
    {
        public Disc(string discId, string discTitle, int year, string genre)
        {
            DiscId = discId;
            DiscTitle = discTitle;
            Year = year;
            Genre = genre;
        }

        public string DiscId { get; set; }
        public string DiscTitle { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }

        public override string ToString()
        {
            return $"{DiscId} | {DiscTitle} | {Year} | {Genre}";
        }
    }
}