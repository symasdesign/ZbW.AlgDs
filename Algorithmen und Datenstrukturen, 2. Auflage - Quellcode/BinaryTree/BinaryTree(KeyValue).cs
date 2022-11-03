using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public enum TraverseModeEnum { PreOrder, PostOrder, InOrder };

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
        public TraverseModeEnum TraverseMode { get; set; }

        public void Add(TKey key, TValue value)
        {
            if (root == null)
                root = new Node() { Key = key, Value = value};
            else
                addTo(root, key, value);

            Count++;
        }

        private void addTo(Node node, TKey key, TValue value)
        {
            if (key.CompareTo(node.Key) < 0)
            {
                if (node.Left == null)
                    node.Left = new Node() { Key = key, Value = value };
                else
                    addTo(node.Left, key, value);
            }
            else
            {
                if (node.Right == null)
                    node.Right = new Node() { Key = key, Value = value };
                else
                    addTo(node.Right, key, value);
            }
        }

        public TValue Search(TKey key)
        {
            Node node = root;

            while (node != null)
            {
                int c = node.Key.CompareTo(key);

                if (c == 0)
                    return node.Value;

                if (c > 0)
                    node = node.Left;
                else
                    node = node.Right;
            }
            return default(TValue);
        }

        public bool Contains(TKey key)		// selbst programmieren
        {
            return Search(key).Equals(default(TValue)) == false;
        }

        public override string ToString()
        {
            string s = "";

            s += traverse(root);

            return s;
        }

        private string traverse(Node node)
        {
            if (node == null)
                return "";

            string s = "";
            if (TraverseMode == TraverseModeEnum.PreOrder)
            {
                s = node.Key.ToString() + "->";
                s += traverse(node.Left);
                s += traverse(node.Right);
            }
            if (TraverseMode == TraverseModeEnum.InOrder)
            {
                s = traverse(node.Left);
                s += node.Key.ToString() + "->";
                s += traverse(node.Right);
            }
            return s;
        }
    }
}

