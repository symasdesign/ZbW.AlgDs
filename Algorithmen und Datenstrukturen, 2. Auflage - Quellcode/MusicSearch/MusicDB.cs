using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ApproximateStringMatching;

namespace MusicSearch
{
    public class MusicDB
    {
        List<Disc> titles = new List<Disc>();

        public MusicDB(string path)
        {
            foreach (var file in Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories))
                ReadDiscFile(file);
        }

        public IEnumerable<Disc> FuzzySearch(string search)
        {
            foreach (var title in titles)
            {
                if (IsSimilar(title, search))
                    yield return title;
            }
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
                    titles.Add(new Disc(discId, discTitle, year, genre));
            }
        }

        private bool IsSimilar(Disc title, string search)
        {
            foreach (var token in title.DiscTitle.Split(' '))
            {
                if (token.Jaro(search) > 0.90)
                    return true;
            }
            if (title.Genre.Jaro(search) >= 0.90 ||
                title.Year.ToString() == search)
                return true;

            return false;
        }

        public class Disc
        {
            public Disc(string discId, string discTitle, int year, string genre)
            {
                DiscId = discId;
                DiscTitle = discTitle;
                Year = year;
                Genre = genre;
            }
            public string DiscId { get; private set; }
            public string DiscTitle { get; private set; }
            public int Year { get; private set; }
            public string Genre { get; private set; }

            public override string ToString()
            {
                return DiscTitle + "(" + Year + ") - " + Genre + " | " + DiscId;
            }
        }
    }
}
