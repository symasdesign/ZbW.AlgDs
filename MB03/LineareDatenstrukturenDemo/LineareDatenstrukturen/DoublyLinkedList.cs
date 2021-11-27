using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineareDatenstrukturen {
    public class DoublyLinkedList {

        private sealed class Node {
            public object Data { get; set; }
            public Node Link { get; set; }
            public Node PrevLink { get; set; }
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
                end.Link = newItem;
                newItem.PrevLink = end;

                end = newItem;
            }
            Count++;
        }

        public bool InsertAfter(object previousData, object data) {
            var previousNode = Find(previousData);
            if (previousNode == null) {
                return false;
            }

            var newNode = new Node() { Data = data };
            newNode.Link = previousNode.Link;
            newNode.PrevLink = previousNode;

            previousNode.Link = newNode;

            if (newNode.Link != null)
                newNode.Link.PrevLink = newNode;
            else
                end = newNode;

            Count++;

            return true;
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

            if (start == node) {
                // Anfang soll gelöscht werden, first korrigieren
                start = node.Link;
            }

            if (end == node) {
                // Ende soll gelöscht werden, last korrigieren
                end = node.PrevLink;
            }

            if (node.PrevLink != null) {
                // Wenn es einen Vorgänger gibt, dessen verweis korrigieren
                node.PrevLink.Link = node.Link;
            }

            if (node.Link != null) {
                // Wenn es einen Nachfolger gibt, dessen verweis korrigieren
                node.Link.PrevLink = node.PrevLink;
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
            get { return this.FindByIndexInternal(index)?.Data; }
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
