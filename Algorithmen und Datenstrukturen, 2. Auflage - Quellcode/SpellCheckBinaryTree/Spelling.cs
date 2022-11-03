using System;
using System.IO;
using System.Collections;
using My.Collections;

namespace SpellCheck
{
    public class Spelling 
    {
        private BinaryTree<string, string> tree;

        public class Word
        {
            private string word;

            public bool IsCorrect { get; private set; }

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
            tree = new BinaryTree<string, string>();

            string word;

            while ((word = reader.ReadLine()) != null)
                if (!word.StartsWith("%"))
                    tree.Add(word.ToLower(), null);

            reader.Close();
        }

        public bool CheckWord(string word)
        {
            return tree.Contains(word.ToLower());
        }

        public IEnumerable CheckText(string text)
        {
            var tokens = text.Split(new char[] { ' ', '.', ',', ':', ';', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string token in tokens)
            {
                yield return new Word(token, tree.Contains(token.ToLower()));
            }
        }
    }
}
