using System;

namespace My.Collections
{
    public enum TraverseModeEnum { PreOrder, PostOrder, InOrder, ReverseInOrder }

    public class BinaryTree<TKey, TValue> where TKey : IComparable<TKey>
    {
        private class Node
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
        }

        private Node root;

        public int Count { get; private set; }

        public TKey Min
        {
            get
            {
                if (Count == 0)
                    throw new ArgumentException("No items in tree.");

                Node node = root;

                TKey min = root.Key;

                while (node != null)
                {
                    min = node.Key;
                    node = node.Left;
                }
                return min;
            }
        }

        public TKey Max
        {
            get
            {
                if (Count == 0)
                    throw new ArgumentException("No items in tree.");

                Node node = root;

                TKey max = root.Key;

                while (node != null)
                {
                    max = node.Key;
                    node = node.Right;
                }
                return max;
            }
        }

        public TraverseModeEnum TraverseMode { get; set; }

        public BinaryTree()
        {
            TraverseMode = TraverseModeEnum.PreOrder;
        }

        public void Add(TKey key, TValue value)
        {
            if (root == null)
                root = new Node() { Key = key, Value = value };
            else
                AddTo(root, key, value);

            Count++;
        }

        public void AddIterative(TKey key, TValue value)
        {
            if (root == null)
            {
                root = new Node() { Key = key, Value = value };
                return;
            }

            var node = root;
            while (node != null)
            {
                if (key.CompareTo(node.Key) < 0)
                {
                    if (node.Left == null)
                    {
                        node.Left = new Node() { Key = key, Value = value };
                        break;
                    }
                    node = node.Left;
                }
                else
                {
                    if (node.Right == null)
                    {
                        node.Right = new Node() { Key = key, Value = value };
                        break;
                    }
                    node = node.Right;
                }
            }

            Count++;
        }

        private void AddTo(Node node, TKey key, TValue value)
        {
            if (key.CompareTo(node.Key) < 0)
            {
                if (node.Left == null)
                    node.Left = new Node() { Key = key, Value = value };
                else
                    AddTo(node.Left, key, value);
            }
            else
            {
                if (node.Right == null)
                    node.Right = new Node() { Key = key, Value = value };
                else
                    AddTo(node.Right, key, value);
            }
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

        public void Clear()
        {
            root = null;
            Count = 0;
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

            if (TraverseMode == TraverseModeEnum.PreOrder)
            {
                s += "".PadLeft(level, ' ') + node.Key.ToString() + "\n";
                Traverse(node.Left, level + 2, ref s);
                Traverse(node.Right, level + 2, ref s);
            }
            if (TraverseMode == TraverseModeEnum.PostOrder)
            {
                Traverse(node.Left, level + 2, ref s);
                Traverse(node.Right, level + 2, ref s);
                s += "".PadLeft(level, ' ') + node.Key.ToString() + "\n";
            }
            if (TraverseMode == TraverseModeEnum.InOrder)
            {
                Traverse(node.Left, level + 2, ref s);
                s += "".PadLeft(level, ' ') + node.Key.ToString() + "\n";
                Traverse(node.Right, level + 2, ref s);
            }
            if (TraverseMode == TraverseModeEnum.ReverseInOrder)
            {
                Traverse(node.Right, level + 2, ref s);
                s += "".PadLeft(level, ' ') + node.Key.ToString() + "\n";
                Traverse(node.Left, level + 2, ref s);
            }
        }
    }
}