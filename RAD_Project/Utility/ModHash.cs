using Utility;

// Opgave 5. Implementering af hashfunktioner til Count-Sketch
public class ModHash
{
    private readonly IHashing g;
    private readonly int t;
    private readonly int b = 89;
    public ModHash(IHashing g, int t)
    {
        this.g = g;
        this.t = t;
    }
    public (ulong, ulong) Hash(ulong x, int l)
    {
        /*
            Lad m = 2^t ≤ 2^64 være en toerpotens.
            Implementer en funktion der som input tager t samt en hashfunktion g : U →[p] og
            kan evaluere de to hashfunktioner h : U → [m] og s : U → {−1,1} defineret ved
            h(x) = g(x) mod m, og
            s(x) = 1 −2*floor(g(x)/2^(b−1)).
            Her er p = 2^b − 1 = 2^89 − 1, så b = 89. Med andre ord består h(x) af de log_2(m) = t
            mindst betydende bits af g(x) og s(x) er enten −1 eller 1 afhængigt af værdien af den mest
            betydende bit i g(x).
            Til implementeringsdetaljerne skal I bruge Algoritme 2 i noterne om second moment estimation.
        */
        ulong y = g.Hash(x, t);
        return (y & ((1UL << l) - 1UL), // h(x)
                y >> (b - 1) << 1); // s(x)
    }
}