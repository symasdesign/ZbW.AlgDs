using System;
using My.Collections;

namespace Stack
{
    class Program
    {
        static void Main(string[] args)
        {
            var stack = new Stack<int>();

            for (int i = 1; i < 100000; i++ )
            { 
                stack.Push(i);
            }

            while(stack.Count > 0)
            {
                Console.WriteLine(stack.Pop());
            }
               
        }
    }
}
