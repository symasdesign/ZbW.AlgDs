using System;
using My.Collections;

namespace DoublyLinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new DoublyLinkedList<string>();
           
            list.Add("Hans Muster");
            list.Add("Berta Müller");
            list.Add("Kurt Schmidt");

            list.InsertAfter("Hans Muster", "Frieda Roth");

            Console.WriteLine(list);

            list.Remove2("Berta Müller");
            Console.WriteLine(list);
            list.Remove2("Hans Muster");
            Console.WriteLine(list);
            list.Remove2("Kurt Schmidt");
            Console.WriteLine(list);

            list.Clear();
        }
    }
}
