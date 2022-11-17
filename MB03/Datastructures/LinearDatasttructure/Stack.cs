using System.Text;
namespace DataStructure
{
    public class Stack
    {
        private sealed class Node
        {
            public Node? Previous { get; set; }
            public object Item { get; set; }
            public Node(object item)
            {
                Item = item;
            }
        }

        private Node? top;

        public void Push(object item)
        {
            Node node = new Node(item);
            
            if (IsEmpty())
            {
                top = node;
                return;
            }

            node.Previous = top;
            top = node;
        }
        public void Pop()
        {
            if (IsEmpty())
                throw new NullReferenceException("Stack is empty!");

            top = top.Previous;
        }
        public object Peek()
        {
            if (IsEmpty())
                throw new NullReferenceException("Stack is empty!");

            return top.Item;
        }
        public bool IsEmpty()
        {
            return top == null;
        }
        public override string ToString()
        {
            Node current = top;
            StringBuilder str = new StringBuilder();
            while (current != null)
            {
                str.Append($"{current.Item.ToString()} -> ");
                current = current.Previous;
            }
            return str.ToString();
        }
    }
}

/*
namespace DataStructure
{
    
    public class Stack<T>
    {
        T[] data;
        int size = 0;

        public void Push(T item)
        {
        }
        public void Pop()
        {
        }
        public object Peek()
        {

        }
        public override string ToString()
        {
        }
    }
}
*/
