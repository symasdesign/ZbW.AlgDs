namespace SortComparison
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cboAlg1 = new System.Windows.Forms.ComboBox();
            this.cboAlg2 = new System.Windows.Forms.ComboBox();
            this.cmdSort = new System.Windows.Forms.Button();
            this.pnlSort1 = new System.Windows.Forms.PictureBox();
            this.pnlSort2 = new System.Windows.Forms.PictureBox();
            this.tbSamples = new System.Windows.Forms.TrackBar();
            this.lblSamples = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSpeed = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.ddTypeOfData = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSort1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSort2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSamples)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // cboAlg1
            // 
            this.cboAlg1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cboAlg1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAlg1.FormattingEnabled = true;
            this.cboAlg1.Items.AddRange(new object[] {
            "",
            "BiDirectional Bubble Sort",
            "Bubble Sort",
            "Comb Sort",
            "Counting Sort",
            "Cycle Sort",
            "Gnome Sort",
            "Heap Sort",
            "Insertion Sort",
            "Merge Sort (In Place)",
            "Merge Sort (Double Storage)",
            "Odd-Even Sort",
            "Pigeonhole Sort",
            "Quicksort",
            "Quicksort with Insertion Sort",
            "Radix Sort",
            "Selection Sort",
            "Shell Sort",
            "Smoothsort",
            "Timsort"});
            this.cboAlg1.Location = new System.Drawing.Point(13, 219);
            this.cboAlg1.Name = "cboAlg1";
            this.cboAlg1.Size = new System.Drawing.Size(200, 21);
            this.cboAlg1.TabIndex = 2;
            // 
            // cboAlg2
            // 
            this.cboAlg2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cboAlg2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAlg2.FormattingEnabled = true;
            this.cboAlg2.Items.AddRange(new object[] {
            "",
            "BiDirectional Bubble Sort",
            "Bubble Sort",
            "Comb Sort",
            "Counting Sort",
            "Cycle Sort",
            "Gnome Sort",
            "Heap Sort",
            "Insertion Sort",
            "Merge Sort (In Place)",
            "Merge Sort (Double Storage)",
            "Odd-Even Sort",
            "Pigeonhole Sort",
            "Quicksort",
            "Quicksort with Insertion Sort",
            "Radix Sort",
            "Selection Sort",
            "Shell Sort",
            "Smoothsort",
            "Timsort"});
            this.cboAlg2.Location = new System.Drawing.Point(219, 219);
            this.cboAlg2.Name = "cboAlg2";
            this.cboAlg2.Size = new System.Drawing.Size(200, 21);
            this.cboAlg2.TabIndex = 3;
            // 
            // cmdSort
            // 
            this.cmdSort.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmdSort.Location = new System.Drawing.Point(13, 312);
            this.cmdSort.Name = "cmdSort";
            this.cmdSort.Size = new System.Drawing.Size(406, 23);
            this.cmdSort.TabIndex = 5;
            this.cmdSort.Text = "Sort";
            this.cmdSort.UseVisualStyleBackColor = true;
            this.cmdSort.Click += new System.EventHandler(this.CmdSort_Click);
            // 
            // pnlSort1
            // 
            this.pnlSort1.BackColor = System.Drawing.Color.White;
            this.pnlSort1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSort1.Location = new System.Drawing.Point(13, 12);
            this.pnlSort1.Name = "pnlSort1";
            this.pnlSort1.Size = new System.Drawing.Size(200, 200);
            this.pnlSort1.TabIndex = 6;
            this.pnlSort1.TabStop = false;
            // 
            // pnlSort2
            // 
            this.pnlSort2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnlSort2.BackColor = System.Drawing.Color.White;
            this.pnlSort2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSort2.Location = new System.Drawing.Point(219, 12);
            this.pnlSort2.Name = "pnlSort2";
            this.pnlSort2.Size = new System.Drawing.Size(200, 200);
            this.pnlSort2.TabIndex = 7;
            this.pnlSort2.TabStop = false;
            // 
            // tbSamples
            // 
            this.tbSamples.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tbSamples.LargeChange = 10;
            this.tbSamples.Location = new System.Drawing.Point(137, 256);
            this.tbSamples.Maximum = 1000;
            this.tbSamples.Minimum = 10;
            this.tbSamples.Name = "tbSamples";
            this.tbSamples.Size = new System.Drawing.Size(120, 45);
            this.tbSamples.TabIndex = 8;
            this.tbSamples.TickFrequency = 100;
            this.tbSamples.Value = 100;
            this.tbSamples.Scroll += new System.EventHandler(this.tbSamples_Scroll);
            // 
            // lblSamples
            // 
            this.lblSamples.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblSamples.AutoSize = true;
            this.lblSamples.Location = new System.Drawing.Point(10, 256);
            this.lblSamples.Name = "lblSamples";
            this.lblSamples.Size = new System.Drawing.Size(121, 13);
            this.lblSamples.TabIndex = 9;
            this.lblSamples.Text = "Number of samples: 100";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 288);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Sorting speed:";
            // 
            // tbSpeed
            // 
            this.tbSpeed.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tbSpeed.Location = new System.Drawing.Point(137, 288);
            this.tbSpeed.Maximum = 20;
            this.tbSpeed.Minimum = 1;
            this.tbSpeed.Name = "tbSpeed";
            this.tbSpeed.Size = new System.Drawing.Size(120, 45);
            this.tbSpeed.TabIndex = 11;
            this.tbSpeed.Value = 7;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(120, 288);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Min";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(252, 288);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Max";
            // 
            // ddTypeOfData
            // 
            this.ddTypeOfData.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.ddTypeOfData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddTypeOfData.FormattingEnabled = true;
            this.ddTypeOfData.Items.AddRange(new object[] {
            "Random",
            "Reversed",
            "Sorted",
            "Nearly Sorted",
            "Few Unique"});
            this.ddTypeOfData.Location = new System.Drawing.Point(255, 256);
            this.ddTypeOfData.Name = "ddTypeOfData";
            this.ddTypeOfData.Size = new System.Drawing.Size(164, 21);
            this.ddTypeOfData.TabIndex = 34;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 347);
            this.Controls.Add(this.cmdSort);
            this.Controls.Add(this.ddTypeOfData);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbSpeed);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblSamples);
            this.Controls.Add(this.tbSamples);
            this.Controls.Add(this.pnlSort2);
            this.Controls.Add(this.cboAlg2);
            this.Controls.Add(this.cboAlg1);
            this.Controls.Add(this.pnlSort1);
            this.MaximumSize = new System.Drawing.Size(10000, 10000);
            this.MinimumSize = new System.Drawing.Size(450, 386);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sort Comparison";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pnlSort1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSort2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSamples)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboAlg1;
        private System.Windows.Forms.ComboBox cboAlg2;
        private System.Windows.Forms.Button cmdSort;
        private System.Windows.Forms.PictureBox pnlSort1;
        private System.Windows.Forms.PictureBox pnlSort2;
        private System.Windows.Forms.TrackBar tbSamples;
        private System.Windows.Forms.Label lblSamples;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar tbSpeed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ComboBox ddTypeOfData;
    }
}

