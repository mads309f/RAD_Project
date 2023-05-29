using System.Numerics;

// Opgave 1. Implementering af hashfunktioner
namespace Utility
{

    public interface IHashing
    {
        ulong Hash(ulong x, int l);
    }

    public class MultiplyShift : IHashing
    {
        private const ulong a = 0x0afa661086a217dd;
        // a: Multiply-shift hashing
        public ulong Hash(ulong x, int l)
        {
            /*
                Multiply-shift hashing som er parametriseret af a og l.
                Hasfunktionen skal være: h(x) = (a * x) >> (64 − l),
                hvor a er et tilfældigt ulige 64-bit tal
            */
            return (a * x) >> (64 - l);
        }
    }

    public class MultiplyModPrime : IHashing
    {
        private readonly BigInteger a = BigInteger.Parse("556660067608673510658973370");
        private readonly BigInteger b = BigInteger.Parse("22714816827324544532436935");
        private readonly BigInteger p = BigInteger.Pow(2, 89) - 1;

        // b: Multiply-mod-prime hashing
        public ulong Hash(ulong x, int l)
        {
            /*
                Multiply-mod-prime hashing, hvor p = 2^89 −1, og som er parametriseret
                af a, b og l, hvor a og b er heltal skarpt mindre end p og hvor l er et positivt heltal
                mindre end 64.
                Hashfunktionen skal være: h(x) = ((a*x + b) mod p) mod 2^l
                hvor a og b er uafhængige og uniformt tilfældige i [p] = {0, 1, ..., p−1}.x
            */

            BigInteger y = ((a * x + b) & p) + ((a * x + b) >> 89);  // (a * x + b) mod p
            if (y >= p) y -= p;

            ulong r = (1UL << l) - 1UL; // 2^l - 1
            y = y & r;

            return (ulong)y;
        }
    }

    // Opgave 4. Implementering af 4-universel hashfunktion
    public class MultiplyModPrimeMersennePrimes : IHashing
    {
        private readonly List<BigInteger> a = new List<BigInteger>();
        private readonly int q = 4;
        private readonly int b = 89;

        public MultiplyModPrimeMersennePrimes()
        {
            a.Add(BigInteger.Parse("522016596352186136429421401"));
            a.Add(BigInteger.Parse("381620565771064524891220591"));
            a.Add(BigInteger.Parse("98929617524125431387652801"));
            a.Add(BigInteger.Parse("260232405640153757640670000"));
        }

        public ulong Hash(ulong x, int p)
        {
            /*
                Hashfunktionen g : U → [p], parametriseret af a_0, a_1, a_2, a_3, og defineret som 
                g(x) = a_0 + a_1*x + a_2*x_2 + a_3*x_3 mod p.
                Her er p = 2^89 −1, og a0, a1,a2 og a3 uafhængige og uniformt tilfældige i [p] = {0,...,p−1}.
            */
            BigInteger y = a[q - 1];

            for (int i = q - 2; i >= 0; i--)
            {
                y = y * x + a[i];
                y = (y & p) + (y >> b);
            }
            if (y >= p) y -= p;
            return (ulong)y;
        }
    }
}