using System;
using System.IO;
using System.Collections.Generic;
using ApproximateStringMatching;

namespace DublettenJaro
{
    class DuplicateRecognitionJaro
    {
        List<string> duplicates = new List<string>();
        List<string[]> records = new List<string[]>();

        public DuplicateRecognitionJaro(TextReader r)
        {
            // Spaltenüberschriften überspringen
            r.ReadLine().Split(new char[] { ';' });
            
            string line;
            while ((line = r.ReadLine()) != null)
            {
                var record = line.Split(new char[] { ';' });
                records.Add(record);
            }
            r.Close();
        }

        public void Recognize(double threshold)
        {
            CountDuplicates = 0;

            for(int i = 0; i < records.Count; i++)
            {
                string[] record1 = records[i];

                for (int j = i + 1; j < records.Count; j++ )
                {
                    double sum = 0.0;

                    string[] record2 = records[j];

                    for (int k = 0; k < record1.Length; k++)
                    {
                        string text1 = record1[k];
                        string text2 = record2[k];

                        sum += text1.Jaro(text2);
                    }
                    double similarity = sum / record1.Length;

                    if (similarity >= threshold)
                        SaveDuplicates(record1, record2, similarity);
                }
            }
        }

        private void SaveDuplicates(string[] record1, string[] record2, double similarity)
        {
            CountDuplicates++;

            string s = string.Format("{0} ({1}): ", CountDuplicates, similarity);

            foreach (var col in record1)
                s += col + ", ";

            duplicates.Add(s);

            s = string.Format("{0} ({1}): ", CountDuplicates, similarity);

            foreach (var col in record2)
                s += col + ", ";

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
