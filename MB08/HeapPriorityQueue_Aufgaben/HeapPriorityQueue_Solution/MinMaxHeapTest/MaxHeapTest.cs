using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinMaxHeap;

namespace MinMaxHeapTest {
    [TestClass]
    public class MaxHeapTest {
        [TestMethod]
        public void AddEmptyRemove() {
            var heap = new MaxHeap();

            heap.Add(10);
            Assert.AreEqual(10, heap.Peek());

            var res = heap.Pop();
            Assert.AreEqual(10, res);
            heap.Add(20);
            Assert.AreEqual(20, heap.Peek());
        }

        [TestMethod]
        public void AddMultipleCheckPeek() {
            foreach (int[] a in GetAllPermutations(new[] { 10, 20, 2, 45, 7, 5, 12 })) {
                var heap = new MaxHeap();
                foreach (int i in a) {
                    heap.Add(i);
                }
                Assert.AreEqual(heap.Peek(), a.Max());
            }
        }

        [TestMethod]
        public void AddMultipleCheckPopThenPeek() {
            foreach (int[] a in GetAllPermutations(new[] { 10, 20, 2, 45, 7, 5, 12 })) {
                var heap = new MaxHeap();
                foreach (int i in a) {
                    heap.Add(i);
                }
                foreach (int i in a.OrderByDescending(x => x)) {
                    Assert.AreEqual(heap.Peek(), i);
                    Assert.AreEqual(heap.Pop(), i);
                }
            }
        }

        private static IEnumerable<T[]> GetAllPermutations<T>(T[] values) {
            if (values.Length == 1)
                return new[] { values };

            return values.SelectMany(v => GetAllPermutations(values.Except(new[] { v }).ToArray()),
                (v, p) => new[] { v }.Concat(p).ToArray());
        }
    }
}
