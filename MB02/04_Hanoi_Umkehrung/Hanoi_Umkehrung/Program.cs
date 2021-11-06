using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanoi_Umkehrung {
    class Program {
        static void Main(string[] args) {
            Bewege(3, "A", "B", "C");

            var ret = Umkehrung("GRAS");
        }

        private static void Bewege(int n, string start, string hilf, string ziel) {
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
            }
            else if (n > 1) {
                Bewege(n - 1, start, ziel, hilf); // es werden die oberen n-1 Scheiben auf den Hilfsstab verschoben
                Console.WriteLine(" Scheibe von Platz " + start + " auf Platz " + ziel + "."); // Die einzig verbleibende Scheibe wird zum Ziel verschoben
                Bewege(n - 1, hilf, start, ziel); // nun wird der Turm, der sich auf dem Hilfsstab befindet zum Ziel verschoben
            }
        }

        public static string Umkehrung(string text) {
            if (text.Length == 1 || String.IsNullOrEmpty(text)) {
                return text;
            }
            else {
                var rest = text.Substring(1);
                var res = Umkehrung(rest);
                return res + text.Substring(0, 1);
            }
        }

        public static string Umkehrung0(string text) {
            if (text.Length == 1 || String.IsNullOrEmpty(text)) {
                return text;
            }
            else {
                var rest = text.Substring(1);
                var res = Umkehrung1(rest);
                return res + text.Substring(0, 1);
            }
        }

        public static string Umkehrung1(string text) {
            if (text.Length == 1 || String.IsNullOrEmpty(text)) {
                return text;
            }
            else {
                var rest = text.Substring(1);
                var res = Umkehrung2(rest);
                return res + text.Substring(0, 1);
            }
        }

        public static string Umkehrung2(string text) {
            if (text.Length == 1 || String.IsNullOrEmpty(text)) {
                return text;
            }
            else {
                var rest = text.Substring(1);
                var res = Umkehrung3(rest);
                return res + text.Substring(0, 1);
            }
        }

        public static string Umkehrung3(string text) {
            if (text.Length == 1 || String.IsNullOrEmpty(text)) {
                return text;
            }
            else {
                var rest = text.Substring(1);
                var res = Umkehrung0(rest);
                return res + text.Substring(0, 1);
            }
        }
    }
}