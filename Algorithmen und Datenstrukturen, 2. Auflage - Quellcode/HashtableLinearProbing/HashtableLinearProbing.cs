using System;

namespace My.Collections
{
    public class HashtableLinearProbing<TKey, TValue>
    {
        private class Pair
        {
            public TKey Key { get; private set; } // key nicht nachträglich ändern
            public TValue Value { get; set; }
            public bool IsDeleted { get; internal set; }

            public Pair(TKey key, TValue value, bool isDeleted = false)
            {
                Key = key;
                Value = value;
                IsDeleted = isDeleted;
            }
        }

        private Pair[] items;

        public int Count { get; private set; }

        public HashtableLinearProbing(int length = 10)
        {
            length = CalcPrimeLength(length * 2);
            items = new Pair[length];
        }

        public void Add(TKey key, TValue value)
        {
            if (ContainsKey(key))
                throw new ArgumentException("Item already exist in collection.");

            // Überlauf, kein weiteres Element kann aufgenommen werden
            if (Count + 1 == items.Length)
                throw new ArgumentException("No more space in array.");

            int hash = Math.Abs(key.GetHashCode()) % items.Length;

            while (items[hash] != null && !items[hash].IsDeleted)
            {
                ++hash;
                hash %= items.Length;
            }

            items[hash] = new Pair(key, value);

            Count++;
        }

        public bool Remove(TKey key)
        {
            int hash = Math.Abs(key.GetHashCode()) % items.Length;

            // bei Add wird immer ein leeres Element beibebehalten, ansonsten
            // bei Füllgrad 100% Endlosschleife bei Suche nach nicht vorhandenem Element
            while (items[hash] != null)
            {
                if (items[hash].Key.Equals(key) && !items[hash].IsDeleted)
                {
                    // items[hash] nicht auf null setzen, da sonst beim Suchen dahinterliegende nicht mehr gefunden werden
                    // Kennzeichen, das Eintrag gelöscht. Kein Umkopieren im Array, da sich sonst der Index verändert und der Hash nicht 
                    // mehr passt
                    items[hash].IsDeleted = true;
                    Count--;
                    return true;
                }
                ++hash;
                hash %= items.Length;
            }
            return false;
        }

        public TValue this[TKey key]
        {
            get
            {
                int hash = Math.Abs(key.GetHashCode()) % items.Length;

                while (items[hash] != null)
                {
                    if (items[hash].Key.Equals(key) && !items[hash].IsDeleted)
                        return items[hash].Value;
                    ++hash;
                    hash %= items.Length;
                }
                throw new ArgumentException("Key not found.");
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
            items = new Pair[items.Length];
            Count = 0;
        }

        public bool Contains(TKey key)
        {
            return ContainsKey(key);
        }

        public bool ContainsKey(TKey key)
        {
            int hash = Math.Abs(key.GetHashCode()) % items.Length;

            while (items[hash] != null && !items[hash].IsDeleted)
            {
                if (items[hash].Key.Equals(key))
                    return true;
                ++hash;
                hash %= items.Length;
            }
            return false;
        }

        public override string ToString()
        {
            string s = "";

            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] != null && !items[i].IsDeleted)
                    s += items[i].Key.ToString() + "|" + items[i].Value.ToString() + " -> ";
            }
            return s + "Count: " + Count.ToString();
        }

        private void Update(TKey key, TValue value)
        {
            int hash = Math.Abs(key.GetHashCode()) % items.Length;

            while (items[hash] != null && !items[hash].IsDeleted)
            {
                if (items[hash].Key.Equals(key))
                {
                    items[hash].Value = value;
                    break;
                }
                ++hash;
                hash %= items.Length;
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
    }
}
