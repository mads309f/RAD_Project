
namespace RAD_Project_Test
{
    public class TestRandomBigInt
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        // set up a test for the hash function with different values of l

        public void TestBigInt()
        {
            int test_size = 10;
            int bits = 89;
            BigInteger[] l = new BigInteger[test_size];

            for (int i = 0; i < test_size; i++)
            {
                BigInteger x = RandomBigInt.Next(bits);
                Console.WriteLine(x);

                l[i] = x;
            }

            // check all values are unique
            Boolean is_correct_len = false;
            for (int i = 0; i < test_size; i++)
            {
                // check all values are "bits" bits long
                BigInteger num = (l[i] >> (bits - 1));
                Console.WriteLine(num);
                Assert.AreEqual(num >> 1, new BigInteger(0));

                if (l[i] >> (bits - 1) == 1)
                {
                    is_correct_len = true;
                }

                for (int j = i + 1; j < test_size; j++)
                {
                    Assert.AreNotEqual(l[i], l[j]);
                }
            }
            Assert.IsTrue(is_correct_len);
        }
    }
}
