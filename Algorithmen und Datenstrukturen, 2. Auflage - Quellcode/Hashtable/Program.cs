using System;
using My.Collections;


namespace HashTable
{
    class Program
    {
        static void Main(string[] args)
        {
            var h = new Hashtable<int, string>(10000);

            var random = new Random();

            for (int i = 0; i <= 100000; i++)
            {
                int n = random.Next(1, 100000);
                h[n] = "Zahl=" + n;
            }

            foreach (var value in h.Values())
            {
                Console.Write(value + " ");
            }

            Console.WriteLine("Load factor: " + h.LoadFactor);
            Console.WriteLine("Occupation factor: " + h.OccupationFactor);
            Console.WriteLine("Anzahl: " + h.Count);        // Anzahl != 100.000, da gleiche Zufallszahl als Key nur den Wert verändert

            h.Clear(); 
        }
    }
}
