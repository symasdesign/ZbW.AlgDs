using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineareDatenstrukturen {
    public class StackLinkedList {
        private DoublyLinkedList items;

        public int Count {
            get {
                return items.Count;
            }
        }

        public StackLinkedList() {
            items = new DoublyLinkedList(); ;
        }

        public void Push(object item) {
            items.Add(item);

        }

        public object Pop() {
            if (Count == 0)
                throw new InvalidOperationException("No items in stack.");

            var ret = items[items.Count - 1];
            this.items.Remove(ret);

            return ret;
        }

        public object Peek() {
            if (Count == 0)
                throw new InvalidOperationException("No items in queue.");

            return items[items.Count - 1];
        }
    }
}
