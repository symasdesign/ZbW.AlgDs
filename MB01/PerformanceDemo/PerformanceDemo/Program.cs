using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Performance {
    class Program {
        static void Main(string[] args) {
            Loop(100);

            CreateAllPairs(100);

            var haystack = new List<int> { 2, 4, 4, 5, 7, 10, 23, 25, 64 };

            ContainsNeedle(64, haystack);

            // Executing a binary search:
            var pos = BinarySearch(haystack, 64, 0, haystack.Count - 1);
            Console.WriteLine(pos == null ? "not found" : "Found at position: " + pos);
            Console.ReadLine();
        }

        /// <summary>
        /// Performs the number of iterations given by the input parameter.
        /// Complexity: O(N)
        /// </summary>
        /// <param name="N">Number of loops</param>
        internal static void Loop(int N) {
            var counter = 0;
            while (counter < N) {
                Console.WriteLine(counter);
                counter = counter + 1;
            }
        }

        /// <summary>
        /// Creates all pairs on the form:
        ///     0, 0
        ///     0, 1
        ///     0, 2
        ///     ...
        ///     N-1, N-1
        /// Complexity: O(N*N)
        /// </summary>
        /// <param name="N">Number of different values per coordinate.</param>
        internal static void CreateAllPairs(int N) {
            var x = 0;
            var y = 0;
            while (x < N) {
                while (y < N) {
                    Console.WriteLine("{0}, {1}", x, y);
                    y = y + 1;
                }
                x = x + 1;
                y = 0;
            }
        }


        /// <summary>
        /// A search algorithm that scans though all items in a list.
        /// Complexity: O(N) where N is the number of elements in the haystack.
        /// </summary>
        /// <param name="needle">Value to look for.</param>
        /// <param name="haystack">The container holding the elements to search.</param>
        /// <returns></returns>
        internal static bool ContainsNeedle(int needle, List<int> haystack) {
            foreach (var sample in haystack) {
                if (sample == needle)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// A binary search algorithm that works on an ORDERED haystack,
        /// and finds the needle by iteratively cutting away half of the
        /// haystack that does not contain the needle.
        /// Complexity: O(log N) where N is the size of the haystack.
        /// </summary>
        /// <param name="haystack">The container holding the elements to search. Must be sorted.</param>
        /// <param name="needle">The value to search for</param>
        /// <param name="min">Initially, 0</param>
        /// <param name="max">Initially, the size of the haystack</param>
        /// <returns></returns>
        internal static int? BinarySearch(List<int> haystack, int needle, int min, int max) {
            var midpoint = (max + min) / 2;
            if (haystack.Count > 0 && haystack[midpoint] == needle)
                return midpoint;
            if (min >= max)
                return null;
            if (haystack[midpoint] > needle)
                return BinarySearch(haystack, needle, min, midpoint - 1);
            return BinarySearch(haystack, needle, midpoint + 1, max);
        }
    }
}
