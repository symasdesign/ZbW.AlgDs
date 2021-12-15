using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace SortComparison {

    public partial class frmMain : Form {
        private const int DEFAULT_NUMBER_OF_SAMPLES = 100;

        private SortingList array1;
        private SortingList array2;
        private Bitmap bmpsave1;
        private Bitmap bmpsave2;

        private int middleSpacer;
        private int leftSpacer;
        private int rightSpacer;
        private int bottomSpacer;
        private int topSpacer;

        private static readonly Random rand = new Random();

        private Thread thread1;
        private Thread thread2;

        public frmMain() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            FillAlgCombo(this.cboAlg1);
            FillAlgCombo(this.cboAlg2);

            if (this.cboAlg1.Items.Count > 0) {
                this.cboAlg1.SelectedIndex = 0;
                this.cboAlg2.SelectedIndex = 0;
                if (this.cboAlg2.Items.Count > 1) {
                    this.cboAlg2.SelectedIndex = 1;
                }
            }

            tbSamples.Value = DEFAULT_NUMBER_OF_SAMPLES;
            middleSpacer = pnlSort2.Left - (pnlSort1.Left + pnlSort1.Width);
            leftSpacer = pnlSort1.Left;
            rightSpacer = this.Width - (pnlSort2.Left + pnlSort2.Width);
            bottomSpacer = this.Height - (pnlSort1.Top + pnlSort1.Height);
            topSpacer = pnlSort1.Top;
            ddTypeOfData.SelectedIndex = ddTypeOfData.Items.IndexOf("Random");
        }

        private void FillAlgCombo(ComboBox cbo) {
            var list = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                from assemblyType in domainAssembly.GetTypes()
                where typeof(SortAlgorithm).IsAssignableFrom(assemblyType) && !assemblyType.IsAbstract
                select assemblyType).ToArray();

            var items = new List<Tuple<string, Type>>();
            foreach (var t in list) {
                var o = (SortAlgorithm) Activator.CreateInstance(t);
                items.Add(new Tuple<string, Type>(o.Name, t));
            }
            cbo.DataSource = items;
            cbo.DisplayMember = "Item1";
        }

        public void Randomize(SortingList list) {
            for (var i = list.Count - 1; i > 0; i--) {
                var swapIndex = rand.Next(i + 1);
                if (swapIndex != i) {
                    var tmp = list[swapIndex];
                    list[swapIndex] = list[i];
                    list[i] = tmp;
                }
            }
        }

        private void PrepareForSort() {
            resizeGraphics();

            bmpsave1 = new Bitmap(pnlSort1.Width, pnlSort1.Height);
            bmpsave2 = new Bitmap(pnlSort2.Width, pnlSort2.Height);

            pnlSort1.Image = bmpsave1;
            pnlSort2.Image = bmpsave2;

            array1 = new SortingList(tbSamples.Value);
            array2 = new SortingList(tbSamples.Value);
            for (var i = 0; i < array1.Capacity; i++) {
                var y = (int) ((double) (i + 1) / array1.Capacity * pnlSort1.Height);
                array1.Add(y);
            }
            Randomize(array1);

            array2 = (SortingList) array1.Clone();
        }

        private void CmdSort_Click(object sender, EventArgs e) {
            // prevent a bogus "Sort #1 failed!" message
            if (thread1 != null) {
                thread1.Abort();
                thread1.Join();
            }
            if (thread2 != null) {
                thread2.Abort();
                thread2.Join();
            }

            PrepareForSort();

            if (ddTypeOfData.SelectedItem.ToString() == "Random") {
                // ready to go
            } else if (ddTypeOfData.SelectedItem.ToString() == "Sorted") {
                array1.Sort();
                array2 = (SortingList) array1.Clone();
            } else if (ddTypeOfData.SelectedItem.ToString() == "Nearly Sorted") {
                array1.Sort();

                var maxValue = array1.Count / 10;

                // move anywhere from 2 items to 20% of the items
                var itemsToMove = rand.Next(1, maxValue);
                for (var i = 0; i < itemsToMove; i++) {
                    var a = rand.Next(0, array1.Count);
                    var b = rand.Next(0, array1.Count);

                    while (a == b) {
                        a = rand.Next(0, array1.Count);
                        b = rand.Next(0, array1.Count);
                    }

                    var temp = array1[a];
                    array1[a] = array1[b];
                    array1[b] = temp;
                }

                array2 = (SortingList) array1.Clone();
            } else if (ddTypeOfData.SelectedItem.ToString() == "Reversed") {
                array1.Sort();
                array1.Reverse();

                array2 = (SortingList) array1.Clone();
            } else if (ddTypeOfData.SelectedItem.ToString() == "Few Unique") {
                var maxValue = 10;

                if (array1.Count < 100)
                    maxValue = 6;

                // choose a random amount of unique values
                maxValue = rand.Next(2, maxValue);

                var temp = new List<int>();
                for (var i = 0; i < maxValue; i++) {
                    var y = (int) ((double) (i + 1) / maxValue * pnlSort1.Height);
                    temp.Add(y);
                }

                for (var i = 0; i < array1.Count; i++) {
                    array1[i] = temp[rand.Next(0, maxValue)];
                }

                array2 = (SortingList) array1.Clone();
            }

            resizeGraphics();

            var speed = 1;
            for (var i = 0; i < tbSpeed.Value; i++) {
                speed *= 2;
            }

            var alg1 = "";
            var alg2 = "";

            if (cboAlg1.SelectedItem != null)
                alg1 = cboAlg1.SelectedItem.ToString();

            if (cboAlg2.SelectedItem != null)
                alg2 = cboAlg2.SelectedItem.ToString();


            ThreadStart ts1 = null;
            ThreadStart ts2 = null;
            if (!string.IsNullOrEmpty(alg1)) {
                var sa1 = (SortAlgorithm) Activator.CreateInstance(((Tuple<string, Type>) this.cboAlg1.SelectedItem).Item2);
                sa1.Setup(array1, pnlSort1, speed, alg1);
                ts1 = delegate() {
                    sa1.Sort(this.array1);
                    sa1.finishDrawing();

                    if (!isSorted(array1))
                        MessageBox.Show("#1 Sort Failed!");
                };
            }

            if (!string.IsNullOrEmpty(alg2)) {
                var sa2 = (SortAlgorithm) Activator.CreateInstance(((Tuple<string, Type>) this.cboAlg2.SelectedItem).Item2);
                sa2.Setup(array2, pnlSort2, speed, alg2);
                ts2 = delegate() {
                    sa2.Sort(this.array2);
                    sa2.finishDrawing();

                    if (!isSorted(array2))
                        MessageBox.Show("#2 Sort Failed!");
                };
            }

            if (ts1 != null) {
                thread1 = new Thread(ts1);
                thread1.Start();
            }
            if (ts2 != null) {
                thread2 = new Thread(ts2);
                thread2.Start();
            }
        }

        private bool isSorted(IList<int> checkThis) {
            for (var i = 0; i < checkThis.Count - 1; i++) {
                if (((IComparable) checkThis[i]).CompareTo(checkThis[i + 1]) > 0)
                    return false;
            }

            return true;
        }

        private void tbSamples_Scroll(object sender, EventArgs e) {
            lblSamples.Text = "Number of samples: " + tbSamples.Value.ToString("n0");
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e) {
            Environment.Exit(0);
        }

        private void frmMain_Resize(object sender, EventArgs e) {
            resizeGraphics();
        }

        public void resizeGraphics() {
            // change the graphics to the right sizes

            pnlSort1.Height = this.Height - topSpacer - bottomSpacer;
            pnlSort2.Height = pnlSort1.Height;

            if (cboAlg2.SelectedItem == null || cboAlg2.SelectedItem.ToString().Trim() == "") {
                pnlSort2.Left = this.Width + 1;
                pnlSort1.Width = (this.Width - leftSpacer - rightSpacer);
                pnlSort2.Width = pnlSort1.Width;
            } else if (cboAlg1.SelectedItem == null || cboAlg1.SelectedItem.ToString().Trim() == "") {
                pnlSort1.Left = this.Width + 1;
                pnlSort1.Width = (this.Width - leftSpacer - rightSpacer);
                pnlSort2.Width = pnlSort1.Width;
                pnlSort2.Left = leftSpacer;
            } else {
                pnlSort1.Width = (this.Width - leftSpacer - rightSpacer - middleSpacer) / 2;
                pnlSort2.Width = pnlSort1.Width;

                pnlSort1.Left = leftSpacer;
                pnlSort2.Left = pnlSort1.Left + pnlSort1.Width + middleSpacer;
            }
        }
    }
}