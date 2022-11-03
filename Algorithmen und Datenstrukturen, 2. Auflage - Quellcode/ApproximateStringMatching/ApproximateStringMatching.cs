using System;

namespace ApproximateStringMatching
{
    public static class StringExtension
    {
        public static double Levenshtein(this string text1, string text2)
        {
            var length1 = text1.Length;
            var length2 = text2.Length;

            var distance = new int[length1 + 1, length2 + 1];

            for(int x = 0; x <= length1; x++)
                distance[x, 0] = x; 
            for(int y = 0; y <= length2; y++)
                distance[0, y] = y;

            for (int x = 1; x <= length1; x++)
            {
                for (int y = 1; y <= length2; y++)
                {
                    int cost = text1[x - 1] == text2[y - 1] ? 0 : 1;

                    distance[x, y] = Min(distance[x - 1, y] + 1,
                                         distance[x, y - 1] + 1,
                                         distance[x - 1, y - 1] + cost);
                }
            }
            return distance[length1, length2];
        }

        private static int Min(int a, int b, int c)
        {
            return Math.Min(a, Math.Min(b, c));
        }

        public static double Jaro(this string text1, string text2)
        {
            double length1 = text1.Length;
            double length2 = text2.Length;

            // zweimal in umgekehrter Reihenfolge, um Transpositionen zu ermitteln
            string common1 = CommonChars(text1, text2); 
            string common2 = CommonChars(text2, text1);

            if (common1.Length == 0 || common1.Length != common2.Length)
                return 0;

            double common = common1.Length;

            int transpositions = CalcTranspositions(common1, common2);

            double similarity = (common / length1 + common / length2 + (common - transpositions) / common) / 3.0;
            return double.IsNaN(similarity) ? 0.0 : similarity;
        }

        private static string CommonChars(string text1, string text2)
        {
            int length1 = text1.Length, length2 = text2.Length;
            
            int halflen = Math.Min(length1, length2) / 2; // halbe Länge der kürzeren Zeichenkette

            var common = "";

            for (int x = 0; x < length1; x++)
            {
                for (int y = Math.Max(0, x - halflen); y < Math.Min(x + halflen, length2); y++)
                {
                    if (text1[x] == text2[y])
                    {
                        common += text1[x];
                        break;
                    }
                }
            }

            return common;
        }

        private static int CalcTranspositions(string common1, string common2)
        {
            int transpositions = 0;

            for (int pos = 0; pos < common1.Length; pos++)
            {
                if (common1[pos] != common2[pos])
                    transpositions++;
            }
            return transpositions / 2;
        }
    }
}
