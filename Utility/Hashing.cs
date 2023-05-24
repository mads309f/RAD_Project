using System.Numerics;

// Opgave 1. Implementering af hashfunktioner
namespace Utility {
    public static class Hashing {

        // a: Multiply-shift hashing
        public static ulong MultiplyShift(ulong x, int l, ulong a = 0x0afa661086a217dd) {
            /*
                Multiply-shift hashing som er parametriseret af a og l.
                Hasfunktionen skal være: h(x) = (a * x) >> (64 − l),
                hvor a er et tilfældigt ulige 64-bit tal
            */
            return (a * x) >> (64 - l);
        }

        // b: Multiply-mod-prime hashing
        public static BigInteger MultiplyModPrime(ulong x, int l) {
            /*

                Multiply-mod-prime hashing, hvor p = 2^89 −1, og som er parametriseret
                af a, b og l, hvor a og b er heltal skarpt mindre end p og hvor l er et positivt heltal
                mindre end 64.
                Hashfunktionen skal være: h(x) = ((a*x + b) mod p) mod 2^l
                hvor a og b er uafhængige og uniformt tilfældige i [p] = {0, 1, ..., p−1}.
            */
            BigInteger a = BigInteger.Parse("556660067608673510658973370");
            BigInteger b = BigInteger.Parse("22714816827324544532436935");

            BigInteger p = BigInteger.Pow(2, 89) - 1;
            
            BigInteger y = ((a * x + b) & p) + ((a * x + b) >> 89);  // (a * x + b) mod p
            if (y >= p) y -= p;

            BigInteger r = BigInteger.Pow(2, l) - 1; // 2^l - 1
            return y & r;       
        }

    }
}