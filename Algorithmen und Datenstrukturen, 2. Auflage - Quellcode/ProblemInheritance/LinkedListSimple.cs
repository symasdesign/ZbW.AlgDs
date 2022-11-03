using System;

namespace My.Collections
{
    public class LinkedListSimple
    {
        private class Node
        {
            public Object Item { get; set; }
            public Node Next { get; set; }
        }

        private Node first, last;

        public int Count { get; private set; }

        public virtual void Add(Object item)
        {
            Node newItem = new Node() { Item = item, Next = null };

            if (first == null)
            {
                first = newItem;
                last = newItem;
            }
            else
            {
                last.Next = newItem;
                last = newItem;
            }
            Count++;
        }

        public virtual void AddRange(Object[] items)
        {
            foreach (object item in items)
                Add(item);
        }
    }
}