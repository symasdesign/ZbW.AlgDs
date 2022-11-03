using System;
using System.IO;
using System.Collections;
using My.Collections;

namespace SpellCheck
{
    public class Spelling 
    {
        private SortedArrayList<string> list;

        public sealed class Word
        {
            private string word;

            public bool IsCorrect { get; internal set; }
            public string NearToWord { get; internal set; }

            public Word(string word, bool isCorrect, string nearToWord)
            {
                this.word = word;
                NearToWord = nearToWord;
                IsCorrect = isCorrect;
            }

            public override string ToString()
            {
                return word;
            }
        }

        public Spelling(TextReader reader)
        {
            list = new SortedArrayList<string>();

            string word;

            while ((word = reader.ReadLine()) != null)
                if (!word.StartsWith("%"))
                    list.Add(word.ToLower());

            reader.Close();
        }

        public bool CheckWord(string word, ref string nearToWord)
        {
            int index = list.BinarySearch(word.ToLower());

            if(index < 0)
            {
                nearToWord = ~index < list.Count ? list[~index] : "";
                return false;
            }
            return true;
        }

        public IEnumerable CheckText(string text)
        {
            var tokens = text.Split(new char[] { ' ', '.', ',', ':', ';', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string token in tokens)
            {
                string nearToWord = "";
                int index = list.BinarySearch(token.ToLower());
                         
                if(index < 0)
                {
                    nearToWord = ~index < list.Count ? list[~index] : "";
                }

                yield return new Word(token, index >= 0, nearToWord);
            }
        }
    }
}
