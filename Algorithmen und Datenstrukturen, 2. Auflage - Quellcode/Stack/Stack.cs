using System;

namespace My.Collections
{
    public class Stack<T> : ICollection
    {
        private T[] items;

        public int Count { get; private set; }

        public Stack(int length = 10)
        {
            items = new T[length];
        }

        public void Push(T item)
        {
            Grow();

            items[Count] = item;

            Count++;
        }

        public T Peek()
        {
            if (Count == 0)
                throw new InvalidOperationException("No items on stack.");

            return items[Count - 1];
        }

        public T Pop()
        {
            if (Count == 0)
                throw new InvalidOperationException("No items on stack.");

            T item = items[Count - 1];
            items[Count - 1] = default(T);
            Count--;

            return item;
        }

        public void Clear()
        {
            items = new T[10];
            Count = 0;
        }

        public override string ToString()
        {
            string s = "";

            for (int i = 0; i < Count; i++)
            {
                s += items[i].ToString() + " -> ";
            }
            s += "Count: " + Count.ToString();

            return s;
        }

        private void Grow()
        {
            // Überprüfen, ob noch Platz
            if (items.Length >= Count + 1)
                return;

            // Array-Kapazität verdoppeln
            int newLength = items.Length * 2;

            Array.Resize(ref items, newLength);
        }
    }
}