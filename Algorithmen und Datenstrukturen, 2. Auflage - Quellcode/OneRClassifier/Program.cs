using System;
using System.IO;
using System.Text;

namespace OneRClassfier
{
    class Program
    {
        static void Main(string[] args)
        {
            var oneR = new OneR();

            oneR.Build(new StreamReader(args[0], Encoding.Default));

            Console.WriteLine("\nGültigen Wert zur Vorhersage eingeben (z.B. sonnig): ");

            string value;
            while ((value = Console.ReadLine()) != "")
            {
                var predicted = oneR.Classify(value);

                Console.WriteLine("Vorhersage: {0}", predicted ?? "<unbekannt>");
            }
        }
    }
}
