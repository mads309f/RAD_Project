using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using Utility;

namespace Algorithms
{
    public class CountSketchStatistics
    {
        private static int l = 21; // l is the size of the hash table
        private static int n = 1 << 21; // n is the size of the stream

        // Determines m = 2^t, where m is the size of the hash table in count sketch
        private static int[] all_t = { 10, 12, 15 }; // max is 30 on my computer
        // 2097216 ved 28 på 3788 ms
        private static int test_count = 100;


        public static void GetStatistics()
        {

            CountSketch cs = new CountSketch();
            var stream = Utility.Stream.CreateStream(n, l);
            ulong actual = Algorithms.SquaredSum.CalculateSquaredSum(stream, new MultiplyShift(), n, l);

            foreach (int t in all_t)
            {
                Console.WriteLine($"Number of estimates: {test_count}");
                Console.WriteLine("Running Count Sketch test");
                ulong[] estimates = new ulong[test_count];
                for (int i = 0; i < estimates.Length; i++)
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    estimates[i] = cs.Apply(stream, t);
                    stopwatch.Stop();
                    Console.WriteLine($"Estimate {i + 1} calculated to {estimates[i]} in {stopwatch.ElapsedMilliseconds} ms");
                }

                // calculate median of group the 9 groups
                ulong[] medians = new ulong[9];
                for (int i = 0; i < medians.Length; i++)
                {
                    ulong[] group = new ulong[11];
                    for (int j = 0; j < group.Length; j++)
                    {
                        group[j] = estimates[i * 11 + j];
                    }
                    Array.Sort(group);
                    medians[i] = group[5];
                }

                // sort estimates
                Array.Sort(estimates);
                Array.Sort(medians);

                // calculate expectation
                double expectation = 0;
                for (int i = 0; i < estimates.Length; i++)
                {
                    expectation += estimates[i];
                }
                expectation /= estimates.Length;

                // calculate variance
                double mse = 0;
                for (int i = 0; i < estimates.Length; i++)
                {
                    ulong diff = estimates[i] - actual;
                    mse += diff * diff;
                }
                mse /= estimates.Length;
                double theoretical_variance = 2 * Math.Pow(actual, 2);
                theoretical_variance /= (1UL << t);

                // print statistics
                Console.WriteLine($"Expectation: {expectation}, \tExpected: {actual}");
                Console.WriteLine($"MSE: {mse.ToString(CultureInfo.InvariantCulture)}, \tExpected: {theoretical_variance.ToString(CultureInfo.InvariantCulture)}");

                // format points for maple
                string[] points = new string[estimates.Length];
                string[] medianPoints = new string[medians.Length];
                for (int i = 0; i < estimates.Length; i++)
                {
                    points[i] = $"({i + 1},{estimates[i]})";
                }
                for (int i = 0; i < medians.Length; i++)
                {
                    medianPoints[i] = $"({i + 1},{medians[i]})";
                }
                // print maple code:
                Console.WriteLine($"""

                    Plot estimates in maple:

                    with(plots):
                    points := [{String.Join(",", points)}]:
                    p1:=pointplot(points, color=blue, symbol=solidcircle, symbolsize=10):
                    p2:=plot({actual}, x=1..100, color=red):
                    p3:=plot({expectation.ToString(CultureInfo.InvariantCulture)}, x=1..100, color=green):
                    display(p1, p2, p3, view=[1..100, {estimates[0]}..{estimates[estimates.Length - 1]}], title="Count Sketch", labels=["Estimate", "Squared sum"]);
                    p4:= pointplot([{String.Join(",", medianPoints)}], color=blue, symbol=solidcircle, symbolsize=10):
                    p5:=plot({actual}, x=1..9, color=red):
                    display(p4,p5, view=[1..9, {medians[0]}..{medians[medians.Length - 1]}], title="Count Sketch", labels=["Estimate", "Squared sum"]);
                    """);
            }
        }

    }

}
