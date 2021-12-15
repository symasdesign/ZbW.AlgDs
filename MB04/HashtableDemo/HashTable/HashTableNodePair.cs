
namespace HashTable
{
    /// <summary>
    /// A node in the hash table array
    /// </summary>
    /// <typeparam name="TKey">The type of the key of the key/value pair</typeparam>
    /// <typeparam name="TValue">The type of the value of the key/value pair</typeparam>
    public class HashTableNodePair<TKey, TValue>
    {
        /// <summary>
        /// Constructs a key/value pair for storage in the hash table
        /// </summary>
        /// <param name="key">The key of the key/value pair</param>
        /// <param name="value">The value of the key/value pair</param>
        public HashTableNodePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// The key.  The key cannot be changed because it would affect the 
        /// indexing in the hash table.
        /// </summary>
        public TKey Key { get; private set; }

        /// <summary>
        /// The value
        /// </summary>
        public TValue Value { get; set; }
    }
}
