using System;
using My.Collections;


namespace HashTable
{
    class Program
    {
        static void Main(string[] args)
        {
            var h = new HashtableLinearProbing<int, string>();

            for (int i = 1; i <= 21; i+=2)
            {
                // Kollisionen verursachen
                h.Add(i, i.ToString());
            }

            h[1] = "Hinzufügen/Ändern ohne Erzeugen Ausnahme";

            for (int i = 1; i <= 10; i++)
            {
                // Kollisionen verursachen
                h.Add(i % 2 == 0 ? i : i - 1 + 100, i.ToString());
            }

            Console.WriteLine("Gelöscht? " + h.Remove(0));
            Console.WriteLine("Vorhanden? " + h.ContainsKey(0));
            Console.WriteLine("Vorhanden? " + h.ContainsKey(1));
            Console.WriteLine("Gelöscht? " + h.Remove(1));

            h.Clear(); 
        }
    }
}
