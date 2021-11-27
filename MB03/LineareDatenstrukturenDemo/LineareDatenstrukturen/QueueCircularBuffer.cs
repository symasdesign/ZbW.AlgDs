using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineareDatenstrukturen {
    public class QueueCircularBuffer<T> {
        private T[] items;
        private int first = 0, last = 0;

        public int Count { get; private set; }

        public QueueCircularBuffer(int length) {
            items = new T[length == 0 ? 10 : length];
        }

        public QueueCircularBuffer()
            : this(0) {
        }

        public void Enqueue(T item) {
            if (Count == items.Length)
                throw new ArgumentOutOfRangeException("No more space in cirular buffer queue.");

            items[last] = item;

            last++;
            Count++;
            last = last % items.Length; // Umlaufprüfung
        }

        public T Peek() {
            if (Count == 0)
                throw new InvalidOperationException("No items in queue.");

            return items[first];
        }

        public T Dequeue() {
            if (Count == 0)
                throw new InvalidOperationException("No items in queue.");

            T item = items[first];

            items[first] = default(T);

            first++;
            Count--;
            first = first % items.Length; // Umlaufprüfung

            return item;
        }

        public void Clear() {
            items = new T[10];
            last = first = 0;
        }

        public override string ToString() {
            string s = "";

            int i = first;

            while (i != last) {
                s += items[i].ToString() + " -> ";
                i = i + 1 == items.Length ? 0 : i + 1;
            }
            s += "Count: " + Count.ToString();

            return s;
        }
    }

}
