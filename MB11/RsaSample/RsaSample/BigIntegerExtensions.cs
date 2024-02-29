using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RsaSample {
    public static class BigIntegerExtensions {
        public static bool IsProbablePrime(this BigInteger source, int certainty) {
            if (source == 2 || source == 3)
                return true;
            if (source < 2 || source % 2 == 0)
                return false;

            BigInteger d = source - 1;
            int s = 0;

            while (d % 2 == 0) {
                d /= 2;
                s += 1;
            }

            Random rand = new Random();
            for (int i = 0; i < certainty; i++) {
                BigInteger a = RandomBigIntegerBelow(source - 2) + 2;
                BigInteger x = BigInteger.ModPow(a, d, source);
                if (x == 1 || x == source - 1)
                    continue;

                for (int r = 1; r < s; r++) {
                    x = BigInteger.ModPow(x, 2, source);
                    if (x == 1)
                        return false;
                    if (x == source - 1)
                        break;
                }

                if (x != source - 1)
                    return false;
            }

            return true;
        }

        private static BigInteger RandomBigIntegerBelow(BigInteger n) {
            Random rand = new Random();
            byte[] bytes = n.ToByteArray();
            BigInteger r;

            do {
                rand.NextBytes(bytes);
                bytes[bytes.Length - 1] &= (byte)0x7F; // Sicherstellen, dass das höchste Bit gesetzt ist
                r = new BigInteger(bytes);
            } while (r >= n || r <= 0);

            return r;
        }
    }

}
