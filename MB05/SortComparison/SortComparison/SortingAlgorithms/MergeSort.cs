using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortComparison.SortingAlgorithms
{
    public class MergeSort : SortAlgorithm
    {
        public override string Name => "MergeSort";

        public override void Sort(IList<int> arrayToSort)
        {
            MergeSorti(arrayToSort, 0, arrayToSort.Count-1);
        }

        private void MergeSorti(IList<int> arrayToSort, int start, int end)
        {
            if (start < end)
            {
                // define midpoint
                int mid = start + (end - start) / 2;

                // Sort first and second halves
                MergeSorti(arrayToSort, start, mid);
                MergeSorti(arrayToSort, mid + 1, end);

                Merge(arrayToSort, start, mid, end);
            }
        }

        private void Merge(IList<int> arrayToSort, int start, int mid, int end)
        {
            int start2 = mid + 1;

            // If the direct merge is already sorted
            if (arrayToSort[mid] <= arrayToSort[start2])
            {
                return;
            }

            // Two pointers to maintain start
            // of both arrays to merge
            while (start <= mid && start2 <= end)
            {

                // If element 1 is in right place
                if (arrayToSort[start] <= arrayToSort[start2])
                {
                    start++;
                }
                else
                {
                    int value = arrayToSort[start2];
                    int index = start2;

                    // Shift all the elements between element 1
                    // element 2, right by 1.
                    while (index != start)
                    {
                        arrayToSort[index] = arrayToSort[index - 1];
                        index--;
                    }
                    arrayToSort[start] = value;

                    // Update all the pointers
                    start++;
                    mid++;
                    start2++;
                }
            }
        }


    }
}
