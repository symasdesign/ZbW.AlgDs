using System.Text;

namespace DataStructure
{
    internal class SinglyLinkedList
    {
        private sealed class Node
        {
            public object Item { get; set; }
            public Node? Next { get; set; }
            public Node(object item)
            {
                Item = item;
            }
        }

        private Node? first;
        private Node? last;

        public void Add(object item)
        {
            var node = new Node(item);

            if (IsEmpty())
            {
                first = last = node;
                return;
            }

            last.Next = node;
            last = last.Next;
        }

        private bool IsEmpty()
        {
            return first == null;
        }

        public void Remove(object item)
        {
            if (IsEmpty())
                throw new ArgumentException("SinglyLinkedList is empty!");

            Node previous = first;
            Node? current = first;

            while(current != null)
            {
                if (current.Item.Equals(item))
                {
                    if (first == last)
                    {
                        first = null;
                        last = null;
                        return;
                    }
                    if (current == first)
                    {
                        first = first.Next;
                        return;
                    }
                    if (current == last)
                    {
                        previous.Next = null;
                        last = previous;
                        return;
                    }

                    previous.Next = current.Next;
                    return;
                }
                previous = current;
                current = current.Next;
            }

            throw new ArgumentException("Item does not exist");
        }
        public object this[int i]
        {
            get
            {
                if (IsEmpty())
                    throw new ArgumentException("SinglyLinkedList is empty!");

                Node? current = first;
                int index = 0;

                while (current != null)
                {
                    if (index++ == i)
                        return current.Item;

                    current = current.Next;
                }

                throw new ArgumentOutOfRangeException();
            }
        }
        public override string ToString()
        {
            Node? current = first;
            StringBuilder result = new StringBuilder();

            while (current != null)
            {
                result.Append($"{current.Item.ToString()} -> ");
                current = current.Next;
            }

            return result.ToString();
        }
    }
}
