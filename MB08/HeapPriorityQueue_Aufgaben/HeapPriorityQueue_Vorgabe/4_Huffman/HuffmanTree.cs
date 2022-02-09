using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinMaxHeap;

namespace _4_Huffman {
    public class HuffmanTree {
        public HuffmanTree(ICollection<Entry> frequencies) {
            // TODO: implement here the Huffman-Tree

            throw new NotImplementedException();
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
            // TODO: implement here

            throw new NotImplementedException();
        }
    }
}
