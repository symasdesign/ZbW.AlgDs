using System;
using HashTable;
using NUnit.Framework;

namespace HashTableTests
{
    [TestFixture]
    class Get
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_From_Empty()
        {
            HashTable<string, int> table = new HashTable<string, int>();
            int value = table["missing"];
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Get_Missing()
        {
            HashTable<string, int> table = new HashTable<string, int>();
            table.Add("100", 100);

            int value = table["missing"];
        }

        [Test]
        public void Get_Succeeds()
        {
            HashTable<string, int> table = new HashTable<string, int>();
            table.Add("100", 100);

            int value = table["100"];
            Assert.AreEqual(100, value, "The returned value was incorrect");

            for (int i = 0; i < 100; i++)
            {
                table.Add(i.ToString(), i);

                value = table["100"];
                Assert.AreEqual(100, value, "The returned value was incorrect");
            }
        }

        [Test]
        public void TryGet_From_Empty()
        {
            HashTable<string, int> table = new HashTable<string, int>();
            int value;

            Assert.IsFalse(table.TryGetValue("missing", out value));
        }

        [Test]
        public void TryGet_Missing()
        {
            HashTable<string, int> table = new HashTable<string, int>();
            table.Add("100", 100);

            int value;
            Assert.IsFalse(table.TryGetValue("missing", out value));
        }

        [Test]
        public void TryGet_Succeeds()
        {
            HashTable<string, int> table = new HashTable<string, int>();
            table.Add("100", 100);

            int value;
            Assert.IsTrue(table.TryGetValue("100", out value));
            Assert.AreEqual(100, value, "The returned value was incorrect");

            for (int i = 0; i < 100; i++)
            {
                table.Add(i.ToString(), i);

                Assert.IsTrue(table.TryGetValue("100", out value));
                Assert.AreEqual(100, value, "The returned value was incorrect");
            }
        }
    }
}
