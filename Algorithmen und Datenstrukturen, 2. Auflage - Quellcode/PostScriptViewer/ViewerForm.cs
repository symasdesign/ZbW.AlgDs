using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Windows;

namespace PostScriptViewer
{
    public partial class ViewerForm : Form
    {
        Viewer viewer;

        public ViewerForm()
        {
            InitializeComponent();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            if (viewer != null)
                viewer.Paint(e.Graphics);
        }

        private void OnClick(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog()
            {
                Filter = "PostScript (*.ps)|*.ps|Alle Dateien (*.*)|*.*",
                RestoreDirectory = false
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                viewer = new Viewer(dlg.FileName);
            }

            Invalidate();
        }
    }
}
