namespace Paint
{
    partial class PaintForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // PaintForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(782, 453);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "PaintForm";
            this.ShowIcon = false;
            this.Text = "Imperfect Paint";
            this.Load += new System.EventHandler(this.OnLoad);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnMouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}

