using System;
using System.Drawing;
using My.Collections;

namespace Paint
{
    class PaintTools
    {
        public static void FloodFill(Bitmap b, Point p, Color newColor)
        {
            Color oldColor = b.GetPixel(p.X, p.Y);

            Stack<Point> stack = new Stack<Point>();

            stack.Push(p);

            while (stack.Count != 0)
            {
                p = stack.Pop();

                if (b.GetPixel(p.X, p.Y).ToArgb() == oldColor.ToArgb())
                {
                    b.SetPixel(p.X, p.Y, newColor);

                    // 4-Neighbor
                    stack.Push(new Point(p.X, p.Y + 1)); // unten
                    stack.Push(new Point(p.X + 1, p.Y)); // rechts
                    stack.Push(new Point(p.X, p.Y - 1)); // oben
                    stack.Push(new Point(p.X - 1, p.Y)); // links vom Punkt

                    // bei 8-Neighbor zusätzlich...
                    stack.Push(new Point(p.X + 1, p.Y + 1)); // unten-rechts
                    stack.Push(new Point(p.X - 1, p.Y + 1)); // unten-links
                    stack.Push(new Point(p.X + 1, p.Y - 1)); // oben-rechts
                    stack.Push(new Point(p.X - 1, p.Y - 1)); // oben-links
                }
            }
        }

        // bei großen zu füllenden Flächen Stapelüberlauf
        public static void FloodFillRecursive(Bitmap b, Point p, Color newColor, Color oldColor)
        {
            if (b.GetPixel(p.X, p.Y).ToArgb() != oldColor.ToArgb())
                return;

            b.SetPixel(p.X, p.Y, newColor);

            // 4-Neighbor
            FloodFillRecursive(b, new Point(p.X, p.Y + 1), newColor, oldColor); // unten
            FloodFillRecursive(b, new Point(p.X + 1, p.Y), newColor, oldColor); // rechts
            FloodFillRecursive(b, new Point(p.X, p.Y - 1), newColor, oldColor); // oben
            FloodFillRecursive(b, new Point(p.X - 1, p.Y), newColor, oldColor); // links vom Punkt
        }
    }
}
