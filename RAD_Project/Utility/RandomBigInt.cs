using System;
using System.Numerics;

public class RandomBigInt
{
    private readonly int _bitLength;
    private readonly Random _rnd;

    public RandomBigInt(int bitLength)
    {
        _bitLength = bitLength;
        _rnd = new Random();
    }

    public BigInteger Next()
    {
        // divide by 8 and round up
        byte[] bytes = new byte[(_bitLength + 8 - 1) / 8];
        _rnd.NextBytes(bytes);
        BigInteger num = new BigInteger(bytes);
        return num >> (_bitLength - 1);
    }

}