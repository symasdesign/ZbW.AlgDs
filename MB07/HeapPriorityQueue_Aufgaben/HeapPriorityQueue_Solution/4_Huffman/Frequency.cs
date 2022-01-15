using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_Huffman {
    public class Frequency {
        public List<Entry> CountFrequency(string text) {
            var alfaCounts = new int[128];   // all ASCII-Characters
            foreach (var c in text) {
                alfaCounts[c]++;
            }

            var list = new List<Entry>();
            for(var i = 0; i<alfaCounts.Length; i++) {
                if (alfaCounts[i] > 0) {
                    list.Add(new Entry(alfaCounts[i], (char)i));
                }
            }

            return list;
        }
    }
}
