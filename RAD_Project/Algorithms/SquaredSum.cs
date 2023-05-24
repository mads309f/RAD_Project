using Hashing;
using Utility;

// Opgave 3. Implementering af hashfunktioner
namespace Algorithms {
    public class SquaredSum {
        public static ulong CalculateSquaredSum(IEnumerable<Tuple<ulong, int>> stream, IHashing hashFunction, int n, int l) {
            HashTable_chaining hashTable = new HashTable_chaining(l, hashFunction);
            foreach (var pair in stream) {
                ulong x = pair.Item1;
                int delta = pair.Item2;
                hashTable.Increment(x, delta);
            }

            ulong squaredSum = 0;
            // Calculate the squared sum of the hash table

            return squaredSum;
        }
    }
}