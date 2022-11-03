using System;
using System.Collections;

namespace My.Collections
{
    public class LinkedList : IEnumerable
    {
        private class Node
        {
            public Object Item { get; set; }
            public Node Next { get; set; }
        }

        public class LinkedListEnumerator : IEnumerator
        {
            private LinkedList list;
            private Object current;
            private int index;

            public LinkedListEnumerator(LinkedList list)
            {
                this.list = list;
                index = -1;
            }

            public object Current
            {
                get { return current; }
            }

            public bool MoveNext()
            {
                ++index;

                if (index >= list.Count)
                    return false;

                current = list[index];
                return true;
            }

            public void Reset()
            {
                index = -1;
            }
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

        public bool Contains(Object item)
        {
            Node node = Find(item);

            if (node == null)
                return false;

            return true;
        }

        public bool Remove(Object item)
        {
            Node node = Find(item);

            if (node == null)
                return false;

            Node previousNode = FindPrevious(item);

            // aus Mitte oder Ende entfernen
            if (previousNode != null)
            {
                previousNode.Next = node.Next;

                if (node == last)
                    last = previousNode;
            }
            else // ersten entfernen, previousNode == null
            {
                first = node.Next;

                if (first == null) // Liste leer
                    last = null;
            }

            Count--;

            return true;
        }

        public bool Remove2(Object item)
        {
            Node node = Find(item);

            if (node == null)
                return false;

            // erstes Element
            if (node == first)
                first = first.Next;
            else
            {
                Node previousNode = FindPrevious(item);

                if (previousNode != null)
                {
                    previousNode.Next = node.Next;

                    if (node == last)
                        last = previousNode;
                }
            }
            if (first == null) // Liste leer
                last = null;

            Count--;

            return true;
        }

        public void Clear()
        {
            first = last = null;
            Count = 0;
        }

        public object this[int index]
        {
            get
            {
                return FindByIndex(index).Item;
            }
            set
            {
                FindByIndex(index).Item = value;
            }
        }

    public IEnumerator GetEnumerator()
    {
        Node node = first;

        while (node != null)
        {
            yield return node.Item;
            node = node.Next;
        }

        //return new LinkedListEnumerator(this);
    }

        public override string ToString()
        {
            string s = "";

            Node node = first;

            while (node != null)
            {
                s += node.Item.ToString() + " -> ";
                node = node.Next;
            }
            s += "Count: " + Count.ToString();

            return s;
        }

        private Node Find(Object item)
        {
            Node node = first;

            while (node != null)
            {
                if (node.Item.Equals(item))
                    return node;

                node = node.Next;
            }
            return null;
        }

        private Node FindPrevious(Object item)
        {
            Node previousNode = null;
            Node node = first;

            while (node != null)
            {
                if (node.Item.Equals(item))
                    return previousNode;

                previousNode = node;
                node = node.Next;
            }
            return null;
        }

        private Node FindByIndex(int index)
        {
            if (index >= Count)
                throw new IndexOutOfRangeException();

            Node node = first;
            int i = 0;

            while (node != null)
            {
                if (i == index)
                    return node;

                node = node.Next;
                i++;
            }
            return null;
        }
    }
}