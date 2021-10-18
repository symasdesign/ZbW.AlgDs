using System.Collections;
using System.Collections.Generic;

namespace DatenstrukturenIntro {
    // The LogReader produces synthetic data for use in experiments.
    // It simulates a log file with 100'000 log lines and 90'000 different IP adresses.
    public class LogReader : IEnumerable<LogLine> {
        private int counter;

        public IEnumerator<LogLine> GetEnumerator() {
            while (counter < 100000) {
                yield return new LogLine(counter % 90000);
                counter++;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }
    }

    public class LogLine {
        readonly int counter;
        public LogLine(int counter) {
            this.counter = counter;
        }

        // The return value is not an IP-address, but just a string.
        // The strings will be unique to the extend of the 'counter' parameter.
        public string GetIP() {
            return "ip" + counter;
        }
    }
}
