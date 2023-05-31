

namespace RAD_Project_Test
{
    public class TestCountSketch
    {

        private int t = 12;
        private int n = 1 << 13;

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        // set up a test for the hash function with different values of l

        public void TestCount()
        {
            CountSketch cs = new CountSketch();
            var stream = Utility.Stream.CreateStream(n, t);

            Console.WriteLine("Running Count Sketch test");
            ulong actual = Algorithms.SquaredSum.CalculateSquaredSum(stream, new MultiplyShift(), n, t);
            Console.WriteLine($"Actual squared sum: {actual}");

            int test_count = 100;
            ulong[] estimates = new ulong[test_count];
            for (int i = 0; i < estimates.Length; i++)
            {
                Console.WriteLine($"Estimate {i + 1}");
                estimates[i] = cs.Apply(stream, t);
            }

            // calculate median of group the 9 groupts
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
            Console.WriteLine($"""

                Plot estimates in maple

                with(plots):
                points := [{String.Join(",", points)}]:
                p1:=pointplot(points, color=blue, symbol=solidcircle, symbolsize=10):
                p2:=plot({actual}, x=1..100, color=red):
                p3:=plot({expectation.ToString(CultureInfo.InvariantCulture)}, x=1..100, color=green):
                display(p1, p2, p3, view=[1..100, {estimates[0]}..{estimates[estimates.Length - 1]}], title="Count Sketch", labels=["Estimate", "Squared sum"]);
                p4:= pointplot([{String.Join(",", medianPoints)}], color=blue, symbol=solidcircle, symbolsize=10):
                p5:=plot({actual}, x=1..9, color=red):
                display(p4,p5, view=[1..9, {estimates[0]}..{estimates[estimates.Length - 1]}], title="Count Sketch", labels=["Estimate", "Squared sum"]);
                """);
        }
    }
}
