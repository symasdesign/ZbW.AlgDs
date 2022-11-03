using System;

namespace Rekursion
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Summe(1, 5));
            Console.WriteLine(SummeRecursive(1, 5));

            Console.WriteLine(IndexOf('S', "HAUS"));
            Console.WriteLine(IndexOf('K', "HAUS"));
            Console.WriteLine(IndexOfRecursive('S', "HAUS"));
            Console.WriteLine(IndexOfRecursive('K', "HAUS"));

            Console.WriteLine(Factorial(6));
            Console.WriteLine(FactorialRecursive(6));
            // Ausnahme: StackOverflowException
            Console.WriteLine(FactorialRecursive(int.MaxValue));

        }

        public static int Summe(int min, int max)
        {
            int ergebnis = 0;

            while (min <= max)
            {
                ergebnis += min;
                min = min + 1;
            }
            return ergebnis;
        }

        public static int SummeRecursive(int min, int max, int ergebnis = 0)
        {
            if (min > max)
                return ergebnis;

            ergebnis = ergebnis + min;
            min = min + 1;
            return SummeRecursive(min, max, ergebnis);
        }
        
        public static int IndexOf(char c, string s)
        {
            for(int i = 0; i < s.Length; i++)
            {
                if (s[i] == c)
                    return i;
            }
            return -1;
        }

        public static int IndexOfRecursive(char c, string s, int i = 0)
        {
            if (i >= s.Length)
                return -1;

            if (s[i] == c)
                return i;

            return IndexOfRecursive(c, s, ++i);
        }

        public static int Factorial(int n)
        {
            int result = 1;

            for (int i = 1; i <= n; i++)
                result = result * i;

            return result;
        }

        public static int FactorialRecursive(int n)
        {
            if (n == 0)
                return 1;

            return n * FactorialRecursive(n - 1);
        }
    }
}
