using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

using My.Collections;
using System.Drawing.Drawing2D;
using System.Globalization;


namespace PostScriptViewer
{
    public class Viewer
    {
        private ArrayList<string[]> commands = new ArrayList<string[]>();

        private Pen p = new Pen(Color.Black);
        private int x1 = 1, y1 = 1;

        public Viewer(string path)
        {
            using (var r = new StreamReader(path))
            {
                string line;

                while ((line = r.ReadLine()) != null)
                {
                    string[] tokens = line.Split(
                        new char[] { ' ' },
                        StringSplitOptions.RemoveEmptyEntries);

                    if (tokens.Length > 0)
                        commands.Add(tokens);
                }
            }
        }

        public void Paint(Graphics g)
        {
            Transform(g);

            foreach (var command in commands)
            {
                var stack = new Stack<string>();

                foreach (string param in command)
                    stack.Push(param);

                switch (stack.Pop().ToLower())
                {
                    case "setlinewidth":
                        {
                            p.Width = int.Parse(stack.Pop());
                        }
                        break;

                    case "setrgbcolor":
                        {
                            // amerikanische Schreibweise für Zahlen in PostScript
                            var ci = CultureInfo.InvariantCulture;

                            int blue = (int)(double.Parse(stack.Pop(), ci) * 255);
                            int green = (int)(double.Parse(stack.Pop(), ci) * 255);
                            int red = (int)(double.Parse(stack.Pop(), ci) * 255);

                            p.Brush = new SolidBrush(Color.FromArgb(red, green, blue));
                        }
                        break;

                    case "arc":
                        {
                            float sweepAngle = float.Parse(stack.Pop());
                            float startAngle = float.Parse(stack.Pop());
                            float radius = float.Parse(stack.Pop());
                            float y = float.Parse(stack.Pop());
                            float x = float.Parse(stack.Pop());

                            g.DrawArc(p, x - radius, y - radius, radius * 2, radius * 2, startAngle, sweepAngle);
                        }
                        break;

                    case "moveto":
                        {
                            y1 = int.Parse(stack.Pop());
                            x1 = int.Parse(stack.Pop());
                        }
                        break;
                    case "lineto":
                        {
                            int y2 = int.Parse(stack.Pop());
                            int x2 = int.Parse(stack.Pop());

                            g.DrawLine(p, x1, y1, x2, y2);

                            x1 = x2;
                            y1 = y2;
                        }
                        break;
                }
            }
        }

        private void Transform(Graphics g)
        {
            // kartesisches Koordinatensystem
            var matrix = new Matrix(1, 0, 0, -1, 0, 0);
            matrix.Translate(0, -Screen.PrimaryScreen.WorkingArea.Height);
            g.Transform = matrix;
        }
    }
}
