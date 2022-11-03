namespace PostScriptViewer
{
    partial class ViewerForm
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
            this.Open = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Open
            // 
            this.Open.AutoSize = true;
            this.Open.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Open.Dock = System.Windows.Forms.DockStyle.Top;
            this.Open.Location = new System.Drawing.Point(0, 0);
            this.Open.Name = "Open";
            this.Open.Size = new System.Drawing.Size(656, 27);
            this.Open.TabIndex = 0;
            this.Open.Text = "Öffnen...";
            this.Open.UseVisualStyleBackColor = true;
            this.Open.Click += new System.EventHandler(this.OnClick);
            // 
            // ViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(656, 545);
            this.ControlBox = false;
            this.Controls.Add(this.Open);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "ViewerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Imperfect PostScript Viewer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Open;
    }
}

