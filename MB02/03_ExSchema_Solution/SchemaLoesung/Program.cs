using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SchemaDemo {
    class Program {
        static void Main(string[] args) {
            var pegSource = new Peg("Source", 1) {4, 3, 2, 1};
            var pegDestination = new Peg("Destination", 2);
            var pegHelp = new Peg("Help", 3);

            MoveTowersOfHanoi(pegSource.Count, pegSource, pegDestination, pegHelp);

            // -------------------------------------------------------------------------------------------

            var stopWatch = new Stopwatch();
            var observations = "";
            for (var i = 10; i <= 42; i++) {
                stopWatch.Restart();
                //Console.WriteLine("\nStart FibonacciRecursive");
                var res = FibonacciRecursive(i);
                //Console.WriteLine($"FibonacciRecursive({i}) = " + res);
                //Console.WriteLine("Time elapsed: {0:0.0000}", stopWatch.ElapsedMilliseconds / 1000.0);
                stopWatch.Stop();
                observations += $"{i}\t{stopWatch.ElapsedTicks}\n";
            }

            observations = "";
            for (var i = 1; i <= 50000; i++) {
                stopWatch.Restart();
                //Console.WriteLine("\nStart FibonacciIterative");
                var res = Fibonacci(i);
                //Console.WriteLine($"Fibonacci({i}) = " + res);
                //Console.WriteLine("Time elapsed: {0:0.0000}", stopWatch.ElapsedMilliseconds / 1000.0);
                stopWatch.Stop();
                observations += $"{i}\t{stopWatch.ElapsedTicks/100}\n";
            }
            observations = "";
            for (var i = 1; i <= 15; i++) {
                stopWatch.Restart();
                //Console.WriteLine("\nStart FibonacciIterative");
                HanoiSimple(i, "A", "B", "C");
                //Console.WriteLine($"Fibonacci({i}) = " + res);
                //Console.WriteLine("Time elapsed: {0:0.0000}", stopWatch.ElapsedMilliseconds / 1000.0);
                stopWatch.Stop();
                observations += $"{i}\t{stopWatch.ElapsedTicks / 100}\n";
            }
            // -------------------------------------------------------------------------------------------

            Console.ReadLine();
        }


        /// <summary>
        /// Fakultät iterativ
        /// </summary>
        public static int Factorial(int n) {
            var result = 1;

            for (var i = 1; i <= n; i++)
                result = result * i;

            return result;
        }

        public static int FactorialRecursive(int n) {
            if (n == 0)
                return 1;

            return n * FactorialRecursive(n - 1);
        }

        public static long FibonacciRecursive(long len) {
            if (len == 1 || len == 2) {
                return 1;
            }
            return FibonacciRecursive(len - 1) + FibonacciRecursive(len - 2);
        }

        public static long Fibonacci(long a) {

            long fib = 1;

            // Schleife wird nur abgearbeitet, wenn n >= 3
            // bis n = 92 werden die Fibonaccizahlen korrekt
            // berechnet. Dann ist der Bereich von long zu klein

            for (long fib1 = 1, fib2 = 1, i = 3; i <= a; i++) {
                fib = fib1 + fib2;
                fib1 = fib2;
                fib2 = fib;
            }
            return fib;
        }

        private static void HanoiSimple(int n, string start, string hilf, string ziel) {
            //// Dies entspricht dem in den Slides beschriebenen Algorithmus (Vereinfachung - siehe unten)
            //if (n > 0) {
            //    Bewege(n - 1, start, ziel, hilf);  // es werden die oberen n-1 Scheiben auf den Hilfsstab verschoben
            //    Console.WriteLine(" Scheibe von Platz " + start + " auf Platz " + ziel + ".");   // Die einzig verbleibende Scheibe wird zum Ziel verschoben
            //    Bewege(n - 1, hilf, start, ziel);  // nun wird der Turm, der sich auf dem Hilfsstab befindet zum Ziel verschoben
            //}

            if (n == 1) {
                //Bewege(n - 1, start, ziel, hilf); // bewirkt nichts, da 0 übergeben wird
                Console.WriteLine(" Scheibe von Platz " + start + " auf Platz " + ziel + ".");
                //Bewege(n - 1, hilf, start, ziel); // bewirkt nichts, da 0 übergeben wird
            } else if (n > 1) {
                HanoiSimple(n - 1, start, ziel, hilf); // es werden die oberen n-1 Scheiben auf den Hilfsstab verschoben
                Console.WriteLine(" Scheibe von Platz " + start + " auf Platz " + ziel + "."); // Die einzig verbleibende Scheibe wird zum Ziel verschoben
                HanoiSimple(n - 1, hilf, start, ziel); // nun wird der Turm, der sich auf dem Hilfsstab befindet zum Ziel verschoben
            }
        }

        public class Peg : List<int> {
            public Peg(string name, int pos) {
                this.Name = name;
                this.Pos = pos;
            }

            public readonly string Name;
            public readonly int Pos;
        }

        public static void MoveTowersOfHanoi(int n, Peg pegSource, Peg pegDestination, Peg pegHelp) {
            if (n == 1) {
                var disc = pegSource.Last();
                pegSource.Remove(disc);
                pegDestination.Add(disc);
                Console.WriteLine($"Move {disc} from {pegSource.Name} to {pegDestination.Name}:");
                DrawPegs(pegSource, pegDestination, pegHelp);
            } else {
                MoveTowersOfHanoi(n - 1, pegSource, pegHelp, pegDestination);
                var disc = pegSource.Last();
                pegSource.Remove(disc);
                pegDestination.Add(disc);
                Console.WriteLine($"Move {disc} from {pegSource.Name} to {pegDestination.Name}:");
                DrawPegs(pegSource, pegDestination, pegHelp);
                MoveTowersOfHanoi(n - 1, pegHelp, pegDestination, pegSource);
            }
        }

        public static void DrawPegs(Peg pegSource, Peg pegDestination, Peg pegHelp) {
            var a = (pegSource.Pos == 1 ? pegSource : pegDestination.Pos == 1 ? pegDestination : pegHelp);
            var b = (pegSource.Pos == 2 ? pegSource : pegDestination.Pos == 2 ? pegDestination : pegHelp);
            var c = (pegSource.Pos == 3 ? pegSource : pegDestination.Pos == 3 ? pegDestination : pegHelp);
            var highest = 0;
            if (a.Any()) {
                highest = Math.Max(highest, a.Max());
            }
            if (b.Any()) {
                highest = Math.Max(highest, b.Max());
            }
            if (c.Any()) {
                highest = Math.Max(highest, c.Max());
            }
            Console.WriteLine();
            for (var i = highest; i > 0; i--) {
                var txt = "".PadRight(6);
                if (a.Count >= i) {
                    txt += a[i - 1].ToString().PadRight(7);
                } else {
                    txt += "".PadRight(7);
                }
                if (b.Count >= i) {
                    txt += b[i - 1].ToString().PadRight(7);
                } else {
                    txt += "".PadRight(7);
                }
                if (c.Count >= i) {
                    txt += c[i - 1].ToString().PadRight(7);
                } else {
                    txt += "".PadRight(7);
                }

                Console.WriteLine(txt);
            }
            Console.WriteLine("      |      |      |");
            Console.WriteLine("----------------------------");
        }

    }
}