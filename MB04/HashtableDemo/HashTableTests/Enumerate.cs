using System;
using System.Collections.Generic;
using HashTable;
using NUnit.Framework;

namespace HashTableTests
{
    [TestFixture]
    class Enumerate
    {
        [Test]
        public void Enumerate_Keys_Empty()
        {
            HashTable<string, string> table = new HashTable<string, string>();

            foreach (string key in table.Keys)
            {
                Assert.Fail("There should be nothing in the Keys collection");
            }
        }

        [Test]
        public void Enumerate_Values_Empty()
        {
            HashTable<string, string> table = new HashTable<string, string>();

            foreach (string key in table.Values)
            {
                Assert.Fail("There should be nothing in the Values collection");
            }
        }

        [Test]
        public void Enumerate_Keys_Populated()
        {
            HashTable<int, string> table = new HashTable<int, string>();

            List<int> keys = new List<int>();
            for (int i = 0; i < 100; i++)
            {
                int value = _rng.Next();
                while (table.ContainsKey(value))
                {
                    value = _rng.Next();
                }
                                
                keys.Add(value);
                table.Add(value, value.ToString());
            }

            foreach (int key in table.Keys)
            {
                Assert.IsTrue(keys.Remove(key), "The key was missing from the keys collection");
            }

            Assert.AreEqual(0, keys.Count, "There were left over values in the keys collection");
        }

        [Test]
        public void Enumerate_Values_Populated()
        {
            HashTable<int, string> table = new HashTable<int, string>();

            List<string> values = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                int value = _rng.Next();
                while (table.ContainsKey(value))
                {
                    value = _rng.Next();
                }

                values.Add(value.ToString());
                table.Add(value, value.ToString());
            }

            foreach (string value in table.Values)
            {
                Assert.IsTrue(values.Remove(value), "The key was missing from the values collection");
            }

            Assert.AreEqual(0, values.Count, "There were left over values in the value collection");
        }

        Random _rng = new Random();
    }
}
