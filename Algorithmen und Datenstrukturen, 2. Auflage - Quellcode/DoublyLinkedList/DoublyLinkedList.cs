using System;
using System.Collections;
using System.Collections.Generic;

namespace My.Collections
{
    public class DoublyLinkedList<T> : IEnumerable<T>
    {
        private class Node
        {
            public T Item { get; set; }
            public Node Previous { get; set; }
            public Node Next { get; set; }
        }

        private Node first, last;

        public int Count { get; private set; }

        public void Add(T item)
        {
            Node newNode = new Node()
            {
                Item = item,
                Next = null,
                Previous = null
            };

            if (first == null)
            {
                first = newNode;
                last = newNode;
            }
            else
            {
                last.Next = newNode;
                newNode.Previous = last;

                last = newNode;
            }
            Count++;
        }

        public bool InsertAfter(T previousItem, T item)
        {
            Node previousNode = Find(previousItem);

            if (previousNode == null)
                return false;

            Node newNode = new Node()
            {
                Item = item,
                Next = previousNode.Next,
                Previous = previousNode
            };

            previousNode.Next = newNode;

            if (newNode.Next != null)
                newNode.Next.Previous = newNode;
            else
                last = newNode;

            Count++;

            return true;
        }


        public void AddRange(T[] items)
        {
            foreach (T item in items)
                Add(item);
        }

        public bool Contains(T item)
        {
            return Find(item) != null;
        }

        public bool Remove2(T item)
        {
            Node node = Find(item);

            if (node == null)
                return false;

            Node previousNode = node.Previous;

            // aus Mitte oder Ende entfernen
            if (previousNode != null)
            {
                previousNode.Next = node.Next;
                if (previousNode.Next != null)
                    previousNode.Next.Previous = previousNode;

                if (node == last)
                    last = previousNode;
            }
            else // ersten entfernen, previousNode == null
            {
                first = node.Next;

                if (first == null) // Liste leer
                    last = null;
                else
                    first.Previous = null;
            }

            Count--;

            return true;
        }

        public bool Remove(T item)
        {
            Node node = Find(item);

            if (node == null)
                return false;

            if (first == node) // Anfang soll gelöscht werden, first korrigieren
                first = node.Next;

            if (last == node) // Ende soll gelöscht werden, last korrigieren
                last = node.Previous;

            if (node.Previous != null) // Wenn es einen Vorgänger gibt, dessen verweis korrigieren
                node.Previous.Next = node.Next;

            if (node.Next != null) // Wenn es einen Nachfolger gibt, dessen verweis korrigieren
                node.Next.Previous = node.Previous;

            Count--;

            return true;
        }

        public void Clear()
        {
            first = last = null;
            Count = 0;
        }

        public T this[int index]
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


        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            Node node = first;

            while (node != null)
            {
                yield return node.Item;
                node = node.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
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

        private Node Find(T item)
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

        private Node FindPrevious(T item)
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