namespace RAD_Project_Test
{
    public class TestHashTableChaining
    {
        private IHashing hashFunction;
        private HashTableChaining hashTable;

        [SetUp]
        public void Setup()
        {
            // Setup the hash function and the hash table
            hashFunction = new SimpleHashing();
            hashTable = new HashTableChaining(4, hashFunction);
        }

        [Test]
        public void Get_ExistingKey()
        {
            // Arrange
            ulong key = 42;
            long value = 123;
            hashTable.Set(key, value);

            // Act
            long result = hashTable.Get(key); // should return the set value = 123

            // Assert
            Assert.That(result, Is.EqualTo(value));
        }

        [Test]
        public void Get_NonExistingKey()
        {
            // Arrange
            ulong key = 42;

            // Act
            long result = hashTable.Get(key); // should return 0, since the key does not exist

            // Assert
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void Set_ExistingKey()
        {
            // Arrange
            ulong key = 42;
            long initialValue = 123;
            long updatedValue = 456;

            hashTable.Set(key, initialValue); // set the initial value

            // Act
            hashTable.Set(key, updatedValue); // update the value
            long result = hashTable.Get(key); // get the updated value

            // Assert
            Assert.That(result, Is.EqualTo(updatedValue)); // should return the updated value = 456
        }

        [Test]
        public void Increment_ExistingKey()
        {
            // Arrange
            ulong key = 42;
            long initialValue = 123;
            int delta = 10;

            hashTable.Set(key, initialValue); // set the initial value

            // Act
            hashTable.Increment(key, delta); // increment the value
            long result = hashTable.Get(key); // get the updated value

            // Assert
            Assert.That(result, Is.EqualTo(initialValue + delta)); // should return the updated value = 133
        }

        [Test]
        public void Increment_NonExistingKey()
        {
            // Arrange
            ulong key = 42;
            int delta = 10;

            // Act
            hashTable.Increment(key, delta); // the key does not exist, so therefore be created and set to delta
            long result = hashTable.Get(key); // get the value

            // Assert
            Assert.That(result, Is.EqualTo(delta)); // should return the value = 10
        }
    }
}
