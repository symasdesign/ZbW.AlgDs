using System;
using System.Threading.Tasks;

namespace My.Collections
{
    public class SortedArrayList<T> : ArrayList<T> where T : IComparable<T>
    {
        public void BubbleSort()
        {
            bool swapped = true;
            int pass = 0;       // bereits sortierte Einträge am Ende des Arrays

            while(swapped)
            {
                swapped = false;
 
                for (int i = 0; i < Count - 1 - pass; i++)
                {
                    if (this[i].CompareTo(this[i + 1]) > 0)
                    {
                        Swap(i, i + 1);
                        swapped = true;
                    }
                }
                pass++;
            }
        }

        public void InsertionSort()
        {
            for (int i = 1; i < Count; i++ )
                for(int j = i; j > 0; j--)
                     if (this[j].CompareTo(this[j - 1]) < 0)
                        Swap(j, j - 1);
                    else
                        break;
        }

        public void BucketSort()
        {
            int max = int.Parse(Max.ToString());
            int min = int.Parse(Min.ToString());
            int n = max - min + 1;

            // Partitionen anlegen
            var partitions = new SortedArrayList<T>[n];
            for(int i = 0; i < n; i++)
                partitions[i] = new SortedArrayList<T>();

            // Einträge auf Partitionen verteilen
            foreach (var item in this)
            {
                int pn = (int)Convert.ChangeType(item, typeof(int)) - min;
                partitions[pn].Add(item);
            }

            // Partitionen konkatenieren
            n = 0;
            foreach(var pn in partitions)
                foreach(var item in pn)
                    this[n++] = item;
        }

        public T Max
        {
            get
            {
                if(Count == 0)
                    throw new ArgumentException("No items in list");

                T itemMax = this[0];
                foreach (var item in this)
                    if (item.CompareTo(itemMax) > 0)
                        itemMax = item;

                return itemMax;
            }
        }

        public T Min
        {
            get
            {
                if (Count == 0)
                    throw new ArgumentException("No items in list");

                T itemMin = this[0];
                foreach (var item in this)
                    if (item.CompareTo(itemMin) < 0)
                        itemMin = item;

                return itemMin;
            }
        }

        public void QuickSort()
        {
            QSort(0, Count - 1);
        }

        private void QSort(int lower, int upper)
        {
            if (lower >= upper)
                return;
            
            int pivot = (lower + upper) / 2; // alternativ: Random().Next(0, Count)

            int pivotNew = Divide(lower, upper, pivot);

            QSort(lower,        pivotNew - 1);
            QSort(pivotNew + 1, upper);           
        }

        private int Divide(int lower, int upper, int pivot)
        {
            T pivotValue = this[pivot];
            int pivotNew = lower;

            // Pivotelement nach oben verschieben
            Swap(pivot, upper);

            for (int i = lower; i < upper; i++)
                if (this[i].CompareTo(pivotValue) < 0)   // kleiner
                {
                    Swap(i, pivotNew);
                    pivotNew++;
                }

            // Pivotwert an den neuen einsortierten Pivotindex setzen
            Swap(upper, pivotNew);

            return pivotNew;
        }

        public void MergeSort()
        {
            // items auf protected setzen, damit Zugriff
            MSort(items, Count);
        }

        private static void Merge(SortedArrayList<T> target, SortedArrayList<T> left, SortedArrayList<T> right)
        {
            int l = 0, r = 0;

            int count = left.Count + right.Count;

            while(count > 0)
            {
                if (l >= left.Count)           // der letzte linke wurde bereits verglichen
                    target.Add(right[r++]);
                else if (r >= right.Count)     // der letzte rechte wurde bereits verglichen
                    target.Add(left[l++]);
                else if (left[l].CompareTo(right[r]) < 0)
                    target.Add(left[l++]);
                else
                    target.Add(right[r++]);

                count--;
            }
        }


        private void MSort(T[] items, int n)
        {
            if (n == 1)         // Aufteilung bis auf 1 Element
                return;

            int mid = n / 2;
            int leftSize = mid;
            int rightSize = n - leftSize;

            T[] left  = new T[leftSize];
            T[] right = new T[rightSize];

            Array.Copy(items, 0, left, 0, leftSize);
            Array.Copy(items, mid, right, 0, rightSize);

            MSort(left, left.Length);
            MSort(right, right.Length);

            Merge(items, left, right);
        }

        private void Merge(T[] target, T[] left, T[] right)
        {
            int l = 0, r = 0, t = 0;

            int count = left.Length + right.Length;

            while(count > 0)
            {
                if (l >= left.Length)           // der letzte linke wurde bereits verglichen
                    target[t] = right[r++];
                else if (r >= right.Length)     // der letzte rechte wurde bereits verglichen
                    target[t] = left[l++];
                else if (left[l].CompareTo(right[r]) < 0)
                    target[t] = left[l++];
                else
                    target[t] = right[r++];

                t++;
                count--;
            }
        }

        public void MergeSortParallel()
        {
            MSortParallel(items, Count);
        }

        private void MSortParallel(T[] items, int n)
        {
            if (n == 1)         // Aufteilung bis auf 1 Element
                return;

            int mid = n / 2;
            int leftSize = mid;
            int rightSize = n - leftSize;

            T[] left = new T[leftSize];
            T[] right = new T[rightSize];

            Array.Copy(items, 0, left, 0, leftSize);
            Array.Copy(items, mid, right, 0, rightSize);

            Task.WaitAll(new Task[] { 
                Task.Run(delegate { MSortParallel(left, left.Length); }), 
                Task.Run(delegate { MSortParallel(right, right.Length); }) });

            Merge(items, left, right);
        }

        public static SortedArrayList<T> Merge(SortedArrayList<T>[] lists)
        {
            // Count berücksichtigen
            if (lists.Length < 2)
                return null;

            var sortedArray = new SortedArrayList<T>();


            for (int i = 0; i < lists.Length - 1; i++)
            {
                SortedArrayList<T>.Merge(sortedArray, lists[i], lists[i + 1]);
            }

            return sortedArray;
        }

        private void Swap(int index1, int index2)
        {
            T item = this[index1];

            this[index1] = this[index2];
            this[index2] = item;
        }

        private void Swap(ref T item1, ref T item2)
        {
            T item = item1;

            item1 = item2;
            item2 = item;
        }

        public int BinarySearch(T item)
        {
            int lower = 0;
            int upper = Count - 1;

            while(lower <= upper)
            {
                int mid = lower + (upper - lower) / 2;  // alternativ: >> 1 anstatt / 2

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

            int mid = lower + (upper - lower) / 2;

            int o = item.CompareTo(this[mid]);

            if (o == 0)
                return mid;
            else if (o > 0)
                return Search(item, mid + 1, upper);    // oben weitersuchen
            else
                return Search(item, lower, mid - 1);    // unten weitersuchen
        }


        public override string ToString()
        {
            string s = "";

            for (int i = 0; i < Count; i++)
            {
                s += this[i].ToString() + ";";
            }

            return s;
        }
    }
}