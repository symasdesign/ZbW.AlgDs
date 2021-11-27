using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineareDatenstrukturen {
    public class SinglyLinkedList {

        private sealed class Node {
            public object Data { get; set; }
            public Node Link { get; set; }
        }

        private Node start;
        private Node end;
        public int Count { get; private set; }

        public void Add(object data) {
            var newItem = new Node() {Data = data, Link = null};

            if (start == null) {
                start = newItem;
                end = newItem;
            } else {
                // *** Ende suchen 
                //var end = this.start;
                //while (end.Link != null) {
                //    end = end.Link;
                //}
                end.Link = newItem;
                end = newItem;
            }
            Count++;
        }

        public bool Contains(object data) {
            return Find(data) != null;
        }

        private Node Find(object data) {
            var node = start;

            while (node != null) {
                if (node.Data.Equals(data)) {
                    return node;
                }

                node = node.Link;
            }
            return null;
        }

        public bool Remove(object data) {
            var node = Find(data);

            if (node == null) {
                return false;
            }
            var previousNode = FindPrevious(data);

            if (previousNode != null) {
                // aus Mitte oder Ende entfernen
                previousNode.Link = node.Link;
                if (node == end) {
                    end = previousNode;
                }
            } else {
                // ersten entfernen, previousNode == null
                start = node.Link;
                if (start == null) {
                    // Liste leer
                    end = null;
                }
            }

            Count--;

            return true;
        }

        private Node FindPrevious(object data) {
            Node previousNode = null;
            var node = start;

            while (node != null) {
                if (node.Data.Equals(data)) {
                    return previousNode;
                }

                previousNode = node;
                node = node.Link;
            }
            return null;
        }

        public object FindByIndex(int index) {
            return FindByIndexInternal(index)?.Data;
        }

        private Node FindByIndexInternal(int index) {
            if (index >= Count) {
                throw new IndexOutOfRangeException();
            }

            var node = start;
            var i = 0;

            while (node != null) {
                if (i == index) {
                    return node;
                }
                node = node.Link;
                i++;
            }
            return null;
        }

        public object this[int index] {
            get {
                return this.FindByIndexInternal(index)?.Data;
            }
            set {
                var node = this.FindByIndexInternal(index);
                if (node != null) {
                    node.Data = value;
                }
            }
        }

        public void Clear() {
            start = end = null;
            Count = 0;
        }
    }
}
