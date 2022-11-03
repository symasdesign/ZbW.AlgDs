using System;

namespace MusicSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
                throw new ArgumentException("Aufruf: MusicSearch <folder>");

            var db = new MusicDB(args[0]);

            string search;
            while((search = Console.ReadLine()) != "")
            {
                foreach (MusicDB.Disc title in db.FuzzySearch(search))
                    Console.WriteLine(title);
            }
        }
    }
}
