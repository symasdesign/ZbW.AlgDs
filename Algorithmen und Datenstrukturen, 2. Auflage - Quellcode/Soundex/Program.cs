using System;


namespace Soundex
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Muster".Soundex());
            Console.WriteLine("Mayer".Soundex());
            Console.WriteLine("Meyer".Soundex());
            Console.WriteLine("Mair".Soundex());
            Console.WriteLine("Mohr".Soundex());
        }
    }
}
