using System;

namespace _4_Huffman {
    static class Program {
        static void Main(string[] args) {
            var text = "";
            if (args.Length > 0) {
                foreach (var arg in args) {
                    text += $" {arg}";
                }

                text = text.Trim();
            } else {
                text = "Bildung laesst sich nicht downloaden.";
            }

            // build the counter
            var fc = new Frequency();
            var frequencies = fc.CountFrequency(text);
            var originalBitCount = 0;
            foreach (var freq in frequencies) {
                originalBitCount += freq.Key * 8;
            }

            // build the Huffman-Tree
            var tree = new HuffmanTree(frequencies);

            // assign the bits
            var bits = new char[frequencies.Count];
            var bitCount = HuffmanTree.AssignBits(tree.Root, bits, 0);

            // output
            Console.WriteLine($"Compression: {(100-100*bitCount/(double)originalBitCount)}%");

            Console.ReadKey();
        }

        /*
         * Ausgabe für "MISSISSIPPI":
         *
         *   4: I: 0
         *   1: M: 100
         *   2: P: 101
         *   4: S: 11
         * Wenn 1 Char = 1 Byte: Compression: 76.1363636363636%
         */


        /*
         * Ausgabe für "Bildung laesst sich nicht downloaden.":
         *
         *  2: a: 0000
         *  2: e: 0001
         *  4:  : 001
         *  4: n: 010
         *  2: c: 0110
         *  2: h: 0111
         *  2: o: 1000
         *  2: t: 1001
         *  1: .: 10100
         *  1: B: 10101
         *  3: d: 1011
         *  3: s: 1100
         *  1: w: 11010
         *  1: g: 110110
         *  1: u: 110111
         *  3: l: 1110 1
         *  3: i: 1111 1
         * Wenn 1 Char = 1 Byte: Compression: 50.3378378378378%
         */
    }
}
