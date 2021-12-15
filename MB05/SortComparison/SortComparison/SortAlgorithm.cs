using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace SortComparison {
    public abstract class SortAlgorithm {
        private SortingList arrayToSort;
        private Graphics g;
        private Bitmap bmpsave;
        private PictureBox pnlSamples;

        private int operationsPerFrame; // operations per frame
        private int frameMS; // time between frames (aim for 40 ms = 25 fps)

        private int operationCount;

        private readonly HashSet<int> highlightedIndexes = new HashSet<int>(); // highlight all of these indexes in the frame

        private DateTime nextFrameTime;
        private int originalPanelHeight;

        public abstract string Name { get; }

        // WICHTIG: Damit die Visualisierung funktioniert, muss eine "InPlace-Sortierung" durchgeführt werden.
        //          D.h. arrayToSort muss sortiert werden - es darf keine neue Liste erzeugt werden.
        //          Deswegen hat die Methode auch keinen Rückgabewert.
        public abstract void Sort(IList<int> arrayToSort);

        public void Setup(SortingList list, PictureBox pic, int s, string outFile) {
            list.OnHighlighting += (source, args) => {
                this.HighlightIndex(args.Index);
            };
            arrayToSort = list;
            pnlSamples = pic;

            operationCount = 0;
            operationsPerFrame = s;
            frameMS = 1000; // so now operationsPerFrame is operations per second

            // reduce the frame wait for better visuals (increased frame rate)
            while (frameMS >= 40 && operationsPerFrame > 1) {
                operationsPerFrame = operationsPerFrame / 2;
                frameMS = frameMS / 2;
            }

            bmpsave = new Bitmap(pnlSamples.Width, pnlSamples.Height);
            g = Graphics.FromImage(bmpsave);
            originalPanelHeight = pnlSamples.Height;
            pnlSamples.Image = bmpsave;
            nextFrameTime = DateTime.UtcNow;

            checkForFrame();
        }

        protected void HighlightIndex(int index) {
            this.highlightedIndexes.Add(index);

            operationCount++;
            checkForFrame();

        }

        private void checkForFrame() {
            lock (this.sync) {
                if (operationCount >= operationsPerFrame || nextFrameTime <= DateTime.UtcNow) {
                    // time to draw a new frame and wait
                    DrawSamples();
                    RefreshPanel(pnlSamples);

                    // prepare for next frame
                    highlightedIndexes.Clear();
                    operationCount -= operationsPerFrame; // if there were more operations than needed, don't just forget those

                    if (DateTime.UtcNow < nextFrameTime) {
                        Thread.Sleep((int) ((nextFrameTime - DateTime.UtcNow).TotalMilliseconds));
                    }

                    nextFrameTime = nextFrameTime.AddMilliseconds(frameMS);
                }
            }
        }

        public void finishDrawing() {
            if (highlightedIndexes.Count > 0) {
                // put one last frame in before the end
                nextFrameTime = DateTime.UtcNow;
                checkForFrame();
            }

            // draw the last frame
            nextFrameTime = DateTime.UtcNow;
            checkForFrame();
        }




        private void RefreshPanel(Control pnlSort) {
            if (pnlSort.InvokeRequired) {
                pnlSort.Invoke((MethodInvoker) delegate { this.RefreshPanel(pnlSort); });
            } else {
                pnlSort.Refresh();
            }
        }

        private object sync = new object();
        public void DrawSamples() {
            lock(this.sync) {
                using (this.arrayToSort.BlockHighlighting()) {
                    // might need to grow or shrink if size is different from original (can't change array!)
                    double multiplyHeight = 1;

                    // check if need to change size
                    if (bmpsave.Width != pnlSamples.Width || bmpsave.Height != pnlSamples.Height) {
                        bmpsave = new Bitmap(pnlSamples.Width, pnlSamples.Height);
                        g = Graphics.FromImage(bmpsave);
                        pnlSamples.Image = bmpsave;
                    }

                    if (pnlSamples.Height != originalPanelHeight) {
                        multiplyHeight = (pnlSamples.Height) / (double) (originalPanelHeight);
                    }

                    // start with white background
                    g.Clear(Color.White);

                    // use black sometimes
                    var pen = new Pen(Color.Black);
                    var b = new SolidBrush(Color.Black);

                    // use red sometimes
                    var redPen = new Pen(Color.Red);
                    var redBrush = new SolidBrush(Color.Red);

                    // draw a nice width based on number of elements
                    var w = (pnlSamples.Width / arrayToSort.Count) - 1;

                    for (var i = 0; i < this.arrayToSort.Count; i++) {
                        var x = (int) (((double) pnlSamples.Width / arrayToSort.Count) * i);

                        var itemHeight = (int) Math.Round(Convert.ToDouble(arrayToSort[i]) * multiplyHeight);

                        if (highlightedIndexes.Contains(i)) {
                            // draw highlighed versions
                            if (w <= 1) {
                                g.DrawLine(redPen, new Point(x, pnlSamples.Height), new Point(x, (pnlSamples.Height - itemHeight)));
                            } else {
                                g.FillRectangle(redBrush, x, pnlSamples.Height - itemHeight, w, pnlSamples.Height);
                            }
                        } else {
                            // draw normal versions
                            if (w <= 1) {
                                g.DrawLine(pen, new Point(x, pnlSamples.Height), new Point(x, (pnlSamples.Height - itemHeight)));
                            } else {
                                g.FillRectangle(b, x, pnlSamples.Height - itemHeight, w, pnlSamples.Height);
                            }
                        }
                    }
                }
            }
        }
    }
}