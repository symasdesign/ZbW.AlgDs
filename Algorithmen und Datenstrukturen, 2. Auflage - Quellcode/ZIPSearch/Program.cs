using System;

namespace ZIPSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            var zip = new ZIP(@"..\Testdaten\ZIPCodes.csv");

            int zipcode;
            do
            {
                Console.WriteLine("ZIPCode eingeben, z.B. 35036: ");
                zipcode = int.Parse(Console.ReadLine());
                ZIP.Location location = zip.SearchLocation(zipcode);
                if (location != null)
                    Console.WriteLine(location);
                else
                    Console.WriteLine("Nicht gefunden!");

            } while (zipcode != 0);
        }
    }
}
