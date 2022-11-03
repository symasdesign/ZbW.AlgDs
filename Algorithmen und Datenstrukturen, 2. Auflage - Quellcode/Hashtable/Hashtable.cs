using System;
using System.Collections;
using System.Collections.Generic;

namespace My.Collections
{
    public class Hashtable<TKey, TValue>
    {
        private class Pair
        {
            public TKey Key { get; private set; } // key nicht nachträglich ändern
            public TValue Value { get; set; }

            public Pair(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }
        }

        private ArrayList<Pair>[] items;

        public int Count { get; private set; }

        public double LoadFactor
        {
            get
            {
                int buckets = 0;

                foreach (var pairs in items)
                {
                    if (pairs != null)
                        buckets++;
                }

                return 1.0 - ((items.Length - buckets) / (double)items.Length);
            }
        }

        // durchschnittlicher Belegungsfaktor
        public double OccupationFactor
        {
            get
            {
                int buckets = 0;

                foreach (var pairs in items)
                {
                    if (pairs != null)
                        buckets++;
                }

                return (double)Count / buckets;
            }
        }

        public Hashtable(int length = 10)
        {
            length = CalcPrimeLength(length);   // keine Verdopplung, da eigentlich Daten in ArrayList
            items = new ArrayList<Pair>[length];
        }

        public void Add(TKey key, TValue value)
        {
            if (ContainsKey(key))
                throw new ArgumentException("Item already exist in collection.");

            int hash = GetHash(key);

            var list = items[hash];

            if (list == null)    // Liste mit Werten existiert noch nicht
            {
                list = new ArrayList<Pair>();
                items[hash] = list;
            }

            list.Add(new Pair(key, value));

            Count++;
        }

        public void Remove(TKey key)
        {
            int hash = GetHash(key);

            var list = items[hash];

            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Key.Equals(key))
                    {
                        list.RemoveAt(i);
                        Count--;
                        break;
                    }
                }
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                int hash = GetHash(key);

                var list = items[hash];

                if (list != null)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].Key.Equals(key))
                            return list[i].Value;
                    }
                }
                return default(TValue);
            }
            set
            {
                if (!ContainsKey(key))
                    Add(key, value);
                else
                    Update(key, value);
            }
        }

        public void Clear()
        {
            items = items = new ArrayList<Pair>[items.Length];
            Count = 0;
        }

        public bool Contains(TKey key)
        {
            return ContainsKey(key);
        }

        public bool ContainsKey(TKey key)
        {
            int hash = GetHash(key);

            var list = items[hash];

            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Key.Equals(key))
                        return true;
                }
            }
            return false;
        }

        public IEnumerable<TValue> Values()
        {
            foreach (var list in items)
            {
                if (list != null)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        yield return list[i].Value;
                    }
                }
            }
        }

        public IEnumerable<TKey> Keys()
        {
            foreach (var list in items)
            {
                if (list != null)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        yield return list[i].Key;
                    }
                }
            }
        }

        public override string ToString()
        {
            string s = "";

            for (int i = 0; i < items.Length; i++)
            {
                var list = items[i];

                foreach (var pair in list)
                    s += pair.Key.ToString() + "|" + pair.Value.ToString() + " -> ";
            }
            return s + "Count: " + Count.ToString();
        }

        private void Update(TKey key, TValue value)
        {
            int hash = GetHash(key);

            var list = items[hash];

            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Key.Equals(key))
                    {
                        list[i].Value = value;
                        break;
                    }
                }
            }
        }

        private int CalcPrimeLength(int length)
        {
            while (!IsPrime(++length))
                ;

            return length;
        }

        private bool IsPrime(int number)
        {
            for (int i = 2; i <= number / 2; i++)
                if (number % i == 0)
                    return false;

            return true;
        }

        private int GetHash(TKey key)
        {
            return Math.Abs(key.GetHashCode()) % items.Length;
        }
    }
}
