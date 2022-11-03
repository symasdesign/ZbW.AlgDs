using System;

namespace My.Collections
{
    public class StackList<T> : ICollection
    {
        private ArrayList<T> items = new ArrayList<T>();

        public int Count 
        { 
            get { return items.Count; }
        }

        public void Push(T item)
        {
            items.Add(item);
        }

        public T Pop()
        {
            T item;

            if (items.Count == 0)
                throw new InvalidOperationException("No items in stack");
            
            item = items[items.Count - 1];
            items.RemoveAt(items.Count - 1);

            return item;
        }

        public T Peek()
        {
            if (items.Count == 0)
                throw new InvalidOperationException("No items in stack");

            return items[items.Count - 1];
        }

        public void Clear()
        {
            items.Clear();
        }
    }
}