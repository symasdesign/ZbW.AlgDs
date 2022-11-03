using System;
using System.IO;
using System.Text;

namespace SpellCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            var output = Console.In;

            Console.InputEncoding = Encoding.Default;
            var spelling = new Spelling(Console.In);

            var color = Console.ForegroundColor;

            using (var stream = new StreamReader(args[0], Encoding.Default))
            {
                string text = stream.ReadToEnd();
                
                foreach (Spelling.Word word in spelling.CheckText(text))
                {
                    if (!word.IsCorrect)
                        Console.ForegroundColor = ConsoleColor.Red;

                    Console.Write(word + " ");
                    Console.ForegroundColor = color;
                }
            }
        }
    }
}
