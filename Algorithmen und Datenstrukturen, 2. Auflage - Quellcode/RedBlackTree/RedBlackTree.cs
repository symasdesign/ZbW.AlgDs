using System;

namespace My.Collections
{
    [Serializable()]
    public class RedBlackTree<TKey, TValue> where TKey : IComparable<TKey>
    {
        [Serializable()]
        private class Node
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public bool IsRed { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
        }

        private Node root;

        public int Count { get; private set; }

        public void Add(TKey key, TValue value)
        {
            root = AddTo(root, key, value);
            root.IsRed = false;

            Count++;
        }

        public bool Contains(TKey key)
        {
            Node node = root;

            while (node != null)
            {
                int c = key.CompareTo(node.Key);

                if (c == 0)
                    return true;

                if (c < 0)
                    node = node.Left;
                else
                    node = node.Right;
            }
            return false;
        }

        public TValue this[TKey key]
        {
            get
            {
                Node node = root;

                while (node != null)
                {
                    int c = key.CompareTo(node.Key);

                    if (c == 0)
                        return node.Value;

                    if (c < 0)
                        node = node.Left;
                    else
                        node = node.Right;
                }
                throw new ArgumentException("Key does'nt exist.");
            }
        }

        private Node AddTo(Node node, TKey key, TValue value)
        {
            if (node == null)
                return new Node() { Key = key, Value = value, IsRed = true };

            int cmp = key.CompareTo(node.Key);
            if (cmp < 0)
                node.Left = AddTo(node.Left, key, value);
            else if (cmp > 0)
                node.Right = AddTo(node.Right, key, value);
            else
                node.Value = value;

            if (IsRed(node.Right) && !IsRed(node.Left))     // Regel für Linkslastigkeit
                node = RotateLeft(node);

            if (IsRed(node.Left) && IsRed(node.Left.Left))  // Keine aufeinanderfolgenden roten Knoten
                node = RotateRight(node);

            if (IsRed(node.Left) && IsRed(node.Right))      // keine zwei roten Kindknoten
                FlipColors(node);

            return node;
        }

        private Node RotateRight(Node node)
        {
            Node newParent = node.Left;

            node.Left = newParent.Right;
            newParent.Right = node;
            newParent.IsRed = newParent.Right.IsRed;
            newParent.Right.IsRed = true;

            return newParent;
        }

        private Node RotateLeft(Node node)
        {
            Node newParent = node.Right;

            node.Right = newParent.Left;
            newParent.Left = node;
            newParent.IsRed = newParent.Left.IsRed;
            newParent.Left.IsRed = true;

            return newParent;
        }

        private void FlipColors(Node node)
        {
            node.IsRed = !node.IsRed;
            node.Left.IsRed = !node.IsRed;
            node.Right.IsRed = !node.IsRed;
        }

        private bool IsRed(Node node)
        {
            if (node == null)   // Null-Verweise stellen schwarze Blätter dar
                return false;

            return node.IsRed;
        }

        public override string ToString()
        {
            string s = "";
            int level = 0;

            Traverse(root, level, ref s);

            return s;
        }

        private void Traverse(Node node, int level, ref string s)
        {
            if (node == null)
                return;

            string color = node.IsRed ? "red" : "black";
            s += "".PadLeft(level, ' ') + $"{node.Key} ({color})\n";
            Traverse(node.Left, level + 2, ref s);
            Traverse(node.Right, level + 2, ref s);
        }
    }
}
