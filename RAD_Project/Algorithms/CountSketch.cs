using Utility;


// Opgave 4. Implementering af 4-universel hashfunktion
public class CountSketch
{
    private readonly IHashing g;

    private readonly int b = 89;
    private readonly int p;
    private readonly BigInteger[] a = {
        BigInteger.Parse("522016596352186136429421401"),
        BigInteger.Parse("381620565771064524891220591"),
        BigInteger.Parse("98929617524125431387652801"),
        BigInteger.Parse("260232405640153757640670000")
    };
    private readonly int q = 4;
    private readonly int b = 89;
    public CountSketch(IHashing g)
    {
        this.g = g;
        this.t = t;
    }
    public CountSketch() : this(new MultiplyModPrimeMersennePrimes()) { }

    // Opgave 5. Implementering af hashfunktioner til Count-Sketch
    public (ulong, int) Hash(ulong x, int l)
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
            Til implementeringsdetaljerne skal Algoritme 2 i noterne om second moment estimation benyttes.
        */
        ulong gx = g.Hash(x, t);
        ulong hx = gx & ((1UL << l) - 1UL);
        int bx = (int)(gx >> (b - 1));
        int sx = 1 - 2 * bx;

        return (hx, sx);
    }

    // Opgave 6. Implementering af Count-Sketch
    private ulong BCS_Init(ulong e)
    {

        return 1;
    }
    private ulong BCS_Process(ulong x, ulong d)
    {
        return 1;
    }

    private ulong BCS_2nd_Moment()
    {
        return 1;
    }
    public ulong Apply(int t)
    {
        /*
            Count-Sketch, der er parametriseret ved hashfunktioner h : U → [m] og s : U → {−1,1}, hvor m = 2^t er en toerpotens.
            Samt en funktion, der givet sketch C[0,...,m−1] udregner estimatet X = ∑_{y∈[m]} C[y]^2 for S.
        */
        ulong m = 1UL << t;

        return 1;
    }
}