using System.Collections.Generic;

namespace SortComparison.SortingAlgorithms {
    public class InsertionSort : SortAlgorithm {

        public override string Name => "Insertionsort";

        public override void Sort(IList<int> arrayToSort) {
            int i, j;

            for (i = 1; i < arrayToSort.Count; i++) {
                var einzsortierender_wert = arrayToSort[i];
                j = i - 1;
                while ((j >= 0) && arrayToSort[j] > einzsortierender_wert) {
                    arrayToSort[j + 1] = arrayToSort[j];
                    j = j - 1;
                }
                arrayToSort[j + 1] = einzsortierender_wert;
            }
        }
    }
}

