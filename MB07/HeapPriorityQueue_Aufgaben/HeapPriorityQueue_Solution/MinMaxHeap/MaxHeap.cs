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
            return (idx - 1) / 2;
        }

        private static int GetLeftIndex(int idx) {
            return (2 * idx) + 1;
        }

        private static int GetRightIndex(int idx) {
            return (2 * idx) + 2;
        }

        private void BubbleUp(int idx) {
            int parentIdx;
            while ( (parentIdx = GetParentIndex(idx))  >= 0) {
                if (Comparer.Default.Compare(this.data[GetParentIndex(idx)], this.data[idx]) >= 0) {
                    // Bedingung erfüllt: aktuelles Parent ist grösser als aktuelles Element
                    break;
                }
                Swap(idx, parentIdx);
                idx = parentIdx;
            }
        }

        private void Swap(int a, int b) {
            var current = this.data[a];
            this.data[a] = this.data[b];
            this.data[b] = current;

        }
        public void Add(object item) {
            // Am Schluss einfügen und solange mit dem Parent-Node vertauschen, bis sich item an der richtigen Position im Heap befindet
            this.data.Add(item);
            this.BubbleUp(this.data.Count - 1);
        }

        public bool Empty => this.data.Count <= 0;

        public int Size => this.data.Count;

        public object Peek() {
            if (this.data.Count <= 0) {
                throw new InvalidOperationException("Heap is empty");
            }

            return this.data[0];
        }

        public object Pop() {
            if (this.data.Count <= 0) {
                throw new InvalidOperationException("Heap is empty");
            }
            var ret = this.data[0];
            if (this.data.Count == 1) {
                this.data.Clear();
                return ret;
            }

            // Root mit dem letzten Leaf ersetzen und dieses dann solange runterschieben, bis es sich an der korrekten Position befindet.
            this.data[0] = this.data[this.data.Count - 1];
            this.data.RemoveAt(this.data.Count-1);
            this.BubbleDown();

            return ret;
        }

        private void BubbleDown() {
            var min = 0;
            while (true) {
                var leftIdx = GetLeftIndex(min);
                var rightIdx = GetRightIndex(min);

                // Ist der abwärts zu verschiebende Node kleiner als beide Child-Nodes, dann vertausche ihn mit dem grösseren Child (das danach als Parent-Node des kleineren Childs fungiert)
                if (this.data.Count > rightIdx && Comparer.Default.Compare(this.data[min], this.data[leftIdx]) < 0 && Comparer.Default.Compare(this.data[min], this.data[rightIdx]) < 0) {
                    if (Comparer.Default.Compare(this.data[leftIdx], this.data[rightIdx]) > 0) {
                        Swap(min, leftIdx);
                        min = leftIdx;
                    } else {
                        Swap(min, rightIdx);
                        min = rightIdx;
                    }

                    continue;
                }

                // Wenn der Node kleiner als ein Child-Node ist, vertausche ihn mit diesem
                if (this.data.Count > rightIdx && Comparer.Default.Compare(this.data[min], this.data[rightIdx]) < 0) {
                    Swap(min, rightIdx);
                    min = rightIdx;
                    continue;
                }
                if (this.data.Count > leftIdx && Comparer.Default.Compare(this.data[min], this.data[leftIdx]) < 0) {
                    Swap(min, leftIdx);
                    min = leftIdx;
                    continue;
                }

                break;
            }
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
