namespace _4_Huffman {
    public class Entry {
        public Entry(int key, char? value) {
            this.Key = key;
            this.Value = value;
        }

        public int Key { get; }
        public char? Value { get; }
    }
}
