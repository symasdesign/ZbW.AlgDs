using DataStructure;
using System.Diagnostics;

namespace MainProgram
{
    internal class Program
    {
        public static void Main()
        {
            // Lingly linked list
            Testing_SinglyLinkedList();

            // Stack
            Testing_Stack();

            // Queue
            Testing_Queue();
        }
        public static void Testing_SinglyLinkedList()
        {
            Console.WriteLine("Singly linked list:");
            SinglyLinkedList list = new SinglyLinkedList();
            list.Add(10);
            list.Add(20);
            list.Add(30);
            list.Add(40);

            Console.WriteLine(list);
            list.Remove(40);
            list.Remove(20);
            list.Remove(10);
            Console.WriteLine(list);
        }
        private static void Testing_Stack()
        {
            Console.WriteLine();
            Console.WriteLine("Stack:");
            Stack stack = new Stack();

            stack.Push(10);
            stack.Push(20);
            stack.Push(30);
            stack.Push(40);

            Console.WriteLine(stack);

            Debug.Assert((int)stack.Peek() == 40);
            stack.Pop();
            Debug.Assert((int)stack.Peek() == 30);
            stack.Pop();
            Debug.Assert((int)stack.Peek() == 20);
            stack.Pop();
            Debug.Assert((int)stack.Peek() == 10);

            Console.WriteLine(stack);
        }
        private static void Testing_Queue()
        {
            Console.WriteLine();
            Console.WriteLine("Stack:");
            Queue queue = new Queue();

            queue.Enqueue(10);
            queue.Enqueue(20);
            queue.Enqueue(30);
            queue.Enqueue(40);

            Console.WriteLine(queue);

            Debug.Assert((int)queue.Peek() == 10);
            queue.Dequeue();
            Debug.Assert((int)queue.Peek() == 20);
            queue.Dequeue();
            Debug.Assert((int)queue.Peek() == 30);
            queue.Dequeue();
            Debug.Assert((int)queue.Peek() == 40);

            Console.WriteLine(queue);
        }
    }
}