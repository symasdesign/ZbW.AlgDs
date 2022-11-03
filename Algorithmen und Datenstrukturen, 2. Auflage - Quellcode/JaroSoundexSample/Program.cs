using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {

static void Main(string[] args)
{
    var words = new List<string>();

    using(var reader = new StreamReader("german.dic", Encoding.Default))
    {
        string line;
        while((line = reader.ReadLine()) != null)
        {
            if(!line.StartsWith("%"))
                words.Add(line);
        }
    }

    string input;
    while((input = Console.ReadLine()) != "")
    {
        Console.WriteLine("---------Jaro----------");
        foreach (var word in words)
        {
            if (input.Jaro(word) >= 0.95)
                Console.WriteLine(word);
        }
        Console.WriteLine("---------Soundex----------");
        foreach (var word in words)
        {
            if (input.Soundex() == word.Soundex())
                Console.WriteLine(word);
        }
    }
}
    }

    static class StringExtension
    {
        private const string map = "01230120022455012623010202";

        public static string Soundex(this string text)
        {
            text = text.ToUpper();

            string code = text[0].ToString();
            char last = char.MinValue;

            for (int i = 1; i < text.Length; i++)
            {
                var chr = text[i];

                int letter = chr - 'A';
                if (letter >= 0 && letter < map.Length)
                {
                    if (map[letter] != last)
                    {
                        code += map[letter];
                        last = map[letter];
                    }
                }
            }
            code = code.Replace("0", "");

            if (code.Length > 4)
                code = code.Remove(4);
            else
                code = code.PadRight(4, '0'); 

            return code;
        }

        public static double Jaro(this string text1, string text2)
        {
            text1 = text1.ToLower();
            text2 = text2.ToLower();

            double length1 = text1.Length;
            double length2 = text2.Length;

            string common1 = commonChars(text1, text2);
            string common2 = commonChars(text2, text1);

            if (common1.Length != common2.Length)
                return 0;
            double common = common1.Length;

            int transpositions = calcTranspositions(common1, common2);

            double similarity =
                   (common / length1 + common / length2 +
                   (common - transpositions) / common)
                    / 3.0;

            return similarity;
        }


        private static string commonChars(string text1, string text2)
        {
            int length1 = text1.Length, length2 = text2.Length;

            //int halflen = Math.Min(length1, length2) / 2;

            var common = "";

            for (int x = 0; x < length1; x++)
            {
                //                for (int y = Math.Max(0, x-halflen); y < Math.Min(x+halflen, length2); y++)
                for (int y = 0; y < length2; y++)
                {
                    if (text1[x] == text2[y])
                    {
                        common += text1[x];
                        break;
                    }
                }
            }

            return common;
        }

        private static int calcTranspositions(string common1, string common2)
        {
            int transpositions = 0;

            for (int pos = 0; pos < Math.Min(common1.Length, common2.Length); pos++)
                if (common1[pos] != common2[pos])
                    transpositions++;

            return transpositions / 2;
        }
    }
}
