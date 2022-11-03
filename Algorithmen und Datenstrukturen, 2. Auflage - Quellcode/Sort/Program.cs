using System;
using My.Collections;
using System.Diagnostics;

namespace Sort
{
    class Program
    {
        private delegate void delegates();

        static void Main(string[] args)
        {
            var list = new SortedArrayList<int>();

            list.AddRange(new int[] { 9,4,8,7,6,4,2,9,4,5 });
            list.BucketSort();
            Console.WriteLine(list);

            delegates[] funcs = new delegates[] 
                            { 
                               new delegates(list.BubbleSort), 
                               new delegates(list.InsertionSort), 
                               new delegates(list.QuickSort), 
                               new delegates(list.BucketSort), 
                               new delegates(list.MergeSort)
                            };

            var w = new Stopwatch();

            foreach(var func in funcs)
            {
                list.AddRange(new int[] { 8, 7, 3, 5, 9, 4, 1 });

                w.Restart();
                func();
                w.Stop();

                Console.WriteLine(func.Method.ToString() + ": " + list.ToString());
                Console.WriteLine(w.ElapsedTicks);
                list.Clear();
            }
            Console.ReadLine();
        }
    }
}
