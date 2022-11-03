using My.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgaben_Queue
{
    class Program
    {
        static void Main(string[] args)
        {
            // Aufgaben
            // 1) 
            var queue = new QueueWithArrayList<int>();

            for (int i = 1; i < 10; i++)
                queue.Enqueue(i);

             while (queue.Count > 0)
                Console.WriteLine(queue.Dequeue());

            for (int i = 1; i < 6; i++)
                queue.Enqueue(i);

            Console.WriteLine(queue.Dequeue());
            Console.WriteLine(queue.Dequeue());
            Console.WriteLine(queue.Dequeue());
            queue.Enqueue(100);
            Console.WriteLine(queue.Dequeue());
            queue.Enqueue(101);
            Console.WriteLine(queue.Dequeue());

            while (queue.Count > 0)
                Console.WriteLine(queue.Dequeue());

            // 2)
            var dequeue = new Dequeue<int>();

            dequeue.AddFirst(2);
            dequeue.AddFirst(1);
            dequeue.AddLast(3);
            dequeue.AddLast(4);
            Console.WriteLine(dequeue.PeekFirst());
            Console.WriteLine(dequeue.PeekLast());
            Console.WriteLine(dequeue.RemoveFirst());
            Console.WriteLine(dequeue.RemoveLast());
            while (dequeue.Count > 0)
                Console.WriteLine(dequeue.RemoveLast());

            // 4) 
            new DatabaseServer(7777, @"c:\DBServer").Start();
        }
    }
}
