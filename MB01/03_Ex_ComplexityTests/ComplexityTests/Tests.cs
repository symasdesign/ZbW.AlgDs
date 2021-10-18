using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexityTests {
    public class Tests {
        /// <summary>
        /// Utility attribute to count the number of do_something calls.
        /// </summary>
        private int cost = 0;

        /// <summary>
        /// Procedure 1.
        /// </summary>
        /// <param name="n"> the test input value. </param>
        public void Procedure1(int n) {
            for (int i = 0; i <= n; i++) {
                do_something(i, n);
            }
            for (int j = n; j >= 0; j--) {
                do_something(j, n);
            }
        }

        /// <summary>
        /// Procedure 2.
        /// </summary>
        /// <param name="n"> the test input value. </param>
        public void Procedure2(int n) {
            for (int i = 0; i < n; i++) {
                for (int j = 0; j < 2 * i; j++) {
                    do_something(i, n);
                }
            }
        }

        /// <summary>
        /// Procedure 3.
        /// </summary>
        /// <param name="n"> the test input value. </param>
        public void Procedure3(int n) {
            for (int i = 0; i < n; i++) {
                int j = 0;
                while (j < 2 * n) {
                    j++;
                    do_something(i, n);
                }
            }
        }

        /// <summary>
        /// Procedure 4.
        /// </summary>
        /// <param name="n"> the test input value. </param>
        public void Procedure4(int n) {
            int j = n;
            while (j > 0) {
                j = j / 2;
                do_something(j, n);
            }
        }

        /// <summary>
        /// ProcRec 1.
        /// To tasks "Algorithm Schemas"
        /// </summary>
        /// <param name="n"> the test input value. </param>
        public void ProcRec1(int n) {
            if (n <= 1) {
                return;
            }
            do_something(n);
            ProcRec1(n / 2);
        }

        /// <summary>
        /// ProcRec 2.
        /// To tasks "Algorithm Schemas"
        /// </summary>
        /// <param name="n"> the test input value. </param>
        /// <param name="res"> the result value. </param>
        public int ProcRec2(int n, int res) {
            res = do_something(res, n);
            if (n <= 1) {
                return res;
            }
            res = ProcRec2(n / 2, res);
            res = ProcRec2(n / 2, res);
            return res;
        }


        /// <summary>
        /// Perfom all complexity tests.
        /// </summary>
        /// <param name="n"> the input value for the tests. </param>
        public StringBuilder DoAllTests(int n) {
            var sb = new StringBuilder();

            Procedure1(n);
            sb.AppendLine($"Procedure1: {this.cost}");
            cost = 0;

            Procedure2(n);
            sb.AppendLine($"Procedure2: {this.cost}");
            cost = 0;

            Procedure3(n);
            sb.AppendLine($"Procedure3: {this.cost}");
            cost = 0;

            Procedure4(n);
            sb.AppendLine($"Procedure4: {this.cost}");
            cost = 0;

            ProcRec1(n);
            sb.AppendLine($"ProcRec1:   {this.cost}");
            cost = 0;

            ProcRec2(n, 0);
            sb.AppendLine($"ProcRec2:   {this.cost}");
            cost = 0;

            return sb;
        }


        /// <summary>
        /// Count the number of do_something calls.
        /// </summary>
        /// <param name="i"> The input parameter do nothing. </param>
        public virtual int do_something(params int[] i) {
            return cost++;
        }
    }
}