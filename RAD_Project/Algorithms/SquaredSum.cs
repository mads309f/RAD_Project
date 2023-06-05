using Hashing;
using Utility;

// Opgave 3. Implementering af hashfunktioner
namespace Algorithms
{
    public class SquaredSum
    {
        public static ulong CalculateSquaredSum(IEnumerable<Tuple<ulong, int>> stream, IHashing hashFunction, int n, int l)
        {
            /*
                Funktion der givet en strøm af par (x_1,d_1),...,(x_n,d_n) beregner kvadratsummen S = ∑_{x∈U} s(x)2.
                Hashtabellen implementeret i opgave 2 benyttes til at gemme værdierne for hvert x i strømmen.
            */
            HashTableChaining hashTable = new HashTableChaining(l, hashFunction);
            foreach (var pair in stream)
            {
                ulong x = pair.Item1;
                int delta = pair.Item2;
                hashTable.Increment(x, delta);
            }

            ulong squaredSum = 0;
            // Calculate the squared sum of the hash table
            foreach (ulong value in hashTable.GetValues())
            {
                squaredSum += value * value;
            }
            return squaredSum;
        }

    }
}