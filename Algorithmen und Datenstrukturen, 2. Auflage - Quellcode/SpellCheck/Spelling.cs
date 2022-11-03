using System;
using System.IO;
using System.Collections;
using My.Collections;

namespace SpellCheck
{
    public class Spelling
    {
        private LinkedList list;

        public class Word
        {
            private string word;

            public bool IsCorrect { get; set; }

            public Word(string word, bool isCorrect)
            {
                this.word = word;
                IsCorrect = isCorrect;
            }

            public override string ToString()
            {
                return word;
            }
        }

        public Spelling(TextReader reader)
        {
            list = new LinkedList();

            string word;

            while ((word = reader.ReadLine()) != null)
                if (!word.StartsWith("%"))
                    list.Add(word.ToLower());

            reader.Close();
        }

        public bool CheckWord(string word)
        {
            return list.Contains(word.ToLower());
        }

        public IEnumerable CheckText(string text)
        {
            var tokens = text.Split(new char[] { ' ', '.', ',', ':', ';', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string token in tokens)
            {
                yield return new Word(token, list.Contains(token.ToLower()));
            }
        }
    }
}
