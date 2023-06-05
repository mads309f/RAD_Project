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
            int test_size = 100;
            int bits = 89;
            BigInteger[] l = new BigInteger[test_size];

            for (int i = 0; i < test_size; i++)
            {
                BigInteger x = RandomBigInt.Next(bits);
                l[i] = x;
            }

            // check all values are unique
            Boolean is_correct_len = false;
            for (int i = 0; i < test_size; i++)
            {
                // check all values are "bits" bits long
                int num = (int)(l[i] >> (bits - 1));
                Assert.That(num >> 1, Is.EqualTo(0));

                if (l[i] >> (bits - 1) == 1)
                {
                    is_correct_len = true;
                }

                for (int j = i + 1; j < test_size; j++)
                {
                    Assert.That(l[i], Is.Not.EqualTo(l[j]));
                }
            }
            Assert.IsTrue(is_correct_len);
        }
    }
}
