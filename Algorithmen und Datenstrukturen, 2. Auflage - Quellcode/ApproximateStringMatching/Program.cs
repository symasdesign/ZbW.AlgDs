using System;

namespace ApproximateStringMatching
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ARRRSSS".Jaro("ASSSRRR"));

            Console.WriteLine("Cordts".Jaro("Sortdz"));

            Console.WriteLine("Cordts".Jaro("Kurdz"));
            Console.WriteLine("Halse".Jaro("Haute"));
            Console.WriteLine("Quantität".Jaro("Qualität"));
            Console.WriteLine("Haus".Jaro("Maus"));
            Console.WriteLine("Psychologie".Jaro("Physiologie"));
        }
    }
}
