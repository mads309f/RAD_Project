using System;
using System.Diagnostics;
using System.Numerics;

namespace Rad_Project
{
    public struct TestResults
    {
        public ulong Sum { get; set; }
        public int Elapsed_ms { get; set; }
    }

    public class Program
    {
        static readonly int TEST_TIMES = 100;
        static void Main(string[] args)
        {
            RunAvgTest(TestMultiplyShift, "multiply-shift");
            RunAvgTest(TestMultiplyModPrime, "multiply-mod-prime");
        }

        static void RunAvgTest(Func<int, int, TestResults> testFunction, string testName)
        {
            TestResults avgTestResults = new TestResults();

            for (int i = 0; i < TEST_TIMES; i++)
            {
                TestResults testResults = testFunction(1000000, 8);
                avgTestResults.Sum += testResults.Sum;
                avgTestResults.Elapsed_ms += testResults.Elapsed_ms;
            }

            avgTestResults.Elapsed_ms /= TEST_TIMES;
            Console.WriteLine($"Total sum for {testName}: {avgTestResults.Sum}");
            Console.WriteLine($"Average time for {testName}: {avgTestResults.Elapsed_ms} ms");
        }

        static TestResults TestMultiplyShift(int n, int l)
        {
            return RunTest((x, y) => Utility.Hashing.MultiplyShift(x, y), "Multiply-shift", n, l);
        }

        static TestResults TestMultiplyModPrime(int n, int l)
        {
            return RunTest((x, y) => Utility.Hashing.MultiplyModPrime(x, y), "Multiply-mod-prime", n, l);
        }

        static TestResults RunTest(Func<ulong, int, ulong> testFunction, string testName, int n, int l)
        {
            var stream = Utility.Stream.CreateStream(n, l);

            Stopwatch stopwatch = new Stopwatch();
            TestResults testResults = new TestResults();

            stopwatch.Start();

            foreach (var pair in stream)
                testResults.Sum += testFunction(pair.Item1, l);

            stopwatch.Stop();
            testResults.Elapsed_ms = (int)stopwatch.ElapsedMilliseconds;

            Console.WriteLine(testName);
            Console.WriteLine($"Sum: {testResults.Sum}, calculated in {testResults.Elapsed_ms} ms");

            return testResults;
        }
    }
}
