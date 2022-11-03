using System;
using System.IO;
using System.Collections.Generic;
using Soundex;

namespace DublettenSoundex
{
    class DuplicateRecognitionSoundex
    {
        private int maxSoundexLength;

        List<string> duplicates = new List<string>();
        List<string> records = new List<string>();
        
        public DuplicateRecognitionSoundex(TextReader r, int maxSoundexLength = 10)
        {
            this.maxSoundexLength = maxSoundexLength;
 
            // Spaltenüberschriften überspringen
            r.ReadLine().Split(new char[] { ';' });
            
            string line;
            while ((line = r.ReadLine()) != null)
            {
                var record = line.Split(new char[] { ';' });
                records.Add(string.Concat(record).Soundex(maxSoundexLength) + ": " + line);
            }
            r.Close();

            maxSoundexLength = 10;
        }

        public void Recognize()
        {
            records.Sort();

            CountDuplicates = 0;

            for(int i = 0; i < records.Count-1; i++)
            {
                if (records[i].Substring(0, maxSoundexLength) == records[i + 1].Substring(0, maxSoundexLength))
                    SaveDuplicates(records[i], records[i + 1]);
            }
        }

        private void SaveDuplicates(string record1, string record2)
        {
            CountDuplicates++;

            string s = string.Format("{0}: {1}", CountDuplicates, record1);
            duplicates.Add(s);

            s = string.Format("{0}: {1}", CountDuplicates, record2);
            duplicates.Add(s);
        }

        public IEnumerable<string> Duplicates
        {
            get
            {
                foreach (var d in duplicates)
                    yield return d;
            }
        }

        public int CountDuplicates
        {
            get; set;    
        }
    }
}
