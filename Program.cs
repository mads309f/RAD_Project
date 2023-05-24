using System;
using System.Diagnostics;
using System.Numerics;

namespace Rad_Project {
    public class Program {
        static void Main(string[] args) {
            var stream = Utility.Stream.CreateStream(100, 10);
            test_hash_func(100, 8);
        }

        static void test_hash_func(int n, int l) {
            var stream = Utility.Stream.CreateStream(n, l);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            ulong sum_shift = 0;
            foreach (var pair in stream) {
                sum_shift += Utility.Hashing.MultiplyShift(pair.Item1, l);   
            }

            stopwatch.Stop();
            int elapsed_sec = stopwatch.Elapsed.Milliseconds;
            
            Console.WriteLine($"Sum: {sum_shift}, calculated in {elapsed_sec} ms");
            
            stopwatch.Reset();
            stopwatch.Start();
            BigInteger sum_mod = 0;
            foreach (var pair in stream) {
                sum_mod += Utility.Hashing.MultiplyModPrime(pair.Item1, l);   
            }
            stopwatch.Stop();
            elapsed_sec = stopwatch.Elapsed.Milliseconds;
            
            Console.WriteLine($"Sum: {sum_mod}, calculated in {elapsed_sec} ms");
            

        }
    }

}