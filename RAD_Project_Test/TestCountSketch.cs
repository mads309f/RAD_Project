

namespace RAD_Project_Test
{
    public class TestCountSketch
    {

        private int t = 21;
        private int n = 1000000;

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        // set up a test for the hash function with different values of l

        public void TestCount()
        {
            CountSketch cs = new CountSketch(t);
            var stream = Utility.Stream.CreateStream(n, l);

            int actual = Algorithms.SquaredSum(stream, new MultiplyShift(), n, l);
        }
    }
}
