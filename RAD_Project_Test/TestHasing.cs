namespace RAD_Project_Test
{
    public class TestHashing
    {
        private IHashing multiplyShift;
        private IHashing multiplyModPrime;

        [SetUp]
        public void Setup()
        {
            // Setup the hashing objects
            multiplyShift = new MultiplyShift();
            multiplyModPrime = new MultiplyModPrime();
        }

        [Test]
        public void TestMultiplyShift()
        {
            // Arrange
            /*
                a = 0x0afa661086a217dd = 791056905721223133
                x = 42
                l = 4
                w = 64
                floor((a*x) mod 2^w / 2^(w-l)) = 12
            */
            ulong expected = 12;
            ulong x = 42;
            int l = 4;

            // Act
            ulong result = multiplyShift.Hash(x, l);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TestMultiplyModPrime()
        {
            // Arrange
            /*
                a = 556660067608673510658973370
                x = 41
                b = 22714816827324544532436935
                p = 2^89 - 1
                l = 5
                m = 2^l
                ((a*x + b) mod p) mod m = 21
            */
            ulong expected = 21;
            ulong x = 41;
            int l = 5;

            // Act
            ulong result = multiplyModPrime.Hash(x, l);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

    }
}