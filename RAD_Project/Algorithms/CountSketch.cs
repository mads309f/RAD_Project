using Utility;
using System.Numerics;
using System;

// Opgave 4. Implementering af 4-universel hashfunktion
public class CountSketch
{
    private const int b = 89;
    private readonly BigInteger p = (new BigInteger(1) << b) - 1;
    private const int q = 4;
    private BigInteger[] a = new BigInteger[q];

    public CountSketch()
    {
    }

    private void ChooseHashUniform()
    {
        // generate random 89 bit number
        for (int i = 0; i < q; i++)
        {
            a[i] = RandomBigInt.Next(b);
        }
    }

    private BigInteger g(ulong x)
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
        return y;
    }

    // Opgave 5. Implementering af hashfunktioner til Count-Sketch
    private (ulong, int) Hash(ulong x, int t)
    {
        /*
            Lad m = 2^t ≤ 2^64 være en toerpotens.
            Implementer en funktion der som input tager t samt en hashfunktion g : U →[p] og
            kan evaluere de to hashfunktioner h : U → [m] og s : U → {−1,1} defineret ved
            h(x) = g(x) mod m, og
            s(x) = 1 −2*floor(g(x)/2^(b−1)).
            Her er p = 2^b − 1 = 2^89 − 1, så b = 89. Med andre ord består h(x) af de log_2(m) = t
            mindst betydende bits af g(x) og s(x) er enten −1 eller 1 afhængigt af værdien af den mest betydende bit i g(x).
            Til implementeringsdetaljerne skal Algoritme 2 i noterne om second moment estimation benyttes.
        */
        BigInteger gx = g(x);
        ulong hx = (ulong)(gx & ((1UL << t) - 1UL));
        int bx = (int)(gx >> (b - 1));
        int sx = 1 - 2 * bx;

        return (hx, sx);
    }

    // Opgave 6. Implementering af Count-Sketch
    public ulong Apply(IEnumerable<Tuple<ulong, int>> stream, int t)
    {
        /*
            Count-Sketch, der er parametriseret ved hashfunktioner:
            h : U → [m] og
            s : U → {−1,1},
            hvor m = 2^t er en toerpotens.
            Samt en funktion, der givet sketch C[0,...,m−1] udregner estimatet X = ∑_{y∈[m]} C[y]^2 for S.
        */
        // Init
        ChooseHashUniform();
        ulong m = 1UL << t;
        long[] C = new long[m];

        // Process stream
        foreach (var pair in stream)
        {
            ulong x = pair.Item1;
            int d = pair.Item2;
            (ulong hx, int sx) = Hash(x, t);
            C[hx] += sx * d;
        }

        // 2nd moment estimation
        ulong F2 = 0;
        foreach (ulong c in C)
        {
            F2 += c * c;
        }
        return F2;
    }
}