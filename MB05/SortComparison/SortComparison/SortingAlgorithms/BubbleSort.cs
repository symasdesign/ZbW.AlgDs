using System.Collections.Generic;

namespace SortComparison {
    public class BubbleSort : SortAlgorithm {
        public override string Name => "Bubblesort";
        public override void Sort(IList<int> arrayToSort) {
            var swapMade = true;
            var n = arrayToSort.Count - 1;
            for (var i = 0; i < n && swapMade; i++) {
                swapMade = false;

                for (var j = n; j > i; j--) {
                    if (arrayToSort[j-1] > arrayToSort[j]) {
                        SwapItems(arrayToSort, j - 1, j);
                        swapMade = true;
                    }
                }
            }
        }

        private void SwapItems(IList<int> arrayToSort, int index1, int index2) {
            var temp = arrayToSort[index1];
            arrayToSort[index1] = arrayToSort[index2];
            arrayToSort[index2] = temp;
        }
    }
}
