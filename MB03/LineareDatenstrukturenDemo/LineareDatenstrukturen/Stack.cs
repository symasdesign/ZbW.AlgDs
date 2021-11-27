using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineareDatenstrukturen {
    public class Stack<T> {
        private T[] items;

        public int Count { get; private set; }

        public Stack(int length = 0) {
            items = new T[length == 0 ? 10 : length];
        }

        public void Push(T item) {
            grow();

            items[Count] = item;

            Count++;
        }

        public T Peek() {
            if (Count == 0)
                throw new InvalidOperationException("No items on stack.");

            return items[Count - 1];
        }

        public T Pop() {
            if (Count == 0)
                throw new InvalidOperationException("No items on stack.");

            T item = items[Count - 1];
            items[Count - 1] = default(T);
            Count--;

            return item;
        }

        public void Clear() {
            items = new T[10];
            Count = 0;
        }

        private void grow() {
            // Überprüfen, ob noch Platz
            if (items.Length >= Count + 1)
                return;

            // Array-Kapazität verdoppeln
            int newLength = items.Length * 2;

            Array.Resize(ref items, newLength);
        }
    }

}
