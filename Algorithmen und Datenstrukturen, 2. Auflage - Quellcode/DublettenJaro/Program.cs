using System;
using System.IO;
using System.Text;

namespace DublettenJaro
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
                throw new ArgumentException("Aufruf: DublettenJaro <file.csv>");

            // CSV einlesen
            var duplicates = new DuplicateRecognitionJaro(new StreamReader(args[0], Encoding.Default));

            duplicates.Recognize(0.60);

            foreach(var d in duplicates.Duplicates)
                Console.WriteLine(d);
        }
    }
}
