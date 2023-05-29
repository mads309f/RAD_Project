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

            BigInteger r = BigInteger.Pow(2, l) - 1; // 2^l - 1
            y = y & r;

            return (ulong)y;
        }
    }
}