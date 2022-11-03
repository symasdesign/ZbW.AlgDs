using System;
using My.Collections;


namespace HashTable
{
    class Program
    {
        static void Main(string[] args)
        {
            var h = new HashtableDoubleHashing<int, string>(10);

            h[1] = "Hinzufügen/Ändern ohne Erzeugen Ausnahme";

            for (int i = 1; i <= 10; i++)
            {
                // Kollisionen verursachen
                h.Add(i % 2 == 0 ? i : i-1 + 100, i.ToString());
            }

            Console.WriteLine(h.ContainsKey(1));
            Console.WriteLine(h.Remove(1));

            h.Clear(); 
        }
    }
}
