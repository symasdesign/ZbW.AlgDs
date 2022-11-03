using System;
using System.IO;
using My.Collections;

namespace CompanySearch
{
    public class Companies
    {
        private BinaryTree<string, Company> companies = new BinaryTree<string, Company>();

        public Companies(TextReader r)
        {
            // Spaltenüberschriften auslesen
            var tokens = r.ReadLine().Split(new char[] { ';' });
            if (tokens.Length != 3)
                throw new ArgumentException("More than 3 columns in company file");

            string line;
            while ((line = r.ReadLine()) != null)
            {
                tokens = line.Split(new char[] { ';' });

                companies.Add(tokens[0], new Company()
                {
                    Bezeichnung = tokens[0],
                    Branche = tokens[1],
                    Ort = tokens[2]
                });
            }
            r.Close();
        }

        public Company Read(string bezeichnung)
        {
            if (companies.Contains(bezeichnung))
                return companies[bezeichnung];

            return null;
        }
    }

    public class Company
    {
        public string Bezeichnung { get; set; }
        public string Branche { get; set; }
        public string Ort { get; set; }

        public override string ToString()
        {
            return $"Bezeichnung: {Bezeichnung}\tBranche: {Branche}\tOrt: {Ort}";
        }
    }
}
