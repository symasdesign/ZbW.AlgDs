using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineareDatenstrukturen {
    class Program {
        static void Main(string[] args) {
            var singlyLinkedList = new SinglyLinkedList();
            singlyLinkedList.Add("aaa");
            singlyLinkedList.Add("bbb");
            singlyLinkedList.Add(5);
            var x = singlyLinkedList[1];

            singlyLinkedList[1] = "xyz";

            var c = singlyLinkedList.Contains("bb");
            var o = singlyLinkedList.FindByIndex(1);
            var cnt = singlyLinkedList.Count;
            singlyLinkedList.Remove("bbb");
            cnt = singlyLinkedList.Count;

            //--------------------------------------

            var doublyLinkedList = new DoublyLinkedList();
            doublyLinkedList.Add("aaa");
            doublyLinkedList.Add("bbb");
            doublyLinkedList.Add(5);
            o = doublyLinkedList.FindByIndex(2);
            doublyLinkedList.InsertAfter("bbb", "ccc");
            o = doublyLinkedList.FindByIndex(2);
            cnt = doublyLinkedList.Count;
            doublyLinkedList.Remove("bbb");
            cnt = doublyLinkedList.Count;

            //--------------------------------------

            var stack = new Stack<int>();

            for (int i = 1; i < 100; i++) {
                stack.Push(i);
            }

            while (stack.Count > 0) {
                Console.WriteLine(stack.Pop());
            }

            //--------------------------------------

            var queue = new Queue<int>();
            //var queue = new QueueCircularBuffer<int>();

            for (int i = 1; i < 16; i++)
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
        }
    }
}
