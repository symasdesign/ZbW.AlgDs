using System;
using My.Collections;

namespace CalculateExpressions
{
    class Program
    {
        static void Main(string[] args)
        {

            // 2.3.3 Aufgaben
            // 1)
            Console.WriteLine(Fakultaet(5));
            Console.WriteLine(FakultaetIterativ(5));

            // 5)
            string expression;

            Console.WriteLine("Eingabe eines arithmetischen Ausdrucks");
            Console.WriteLine("Auswertung von links nach rechts mit Leerzeichen");
            Console.WriteLine("z.B. 2 + 3 * 4, Ergebnis = 20");

            while(!String.IsNullOrEmpty(expression = Console.ReadLine()))
            {
                Console.WriteLine("Ergebnis: " + expression.EvaluateExpression());
            }
        }

        public static int FakultaetIterativ(int n)
        {
            Stack<int> stack = new Stack<int>();

            for (; n > 1; n--)
                stack.Push(n);
            while (stack.Count != 0)
                n *= stack.Pop();
            return n;
        }

        public static int Fakultaet(int n)
        {
            if (n == 0)
                return 1;
            else
                return n * Fakultaet(n - 1);
        }


    }
}
