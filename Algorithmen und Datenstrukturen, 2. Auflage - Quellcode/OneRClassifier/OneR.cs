using System;
using My.Collections;
using System.IO;
using System.Linq;

namespace OneRClassfier
{
    public class OneR
    {
        private class Item
        {
            public string Attribute { get; private set; }
            public string Classification { get; private set; }
            public int Frequency { get; set; }

            public Item(string attribute, string classification, int frequency = 1)
            {
                Classification = classification;
                Attribute = attribute;
                Frequency = frequency;
            }

            public override bool Equals(object obj)
            {
                Item item = obj as Item;

                return (Attribute.Equals(item.Attribute) &&
                        Classification.Equals(item.Classification));
            }

            public override int GetHashCode()
            {
                return Classification.GetHashCode() +
                        Attribute.GetHashCode();
            }
        }

        private class ItemSet
        {
            public string Column { get; private set; }
            public Hashtable<string, Item> Items { get; private set; }
            public double ErrorRate { get; private set; }

            public ItemSet(string column)
            {
                Column = column;
                Items = new Hashtable<string, Item>(10);
            }

            public void AddItem(string attribute, string classification)
            {
                var key = attribute + "->" + classification;

                Item item = null;
                if (Items.ContainsKey(key))
                    item = Items[key];

                if (item == null)
                {
                    item = new Item(attribute, classification);
                    Items[key] = item;
                }
                else
                    item.Frequency++;
            }

            public void Process()
            {
                var result = from Item item in Items.Values()
                             orderby item.Attribute, item.Frequency descending
                             select item;

                int total = 0;
                int correct = 0;
                string attribute = null;

                foreach (var item in result)
                {
                    total += item.Frequency;

                    var key = item.Attribute + "->" + item.Classification;

                    // Gruppenwechsel
                    if (attribute == null || attribute != item.Attribute)
                    {
                        attribute = item.Attribute;
                        correct += item.Frequency;
                    }
                    else
                    {
                        Items.Remove(key);
                    }
                }

                ErrorRate = 100.0 - (correct * 100.0 / total);
            }

            public override string ToString()
            {
                string s = Column + "\n";

                foreach (Item value in Items.Values())
                {
                    s += string.Format("  {0}->{1} : {2}\n",
                        value.Attribute, value.Classification, value.Frequency);
                }

                if (!double.IsNaN(ErrorRate))
                    s += string.Format("\n  ErrorRate: {0} %\n", ErrorRate);

                return s;
            }
        }

        public Hashtable<string, string> Solution { get; private set; }

        public OneR()
        {
            Solution = new Hashtable<string, string>();
        }

        public void Build(TextReader r)
        {
            var list = new ArrayList<ItemSet>();

            var header = r.ReadLine().Split(new char[] { ';' });

            // Spaltenüberschriften lesen
            foreach (var head in header)
            {
                list.Add(new ItemSet(head));
            }

            // Daten einlesen und Häufigkeiten ermitteln
            string line;

            while ((line = r.ReadLine()) != null)
            {
                var tokens = line.Split(new char[] { ';' });

                string classfication = tokens[0];

                for (int i = 1; i < tokens.Length; i++)
                {
                    string attribute = tokens[i];

                    list[i].AddItem(attribute, classfication);
                }
            }
            r.Close();

            // Gruppen mit bester Vorhersage pro Spalte ermitteln
            foreach (var items in list)
            {
                // vorher: Ausgabe alle Häufigkeitsverteilungen
                //Console.WriteLine(items);
                items.Process();
                // nacher: Ausgabe bester Häufigkeitsverteilungen
                //Console.WriteLine(items);
            }

            // Spalte mit geringster Fehlerrate ermitteln
            var result = (from ItemSet items in list
                          where !double.IsNaN(items.ErrorRate)
                          orderby items.ErrorRate
                          select items);

            ItemSet solution = result.First();

            // Lösung für Vorhersage speichern
            foreach (Item item in solution.Items.Values())
                Solution.Add(item.Attribute, item.Classification);

            Console.WriteLine("".PadLeft(80, '_'));
            Console.WriteLine("Regeln: " + solution);

            list.Clear();
        }

        public string Classify(string value)
        {
            return Solution[value];
        }
    }
}
