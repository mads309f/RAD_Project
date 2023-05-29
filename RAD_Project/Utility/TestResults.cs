using System.Diagnostics;

// Opgave 1.c. Test af hashfunktioner
namespace Utility
{
    public class TestHashFunction
    {
        public struct TestResults
        {
            public ulong Sum { get; set; }
            public int Elapsed_ms { get; set; }
        }

        static readonly int TEST_TIMES = 100;
        static readonly int STREAM_SIZE = 1000000;
        static readonly int L = 8;

        static public void RunAvgTests()
        {
            TestHashFunction.RunAvgTest(new MultiplyShift(), "multiply-shift");
            TestHashFunction.RunAvgTest(new MultiplyModPrime(), "multiply-mod-prime");
        }

        static public void RunAvgTest(IHashing hashFunction, string testName)
        {
            TestResults avgTestResults = new TestResults();

            for (int i = 0; i < TEST_TIMES; i++)
            {
                TestResults testResults = RunTest(hashFunction, testName);
                avgTestResults.Sum += testResults.Sum;
                avgTestResults.Elapsed_ms += testResults.Elapsed_ms;
            }

            avgTestResults.Elapsed_ms /= TEST_TIMES;
            Console.WriteLine($"Total sum for {testName}: {avgTestResults.Sum}");
            Console.WriteLine($"Average time for {testName}: {avgTestResults.Elapsed_ms} ms");
        }

        static private TestResults RunTest(IHashing hashFunction, string testName)
        {
            var stream = Utility.Stream.CreateStream(STREAM_SIZE, L);

            Stopwatch stopwatch = new Stopwatch();
            TestResults testResults = new TestResults();

            stopwatch.Start();

            foreach (var pair in stream)
                testResults.Sum += hashFunction.Hash(pair.Item1, L);

            stopwatch.Stop();
            testResults.Elapsed_ms = (int)stopwatch.ElapsedMilliseconds;

            Console.WriteLine(testName);
            Console.WriteLine($"Sum: {testResults.Sum}, calculated in {testResults.Elapsed_ms} ms");

            return testResults;
        }
    }
}