using System;
using System.Numerics;


namespace Utility {
    public class RandomBigInt
    {
        private static readonly Random rnd = new Random();

        public static BigInteger Next(int bitLength)
        {
            // divide by 8 and round up
            int byteLength = (bitLength + 7) / 8;
            byte[] bytes = new byte[byteLength];
            rnd.NextBytes(bytes);
            BigInteger num = new BigInteger(bytes);
            return num >> (byteLength - bitLength);
        }

    }
}