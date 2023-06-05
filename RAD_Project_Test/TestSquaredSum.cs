namespace RAD_Project_Test
{
    public class TestSquaredSum
    {
        IHashing multiply = new MultiplyShift();
        IHashing modPrime = new MultiplyModPrime();
        int n = 1 << 20;

        [SetUp]
        public void Setup()
        {
        }

        // set up a test for the hash function with different values of l
        [Test]
        public void TestCalculateSum()
        {
            List<Tuple<ulong, int>> stream = new List<Tuple<ulong, int>>();
            // Populate the stream with tuples
            stream.Add(new Tuple<ulong, int>(1230322, 10));
            stream.Add(new Tuple<ulong, int>(112234, 15));
            stream.Add(new Tuple<ulong, int>(1230322, 31));
            stream.Add(new Tuple<ulong, int>(923456, -5));
            stream.Add(new Tuple<ulong, int>(112234, 1));

            int l = 2;
            ulong result = SquaredSum.CalculateSquaredSum(stream, new MultiplyShift(), stream.Count, l);
            Assert.That(result, Is.EqualTo(1962));

            List<Tuple<ulong, int>> stream2 = new List<Tuple<ulong, int>>();

            int l2 = 20;
            int elements = 1 << l2;
            for (int i = 0; i < elements; i++)
            {
                stream2.Add(new Tuple<ulong, int>((ulong)i, i));
            }
            ulong result2 = SquaredSum.CalculateSquaredSum(stream2, new MultiplyShift(), stream2.Count, l2);
            ulong actualSum = 384306618446643200;
            Assert.That(result2, Is.EqualTo(actualSum));
        }

        // set up a test for the hash function with different values of l
        [TestCase(10)]
        [TestCase(15)]
        [TestCase(20)]
        [TestCase(21)]
        public void TestCalculateSumModprime(int l)
        {
            int n = 1 << 24;

            IEnumerable<Tuple<ulong, int>> stream = Utility.Stream.CreateStream(n, l);

            // get length of stream
            int streamLength = stream.Count();

            // calculate l and the actual sum
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ulong result2 = SquaredSum.CalculateSquaredSum(stream, modPrime, streamLength, l);
            stopwatch.Stop();

            Console.WriteLine($"ModPrime");
            Console.WriteLine($"L: {l} and Time elapsed: {stopwatch.ElapsedMilliseconds} ms");
            Assert.Pass();
        }

        // set up a test for the hash function with different values of l
        [TestCase(10)]
        [TestCase(15)]
        [TestCase(20)]
        [TestCase(21)]
        public void TestCalculateSumMultiply(int l)
        {
            // uint one = 1;
            int n = 1 << 24;

            IEnumerable<Tuple<ulong, int>> stream = Utility.Stream.CreateStream(n, l);

            // get length of stream
            int streamLength = stream.Count();

            // calculate l and the actual sum
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ulong result2 = SquaredSum.CalculateSquaredSum(stream, multiply, streamLength, l);
            stopwatch.Stop();

            Console.WriteLine($"MultiplyShift");
            Console.WriteLine($"L: {l} and Time elapsed: {stopwatch.ElapsedMilliseconds} ms");
            Assert.Pass();
        }

    }
}
