using System.Text;
namespace DataStructure
{
    public class Queue
    {
        private sealed class Node
        {
            public Node? Next { get; set; }
            public object Item { get; set; }
            public Node(object item)
            {
                Item = item;
            }
        }

        Node front;
        Node back;

        public void Enqueue(object item)
        {
            Node node = new Node(item);
            if (front == null)
            {
                front = node;
                back = node;
                return;
            }

            back.Next = node;
            back = back.Next;
        }
        public void Dequeue()
        {
            if (front == null)
                throw new NullReferenceException("Queue is empty!");

            front = front.Next;
        }
        public object Peek()
        {
            if (front == null)
                throw new NullReferenceException("Queue is empty!");

            return front.Item;
        }
        public override string ToString()
        {
            Node current = front;
            StringBuilder str = new StringBuilder();
            while (current != null)
            {
                str.Append($"{current.Item.ToString()} -> ");
                current = current.Next;
            }
            return str.ToString();
        }
    }
}