using System;
using System.Collections;
using System.Collections.Generic;

namespace My.Collections
{
    public class ArrayList<T> : IEnumerable<T> where T : IComparable<T>
    {
        protected T[] items;

        public int Count { get; private set; }

        public T Min
        {
            get
            {
                if (Count == 0)
                    throw new InvalidOperationException("No data in list.");

                T min = this[0];

                for (int i = 1; i < Count; i++)
                {
                    if (this[i].CompareTo(min) < 0)
                        min = this[i];
                }

                return min;

            }
        }

        public T Max
        {
            get
            {
                if (Count == 0)
                    throw new InvalidOperationException("No data in list.");

                T max = this[0];

                for (int i = 1; i < Count; i++)
                {
                    if (this[i].CompareTo(max) > 0)
                        max = this[i];
                }

                return max;

            }
        }

        public ArrayList(int length = 4)
        {
            items = new T[length];
        }

        public ArrayList(ArrayList<T> copyList) : this()
        {
            foreach (T item in copyList)
            {
                this.Add(item);
            }
        }

        public bool InsertAt(int index, T item)
        {
            if (index < 0 || index >= Count)
                return false;

            Grow();

            Array.Copy(items, index, items, index + 1, Count - index);
            items[index] = item;

            Count++;
            return true;
        }

        public void Add(T item)
        {
            Grow();

            items[Count] = item;

            Count++;
        }

        public void AddRange(T[] items)
        {
            foreach (T item in items)
                Add(item);
        }

        public int IndexOf(T item)
        {
            // return Array.IndexOf<T>(items, item);

            for (int i = 0; i < Count; i++)
            {
                if (items[i].Equals(item))
                    return i;
            }
            return -1;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException();

            if (Count < items.Length / 2 && items.Length > 4)
            {
                int newLength = items.Length / 2;
                Array.Resize(ref items, newLength);
            }
            Array.Copy(items, index + 1, items, index, Count - (index + 1));

            Count--;
            items[Count] = default(T);
        }

        public bool Remove(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (items[i].Equals(item))
                {
                    RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public void Clear()
        {
            items = new T[4];
            Count = 0;
        }

        public T this[int index]
        {
            get
            {
                if (index >= Count)
                    throw new IndexOutOfRangeException();

                return items[index];
            }
            set
            {
                if (index >= Count)
                    throw new IndexOutOfRangeException();

                items[index] = value;
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
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