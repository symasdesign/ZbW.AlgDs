using System;
using System.IO;
using System.Text;

namespace CompanySearch
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
                throw new ArgumentException("Aufruf: CompanySearch <file.csv>");

            // CSV einlesen
            var companies = new Companies(new StreamReader(args[0], Encoding.Default));

            while (true)
            {
                Console.Write("Unternehmensbezeichnung eingeben: ");
                string name = Console.ReadLine();
                if (string.IsNullOrEmpty(name))
                    break;

                Company company = companies.Read(name);

                if (company == null)
                    Console.WriteLine("Unternehmen nicht gefunden!");
                else
                    Console.WriteLine(company + "\n");
            }
        }
    }
}
