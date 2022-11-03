namespace My.Collections
{
    using System;

    public class HashtableDoubleHashing<TKey, TValue>
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

        public HashtableDoubleHashing(int length = 10)
        {
            length = CalcPrimeLength(length * 2);
            if (length < 5)
                length = 5; // siehe Methode GetHashSteps
            items = new Pair[length];
        }

        public void Add(TKey key, TValue value)
        {
            if (ContainsKey(key))
                throw new ArgumentException("Item already exist in collection.");

            // Überlauf, kein weiteres Element kann aufgenommen werden
            if (Count + 1 == items.Length)
                throw new ArgumentException("No more space in array.");

            int hash = GetHash(key);
            int steps = GetHashSteps(key);

            while (items[hash] != null && !items[hash].Key.Equals(default(TKey)))
            {
                hash += steps;
                hash %= items.Length;  // Arrayüberlauf verhindern
            }

            items[hash] = new Pair(key, value);

            Count++;
        }

        public bool Remove(TKey key)
        {
            int hash = GetHash(key);
            int steps = GetHashSteps(key);

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
                hash += steps;
                hash %= items.Length;  // Arrayüberlauf verhindern
            }
            return false;
        }

        public TValue this[TKey key]
        {
            get
            {
                int hash = GetHash(key);
                int steps = GetHashSteps(key);

                while (items[hash] != null)
                {
                    if (items[hash].Key.Equals(key))
                        return items[hash].Value;
                    hash += steps;
                    hash %= items.Length;  // Arrayüberlauf verhindern
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
            int hash = GetHash(key);
            int steps = GetHashSteps(key);

            while (items[hash] != null)
            {
                if (items[hash].Key.Equals(key) && !items[hash].IsDeleted)
                    return true;
                hash += steps;
                hash %= items.Length;  // Arrayüberlauf verhindern
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
            int hash = GetHash(key);
            int steps = GetHashSteps(key);

            while (items[hash] != null && !items[hash].IsDeleted)
            {
                if (items[hash].Key.Equals(key))
                {
                    items[hash].Value = value;
                    break;
                }
                hash += steps;
                hash %= items.Length;  // Arrayüberlauf verhindern
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

        private int GetHashSteps(TKey key)
        {
            return 5 - (key.GetHashCode() % 5);
        }
    }
}
