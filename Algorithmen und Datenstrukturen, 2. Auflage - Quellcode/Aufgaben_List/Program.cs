using System;
using System.Collections.Generic;
using My.Collections;

namespace Aufgaben_List
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new ArrayList<int>();

            // 2.2.3 Aufgaben
            // 1) 
            list.AddRange(new[] { 1, 23, 4, 12, 22, 42 });

            // 2) 
            while (list.Count > 0)
            {
                list.RemoveAt(0);
            }
            list.AddRange(new[] { 1, 23, 4, 12, 22, 42 });

            // 3)
            var listCopy = new ArrayList<int>(list);
            foreach (int item in listCopy)
            {
                Console.WriteLine(item);
            }

            // 4) null muss explizit in den Methoden berücksichtigt werden
            // 5) int kann kein null sein. Lösung: new ArrayList<int?>()

            // 6)
            var doubleList = new DoublyLinkedList<int>();
            doubleList.AddRange(new[] { 1, 23, 4, 12, 22, 42 });

            doubleList.RemoveAfter(23);

            // 7)
            foreach (double d in Pow(2, 16))
            {
                Console.WriteLine(d);
            }

            // 9)
            list.InsertAt(1, 3);
            list.InsertAt(6, 99);
            list.InsertAt(0, 0);

            doubleList.InsertAt(2, 4);
            doubleList.InsertAt(1, 3);
            doubleList.InsertAt(6, 99);
            doubleList.InsertAt(0, 0);

            // 10)
            // ArrayList: O(1), O(1) im Average Case (Array muss nicht vergrößert werden)
            //            bei Umkopieren Array im Worst Case O(n), wenn an Position 0 eingefügt wird
            // LinkedList: O(n) im Worst/Average Case

            // 11)
            // Die Elemente werden zweimal iteriert, einmal zum Finden des Knotenelements und 
            // ein weiteres mal zum Finden des vorherigen Elements, also O(2n). Beim Suchen des Elementes in der 
            // Find-Methode sollte bereits das vorherige Element gemerkt werden, dann O(n). 
            // Alternative: Doppelt verkettete Liste.

            // 12) siehe Projekt "2 Datenstrukturen\2.2 List\7) VisualStudioProfiler"
        }

        private static IEnumerable<double> Pow(double x, int y)
        {
            for (double i = 1.0; i <= y; i++)
            {
                yield return Math.Pow(x, i);
            }
        }
    }
}
