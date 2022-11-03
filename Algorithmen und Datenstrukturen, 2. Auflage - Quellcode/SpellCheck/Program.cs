using My.Collections;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace SpellCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            //1. Wörterbuch über http 
            //
            //var request = WebRequest.Create("https://jvpksa.dm2303.livefilestore.com/y2mC5PJrlcnJJ6LCWcvpCR5qp5lUnOMyb6zfkrxUyCP22Op3Bab-nnen38GjpXge-FFIyYgLHYsEw32S_u_3f7CghnO5A3ttj_a7IVBMMWDeuRyBxwxguR1TYxcl4WUyFuP/german.dic?download&psid=1");
            //var response = request.GetResponse();
            //var streamDict = response.GetResponseStream();
            //var spelling = new Spelling(new StreamReader(streamDict, System.Text.Encoding.Default));

            //2. Wörterbuch als Datei lesen
            //
            //var spelling = new Spelling(new StreamReader(@"..\Testdaten\german.dic", Encoding.Default));

            //3. Wörterbuch über Eingabeumleitung 
            //
            Console.InputEncoding = Encoding.Default;
            var spelling = new Spelling(Console.In);

            var color = Console.ForegroundColor;

            string text = File.ReadAllText(args[0], Encoding.Default);
                
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
