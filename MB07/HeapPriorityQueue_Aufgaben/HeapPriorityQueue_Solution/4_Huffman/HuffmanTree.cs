using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinMaxHeap;

namespace _4_Huffman {
    public class HuffmanTree {
        public HuffmanTree(ICollection<Entry> frequencies) {
            MinHeap priorityQueue = new MinHeap();
            foreach (var freq in frequencies) {
                priorityQueue.Add(new Node(freq));
            }

            while (priorityQueue.Size > 1) {
                var a = (Node)priorityQueue.Pop();
                var b = (Node)priorityQueue.Pop();
                var value = a.Value.Key + b.Value.Key;
                var n = new Node(new Entry(value, null), a, b);
                priorityQueue.Add(n);
            }

            this.Root = (Node)priorityQueue.Pop();
        }

        public Node Root { get; }

        /// <summary>
        /// Recursively traverses the tree and determines the Huffman-Bitcode.
        /// </summary>
        /// <param name="n">The current node of the tree.</param>
        /// <param name="bits">char-array containing the actual bitcode so far.</param>
        /// <param name="nBits">The length of the actual bitcode.</param>
        /// <returns>Length of the whole code.</returns>
        public static int AssignBits(Node n, char[] bits, int nBits) {
            int bitCount;
            if (n.LeftChild == null && n.RightChild == null) {
                bits[nBits] = '\0';
                bitCount = nBits * n.Value.Key;
                Console.WriteLine($"{n.Value.Key,2}: {n.Value.Value}: {new string(bits)}");
            } else {
                bits[nBits] = '0';
                bitCount = AssignBits(n.LeftChild, bits, nBits + 1);
                bits[nBits] = '1';
                bitCount += AssignBits(n.RightChild, bits, nBits + 1);
            }
            return bitCount;
        }
    }
}
