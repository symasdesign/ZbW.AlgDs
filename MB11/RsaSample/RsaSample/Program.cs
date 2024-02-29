using System.Numerics;
using System.Text;

namespace RsaSample {
    class Program {
        static void Main(string[] args) {
            //var p = BigInteger.Parse("229");
            //var q = BigInteger.Parse("389");
            //var k = 43;

            //var phiOfN = (p - 1) * (q - 1);
            //var d = ModInverse(k, phiOfN);

            //var a = new BigInteger(666);
            //var m = BigInteger.Multiply(p, q);
            //var b = Encrypt(a, m, k);

            //var a2 = Decrypt(b, d, m);

            BigPrimeExample();

            var p = BigInteger.Parse("1532495540865888858358347027150309183618739357528837633");
            var q = BigInteger.Parse("1532495540865888858358347027150309183618974467948366513");
            var k = 43;

            var phiOfN = (p - 1) * (q - 1);
            var d = ModInverse(k, phiOfN);

            var a = new BigInteger(1514297088);
            var m = BigInteger.Multiply(p, q);
            var b = Encrypt(a, m, k);

            var a2 = Decrypt(b, d, m);
        }


        public static void WikipediaExample() {
            var p = new BigInteger(61);
            var q = new BigInteger(53);
            var e = 17;

            var n = p * q;
            var phiOfN = (p - 1) * (q - 1);
            var d = ModInverse(e, phiOfN);

            var m = 65;
            var c = Encrypt(m, n, e);
            var m_ = Decrypt(c, d, n);
        }

        public static void BigPrimeExample() {
            var p = GenerateLargePrime(1200);
            var q = GenerateLargePrime(1200);
            var e = 65537;

            var n = p * q;
            var phiOfN = (p - 1) * (q - 1);
            var d = ModInverse(e, phiOfN);

            //var m = 65;
            var m = "Mathematic ist cool!";
            var c = EncryptText(m, n, e);
            var m_ = DecryptText(c, d, n);
        }

        private static BigInteger Encrypt(BigInteger m, BigInteger n, BigInteger e) {
            return BigInteger.ModPow(m, e, n);
        }

        private static BigInteger Decrypt(BigInteger mEnc, BigInteger d, BigInteger n) {
            return BigInteger.ModPow(mEnc, d, n);
        }
        public static BigInteger EncryptText(string text, BigInteger n, BigInteger e) {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            BigInteger m = new BigInteger(bytes);
            return Encrypt(m, n, e);
        }

        public static string DecryptText(BigInteger encryptedText, BigInteger d, BigInteger n) {
            BigInteger decrypted = Decrypt(encryptedText, d, n);
            byte[] bytes = decrypted.ToByteArray();
            // BigInteger.ToByteArray() gibt ein Array zurück, das möglicherweise ein zusätzliches Null-Byte am Ende enthält (wg. Zweierkomplementdarstellung)
            // Dieses zusätzliche Byte muss entfernt werden, wenn es nicht zum ursprünglichen Text gehört.
            // Wir entfernen einfach ein allfälliges Null-Byte am Ende des Arrays.
            if (bytes.Length > 1 && bytes[^1] == 0) {
                Array.Resize(ref bytes, bytes.Length - 1);
            }
            return Encoding.UTF8.GetString(bytes);
        }


        /// <summary>
        /// Calculates the modular multiplicative inverse of <paramref name="e"/> modulo <paramref name="m"/>
        /// using the extended Euclidean algorithm.
        /// </summary>
        /// <remarks>
        /// This implementation comes from the pseudocode defining the inverse(a, n) function at
        /// https://en.wikipedia.org/wiki/Extended_Euclidean_algorithm
        /// </remarks>
        public static BigInteger ModInverse(BigInteger e, BigInteger phiOfN) {
            // es gilt: e*d+k*phiOfN=1=ggT(e, phiOfN)
            // Resultat ist d und k, wobei d das Modulare Inverse ist und k nicht weiter benötigt wird


            if (phiOfN < 0) {
                phiOfN = -phiOfN;
            }

            if (e < 0) {
                e = phiOfN - (-e % phiOfN);
            }

            BigInteger t = 0, nt = 1, r = phiOfN, nr = e;

            // Der Algorithmus verwendet eine Reihe von Divisionen mit Rest,
            // um schrittweise xx und yy zu finden, wobei er die Gleichung umformt, bis der Rest 0 erreicht.
            while (nr != 0) {
                var quot = r / nr;

                var tmp = nt; nt = t - quot * nt; t = tmp;
                tmp = nr; nr = r - quot * nr; r = tmp;
            }

            if (r > 1) throw new ArgumentException(nameof(e) + " is not convertible.");
            if (t < 0) t = t + phiOfN;
            return t;
        }

        private static byte[] CopyAndReverse(byte[] data) {
            byte[] reversed = new byte[data.Length];
            Array.Copy(data, 0, reversed, 0, data.Length);
            Array.Reverse(reversed);
            return reversed;
        }
        private static BigInteger GenerateLargePrime(int digits) {
            // Einfache Variante: Generierung einer zufälligen großen Zahl und Anwendung eines einfachen Primzahltests (Miller-Rabin)
            Random random = new Random();
            BigInteger prime;

            do {
                // Erzeugen einer zufälligen großen Zahl mit ungefähr der gewünschten Stellenanzahl
                byte[] bytes = new byte[digits / 8 + 1];
                random.NextBytes(bytes);
                bytes[bytes.Length - 1] &= (byte)0x7F; // Sicherstellen, dass das höchste Bit gesetzt ist, um die Länge zu bewahren
                bytes[0] |= 1; // Sicherstellen, dass das niedrigste Bit gesetzt ist, um ungerade Zahlen zu erzeugen
                prime = new BigInteger(bytes);

                // Anwendung eines einfachen Primzahltests
            } while (!BigIntegerExtensions.IsProbablePrime(prime, 10));

            return prime;
        }
    }
}
