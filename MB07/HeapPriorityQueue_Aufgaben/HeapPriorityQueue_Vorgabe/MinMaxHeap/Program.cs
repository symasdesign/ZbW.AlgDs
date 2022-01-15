using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinMaxHeap {
    class Program {
        static void Main(string[] args) {
            int[] a = new int[] { 2, 4, 5, 1, 6, 7, 8 };
            var maxHeap = new MaxHeap(a);
            maxHeap.PrintHeap();
            a = new int[] { 15, 72, 49, 79, 39, 3, 43, 89, 18 };
            maxHeap = new MaxHeap(a);
            maxHeap.PrintHeap();

             a = new int[] { 2, 4, 5, 1, 6, 7, 8 };
            var minHeap = new MinHeap(a);
            minHeap.PrintHeap();
            a = new int[] { 15, 72, 49, 79, 39, 3, 43, 89, 18 };
            minHeap = new MinHeap(a);
            minHeap.PrintHeap();

        }
    }
}
