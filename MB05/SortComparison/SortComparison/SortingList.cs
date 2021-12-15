using System;
using System.Collections;
using System.Collections.Generic;

namespace SortComparison {
    public delegate void HighlightingEventHandler(object source, HighlightingEventArgs e);

    public class HighlightingEventArgs : EventArgs {
        public HighlightingEventArgs(int index) {
            Index = index;
        }

        public int Index { get; }
    }

    public class SortingList : IList<int> {
        public event HighlightingEventHandler OnHighlighting;
        private bool highlightingBlocked;
        private readonly List<int> list;
        public int IndexOf(int item) {
            return this.list.IndexOf(item);
        }

        public void Insert(int index, int item) {
            this.list.Insert(index, item);
        }

        public void RemoveAt(int index) {
            this.list.RemoveAt(index);
        }

        public int this[int index] {
            get {
                if (!this.highlightingBlocked) {
                    this.OnHighlighting?.Invoke(this, new HighlightingEventArgs(index));
                }
                return list[index];
            }
            set {
                if (!this.highlightingBlocked) {
                    this.OnHighlighting?.Invoke(this, new HighlightingEventArgs(index));
                }
                list[index] = value;
            }
        }

        public SortingList() {
            this.list = new List<int>();
        }
        public SortingList(IEnumerable<int> c) {
            this.list = new List<int>(c);
        }
        public SortingList(int capacity) {
            this.list = new List<int>(capacity);
        }

        public object Clone() {
            var ret = new SortingList(this);
            return ret;
        }

        public HighlightingBlocker BlockHighlighting() {
            return new HighlightingBlocker(this);
        }

        public class HighlightingBlocker : IDisposable {
            private readonly SortingList list;
            public HighlightingBlocker(SortingList list) {
                this.list = list;
                this.list.highlightingBlocked = true;
            }


            public void Dispose() {
                this.list.highlightingBlocked = false;
            }
        }

        public IEnumerator<int> GetEnumerator() {
            return this.list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }

        public void Add(int item) {
            this.list.Add(item);
        }

        public void Clear() {
            this.list.Clear();
        }

        public bool Contains(int item) {
            return this.list.Contains(item);
        }

        public void CopyTo(int[] array, int arrayIndex) {
            this.list.CopyTo(array, arrayIndex);
        }

        public bool Remove(int item) {
            return this.list.Remove(item);
        }

        public int Count => this.list.Count;
        public bool IsReadOnly => false;

        public int Capacity => this.list.Capacity;

        public void Sort() {
            this.list.Sort();
        }

        public void Reverse() {
            this.list.Reverse();
        }
    }
}
