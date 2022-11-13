using System;

namespace LineareDatenstrukturen {
    public class QueueLinkedList {
        private DoublyLinkedList items;

        public int Count {
            get {
                return items.Count;
            }
        }

        public QueueLinkedList() {
            items = new DoublyLinkedList(); ;
        }

        public void Enqueue(object item) {
            items.Add(item);
        }

        public object Peek() {
            if (Count == 0)
                throw new InvalidOperationException("No items in queue.");

            return items[0];
        }

        public object Dequeue() {
            if (Count == 0)
                throw new InvalidOperationException("No items in queue.");

            object item = items[0];

            items.Remove(item);

            return item;
        }

        public void Clear() {
            items.Clear();
        }
    }

}
