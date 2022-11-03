using System;
using My.Collections;

namespace CalculateExpressions
{
    public static class StringExtension
    {
        public static bool IsNumeric(this string s)
        {
            double d;
            return double.TryParse(s, out d);
        }

        public static double EvaluateExpression(this string s)
        {
            var ops = new Stack<string>();
            var vals = new Stack<double>();

            string[] tokens = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var t in tokens)
            {
                if (t.IsNumeric())
                    vals.Push(double.Parse(t));
                else
                    ops.Push(t);

                if (vals.Count == 2)
                {
                    var v2 = vals.Pop();
                    var v1 = vals.Pop();

                    switch (ops.Pop())
                    {
                        case "+":
                            vals.Push(v1 + v2);
                            break;
                        case "-":
                            vals.Push(v1 - v2);
                            break;
                        case "*":
                            vals.Push(v1 * v2);
                            break;
                        case "/":
                            vals.Push(v1 / v2);
                            break;
                    }
                }
            }

            return vals.Pop();
        }
    }
}
