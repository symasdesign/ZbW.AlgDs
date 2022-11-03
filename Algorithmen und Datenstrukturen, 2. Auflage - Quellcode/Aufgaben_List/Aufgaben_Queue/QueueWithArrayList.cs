using System;

namespace My.Collections
{
    public class QueueWithArrayList<T>
    {
        private ArrayList<T> list;

        public int Count
        {
            get
            {
                return list.Count;
            }
        }

        public QueueWithArrayList(int length = 10)
        {
            list = new ArrayList<T>(length);
        }

        public void Enqueue(T item)
        {
            list.Add(item);
        }

        public T Peek()
        {
            if (Count == 0)
                throw new InvalidOperationException("No items in queue.");

            return list[0];
        }

        public T Dequeue()
        {
            if (Count == 0)
                throw new InvalidOperationException("No items in queue.");

            T item = list[0];
            list.RemoveAt(0);
            return item;
        }

        public void Clear()
        {
            list.Clear();
        }
    }
}
