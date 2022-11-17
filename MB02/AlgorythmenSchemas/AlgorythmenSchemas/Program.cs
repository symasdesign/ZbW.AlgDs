using System.Drawing;
using System.Runtime.ExceptionServices;

namespace AlgorythmenSchemas
{
    
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // Aufgabe 1
            string str = "Hello";
            Console.WriteLine($"String reverse: {str} -> {StringReverse("Hello Jana")}");
            Console.WriteLine();

            // Aufgabe 2
            int number = 40;
            Console.WriteLine($"Fibonacci iterative: {number}. -> {FibonacciItterative(number)}");
            Console.WriteLine();

            // Aufgabe 3
            Console.WriteLine("Towers of hanoi:");
            TowersOfHanoi(4, "A", "B", "C");

            // Aufgabe 4
            
            Benchmark benchmarkFibonacciIterative = new Benchmark(10000, BenchmarkableFibonacciItrative, "Fibonacci itterative");
            benchmarkFibonacciIterative.Start();

            Benchmark benchmarkFibonacciRecursive = new Benchmark(40, BenchmarkableFibonacciRecursive, "Fibonacci recursive");
            benchmarkFibonacciRecursive.Start();
            
            Benchmark benchmarkFibonacciRecursiveOptimized = new Benchmark(40, BenchmarkableFibonacciRecursiveOptimized, "Fibonacci recursive opitmized");
            benchmarkFibonacciRecursiveOptimized.Start();
            
            Benchmark benchmarkTowersOfHanoi = new Benchmark(15, BenchmarkableTowersOfHanoi, "Towers of Hanoi");
            benchmarkTowersOfHanoi.Start();

            Console.WriteLine();

            // Aufgabe 5
            Console.WriteLine("Recursive complexity 1:");
            for (int i = 1; i < 20; i++)
            {
                int amount = (int)Math.Pow(2, i);
                totalFunctionCalls = 0;
                procRec1(amount);
                Console.WriteLine($"Total function calls with {amount} elements : {totalFunctionCalls}");
            }
            Console.WriteLine();

            // Aufgabe 6
            Console.WriteLine("Recursive complexity 1:");
            for (int i = 1; i < 20; i++)
            {
                int amount = (int)Math.Pow(2, i);
                totalFunctionCalls = 0;
                procRec2(amount, 10);
                Console.WriteLine($"Total function calls with {amount} elements : {totalFunctionCalls}");
            }
            Console.WriteLine();
        }

        // Aufgabe 1

        /*
         * Algorythm: (String reverse)
         *  - return if string is not longer than 1 character
         *  - getting the first character
         *  - recursively calling the function with de rest of the string
         *  - return the last caracter + the recursive call
         */

        private static string StringReverse(string str)                     // O(n)
        {
            if (str == null)                                                // O(1)
                return "";                                                  // O(1)
            if (str.Length <= 1)                                            // O(1)
                return str;                                                 // O(1)

            return StringReverse(str.Substring(1)) + str.Substring(0, 1);   // O(n)
        }

        // Aufgabe 2
        
        /*
         * Algorythm: (Fibonacci Iterative)
         *  - loop over the numbers from 1 - n
         *      - add the current and the previous to a total
         *      - move the current into the previous
         *      - move the total into the current
         *  - return the current
         */

        private static long FibonacciItterative(int number) // O(n)
        {
            if (number <= 0)                                // O(1)
                return 0;                                   // O(1)

            long current = 1, previous = 0;                 // O(1)
            long next;                                      // O(1)
            for (int i = 0; i < number; i++)                // O(n)
            {
                next = current + previous;                  // O(1)
                previous = current;                         // O(1)
                current = next;                             // O(1)
            }
            return current;                                 // O(1)
        }

        /*
         * Algorythm: (FibonacciRecursive)
         *  - if count <= 0 return
         *  - calcolate Fibonacci recursive of count - 1 and count - 2
         *  - return the sum of them
         */

        private static long FibonacciRecursive(int number)                          // O(2^n)
        {
            if (number <= 0)                                                        // O(1)
                return 0;                                                           // O(1)

            return FibonacciRecursive(number - 1) + FibonacciRecursive(number - 2); // O(2^n)
        }

        private static long FibonacciRecursiveOptimized(int number) // O(n)
        {
            long[] data = new long[number];                         // O(1)
            return FibonacciRecursiveOptimized(number, data);       // O(n)
        }
        private static long FibonacciRecursiveOptimized(int number, long[] data)    // O(n)
        {
            number--;                                                               // O(1)

            if (number <= 1)                                                        // O(1)
                return 1;                                                           // O(1)

            if (data[number] == 0)                                                  // O(1)
                data[number] = FibonacciRecursiveOptimized(number, data);           // O(n)

            if (data[number - 1] == 0)                                              // O(n)
                data[number - 1] = FibonacciRecursiveOptimized(number - 1, data);   // O(n)

            return data[number] + data[number - 1];                                 // O(1)
        }

        // Aufgabe 3

        /*
         * Algorythm: (Türme von Hanoi)
         *  - if the size == 1
         *      - move it from the current to the target
         *      - return
         *  - move all recursivly from the current to the free, exept for the most bottom one
         *  - move the most bottom one from the current to the target
         *  - move all the ones from the free to the target
         */

        private static void TowersOfHanoi(int size, string current = "A", string target = "B", string free = "C")   // O(2^n)
        {
            if (size == 1)                                                                                          // O(1)
            {
                Console.WriteLine($"Move from {current} -> {target} ms");                                           // O(1)
                return;                                                                                             // O(1)
            }

            TowersOfHanoi(size - 1, current, free, target);                                                         // O(2^n)
            TowersOfHanoi(1, current, target, free);                                                                // O(1)
            TowersOfHanoi(size - 1, free, target, current);                                                         // O(2^n)
        }

        // Aufgabe 4


        private static TimeSpan BenchmarkableFibonacciItrative(int size)
        {
            Timer timer = new Timer();
            timer.Start();
            FibonacciItterative(size);
            return timer.End();
        }

        private static TimeSpan BenchmarkableFibonacciRecursive(int size)
        {
            Timer timer = new Timer();
            timer.Start();
            FibonacciRecursive(size);
            return timer.End();
        }

        private static TimeSpan BenchmarkableFibonacciRecursiveOptimized(int size)
        {
            Timer timer = new Timer();
            timer.Start();
            FibonacciRecursiveOptimized(size);
            return timer.End();
        }

        private static TimeSpan BenchmarkableTowersOfHanoi(int size)
        {
            Timer timer = new Timer();
            timer.Start();
            TowersOfHanoi(size);
            return timer.End();
        }

        internal class Timer
        {
            DateTime startTime;
            DateTime endTime;

            public void Start()
            {
                startTime = DateTime.Now;
            }
            public TimeSpan End()
            {
                endTime = DateTime.Now;
                return endTime - startTime;
            }
        }

        internal class Benchmark
        {

            public Benchmark(int size, Func<int, TimeSpan> func, string name)
            {
                m_results = new TimeSpan[size];
                m_func = func;
                m_name = name;
            }

            public void Start()
            {
                Console.WriteLine("********************************************************************");
                Console.WriteLine($"***** Benchmark {m_name} start");
                Console.WriteLine("********************************************************************");

                Console.WriteLine("Testing ...");
                for (int i = 0; i < m_results.Length; i++)
                {
                    m_results[i] = m_func(i + 1);
                }

                Console.WriteLine("Results:");
                for (int i = 0; i < m_results.Length; i++)
                {
                    Console.WriteLine($"{i + 1}: {m_results[i].TotalMilliseconds}");
                }

                Console.WriteLine("********************************************************************");
                Console.WriteLine($"***** Benchmark {m_name} end");
                Console.WriteLine("********************************************************************");
            }
            private TimeSpan[] m_results;
            private Func<int, TimeSpan> m_func;
            private string m_name;
        }

        // Aufgabe 5

        private static void procRec1(int n) // O(log n)
        {
            if (n <= 1)                     // O(1)
            {
                return;                     // O(1)
            }
            CountFunctionCall(n);           // O(1)
            procRec1(n / 2);                // O(log n)
        }

        private static int totalFunctionCalls = 0;
        private static void CountFunctionCall(int n)
        {
            totalFunctionCalls++;
        }

        // Aufgabe 6

        private static int procRec2(int n, int res)    // O(2^(log n)) -> O(n)
        {
            CountFunctionCall(n);                       // O(1)
            if (n <= 1)                                 // O(1)
            {
                return res;                             // O(1)
            }
            res = procRec2(n / 2, res);                 // O(log n)
            res = procRec2(n / 2, res);                 // O(log n)
            return res;                                 // O(1)
        }
    }
}