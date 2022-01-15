using System;

namespace _4_Huffman {
    public class Node : IComparable {

        public Node(Entry val) {
            this.Value = val;
        }

        public Node(Entry val, Node l, Node r) : this(val) {
            this.LeftChild = l;
            this.RightChild = r;
        }

        public Entry Value { get; }
        public Node LeftChild { get; }
        public Node RightChild { get; }
        public int CompareTo(object obj) {
            var other = obj as Node;
            if (other == null) {
                return -1;
            }

            return this.Value.Key.CompareTo(other.Value.Key);
        }
    }
}
