using System;
using System.Collections;
using System.Text;

namespace MinMaxHeap {
    /// <summary>
    /// Hinweis: Es wurde bewusst auf eine Abstrakte Klasse "Heap" verzichtet, um die Komplexität nicht unnötig zu erhöhen.
    /// </summary>
    public class MaxHeap {
        private readonly ArrayList data;

        public MaxHeap(ICollection data) : this() {
            foreach (var item in data) {
                this.Add(item);
            }
        }

        public MaxHeap() {
            this.data = new ArrayList();
        }

        private static int GetParentIndex(int idx) {
            // TODO: Implement

            throw new NotImplementedException();
        }

        private static int GetLeftIndex(int idx) {
            // TODO: Implement

            throw new NotImplementedException();
        }

        private static int GetRightIndex(int idx) {
            // TODO: Implement

            throw new NotImplementedException();
        }

        public void Add(object item) {
            // TODO: Implement

            throw new NotImplementedException();
        }

        public bool Empty => this.data.Count <= 0;

        public int Size => this.data.Count;

        public object Peek() {
            // TODO: Implement

            throw new NotImplementedException();
        }

        public object Pop() {
            // TODO: Implement

            throw new NotImplementedException();
        }

        public void PrintHeap() {
            int iMax = this.data.Count - 1, i;
            if (iMax < -1)
                Console.WriteLine("[]");

            var b = new StringBuilder();
            b.Append('[');
            for (i = 0; i < iMax; i++) {
                b.Append(data[i]);
                b.Append(", ");
            }
            Console.WriteLine(b.Append(this.data[i]).Append(']'));
        }
    }
}
