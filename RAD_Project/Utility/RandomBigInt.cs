using System;
using System.Numerics;

namespace Utility
{
    public class RandomBigInt
    {
        private static readonly Random rnd = new Random(103);

        public static BigInteger Next(int bitLength)
        {
            // divide by 8 and round up
            int byteLength = (bitLength + 7) / 8;
            byte[] bytes = new byte[byteLength + 1];
            rnd.NextBytes(bytes);
            bytes[byteLength] = 0; // force sign bit to positive
            BigInteger num = new BigInteger(bytes);
            return num >> (byteLength * 8 - bitLength);
        }

    }
}