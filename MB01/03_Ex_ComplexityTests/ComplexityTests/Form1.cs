using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComplexityTests {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            var tests = new Tests();
            var n = Convert.ToInt32(this.numericUpDown1.Value);
            var res = tests.DoAllTests(n);

            this.textBox1.Text = res.ToString();
        }
    }
}
