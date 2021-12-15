using HashTable;
using NUnit.Framework;

namespace HashTableTests
{
    [TestFixture]
    class Remove
    {
        [Test]
        public void Remove_Empty()
        {
            HashTable<string, int> table = new HashTable<string, int>();
            Assert.IsFalse(table.Remove("missing"), "Remove on an empty collection should return false");

        }

        [Test]
        public void Remove_Missing()
        {
            HashTable<string, int> table = new HashTable<string, int>();
            table.Add("100", 100);

            Assert.IsFalse(table.Remove("missing"), "Remove on an empty collection should return false");
        }

        [Test]
        public void Remove_Found()
        {
            HashTable<string, int> table = new HashTable<string, int>();
            for (int i = 0; i < 100; i++)
            {
                table.Add(i.ToString(), i);
            }

            for (int i = 0; i < 100; i++)
            {
                Assert.IsTrue(table.ContainsKey(i.ToString()), "The key was not found in the collection");
                Assert.IsTrue(table.Remove(i.ToString()), "The value was not removed (or remove returned false)");
                Assert.IsFalse(table.ContainsKey(i.ToString()), "The key should not have been found in the collection");
            }
        }
    }
}
