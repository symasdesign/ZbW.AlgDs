using System.Collections.Generic;

namespace SortComparison {
    public class BiDirectionalBubbleSort : SortAlgorithm {
        public override string Name => "BiDirectional Bubblesort";


        // better than normal bubble sort, but still slow
        public override void Sort(IList<int> arrayToSort) {
            var limit = arrayToSort.Count;
            var st = -1;
            bool swapped;
            do {
                swapped = false;
                st++;
                limit--;

                for (var j = st; j < limit; j++) {
                    if (arrayToSort[j] > arrayToSort[j + 1]) {
                        SwapItems(arrayToSort, j, j + 1);
                        swapped = true;
                    }
                }

                if (swapped) {
                    // if there was no swap in the first half, we're sorted!
                    swapped = false; // stop after this if we make no swaps

                    for (var j = limit - 2; j >= st; j--) {
                        // subtract 2 since we already checked and fixed limit - 1
                        if (arrayToSort[j] > arrayToSort[j + 1]) {
                            SwapItems(arrayToSort, j, j + 1);
                            swapped = true;
                        }
                    }
                }

            } while (st < limit && swapped);
        }

        private void SwapItems(IList<int> arrayToSort, int index1, int index2) {
            var temp = arrayToSort[index1];
            arrayToSort[index1] = arrayToSort[index2];
            arrayToSort[index2] = temp;
        }
    }
}