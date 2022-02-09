using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortComparison.SortingAlgorithms
{
    public class BubbleSort : SortAlgorithm {
        public override string Name => "BubbleSort";

        public override void Sort(IList<int> arrayToSort) {
            var length = arrayToSort.Count;

            var swapped = true;

            while (swapped) {
                swapped = false;

                for (var i = 0; i < length - 1; i++) {
                    if (arrayToSort[i] > arrayToSort[i + 1]) {
                        SwapItems(arrayToSort, i, i + 1);
                        swapped = true;
                    }
                }

                length--;
            }
        }

        private void SwapItems(IList<int> arrayToSort, int idx1, int idx2) {
            var temp = arrayToSort[idx2];
            arrayToSort[idx2] = arrayToSort[idx1];
            arrayToSort[idx1] = temp;
        }
    }
}
