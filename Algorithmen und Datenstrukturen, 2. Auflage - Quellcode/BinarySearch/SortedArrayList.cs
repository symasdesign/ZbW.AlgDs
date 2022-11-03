using System;

namespace My.Collections
{
    public class SortedArrayList<T> : ArrayList<T> where T : IComparable<T>
    {
        public int BinarySearch(T item)
        {
            int lower = 0;
            int upper = Count - 1;

            while(lower <= upper)
            {
                int mid = (lower + upper) / 2;  // alternativ: >> 1 anstatt / 2

                int o = item.CompareTo(this[mid]);

                if (o == 0)
                    return mid;
                else if (o > 0)
                    lower = mid + 1;    // oben weitersuchen
                else
                    upper = mid - 1;    // unten weitersuchen
            }
            return ~lower;
        }

        public int BinarySearchRecursive(T item)
        {
            return Search(item, 0, Count-1);
        }

        private int Search(T item, int lower, int upper)
        {
            if (lower > upper)
                return ~lower;

            int mid = (lower + upper) / 2;

            int o = item.CompareTo(this[mid]);

            if (o == 0)
                return mid;
            else if (o > 0)
                return Search(item, mid + 1, upper);    // oben weitersuchen
            else
                return Search(item, lower, mid - 1);    // unten weitersuchen
        }
    }
}