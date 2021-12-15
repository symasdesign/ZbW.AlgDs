using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HashtableImpl.Tests {
    [TestClass()]
    public class HashtableTests {
        private Hashtable ht;
        [TestInitialize]
        public void Setup() {
            this.ht = new Hashtable();
        }


        [TestMethod]
        public void TestInsert() {
            Assert.IsTrue(this.ht.Put(new Element { Id = "Meier", Name = "St.Gallen" }));
            Assert.IsTrue(this.ht.Put(new Element { Id = "Wagner", Name = "Wil" }));
            Assert.AreEqual("Wil", this.ht.Get("Wagner").Name);
            Assert.AreEqual("St.Gallen", this.ht.Get("Meier").Name);
            Assert.IsTrue(this.ht.Put(new Element { Id = "Müller", Name = "Gossau" }));
            Assert.AreEqual("Gossau", this.ht.Get("Müller").Name);
        }

        [TestMethod]
        public void TestDoppelteEintraege() {
            Assert.IsTrue(this.ht.Put(new Element { Id = "Wagner", Name = "Wil" }));
            Assert.IsTrue(this.ht.Put(new Element { Id = "Müller", Name = "Gossau" }));
            Assert.AreEqual("Gossau", this.ht.Get("Müller").Name);
            Assert.IsFalse(this.ht.Put(new Element { Id = "Müller", Name = "Gossau" }));
            Assert.IsTrue(this.ht.Put(new Element { Id = "Meier", Name = "St.Gallen" }));
            Assert.IsFalse(this.ht.Put(new Element { Id = "Meier", Name = "St.Gallen" }));
        }

        [TestMethod]
        public void TestGet() {
            Assert.AreEqual(true, ht.Put(new Element { Id = "Meier", Name = "St.Gallen" }));
            Assert.AreEqual(true, ht.Put(new Element { Id = "Wagner", Name = "Wil" }));
            Assert.AreEqual(true, ht.Put(new Element { Id = "Müller", Name = "Gossau" }));
            Assert.AreEqual("Wil", ht.Get("Wagner").Name);
            Assert.AreEqual("St.Gallen", ht.Get("Meier").Name);
            Assert.AreEqual("Gossau", ht.Get("Müller").Name);
        }
    }
}