using NUnit.Framework;
using Hashing;

namespace Hashing.Tests
{
    [TestFixture]
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
        public void Get_ExistingKey_ReturnsValue()
        {
            // Arrange
            ulong key = 42;
            long value = 123;
            hashTable.Set(key, value);

            // Act
            long result = hashTable.Get(key);

            // Assert
            Assert.AreEqual(value, result);
        }

        [Test]
        public void Get_NonExistingKey_ReturnsZero()
        {
            // Arrange
            ulong key = 42;

            // Act
            long result = hashTable.Get(key);

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void Set_NewKey_AddsKeyValue()
        {
            // Arrange
            ulong key = 42;
            long value = 123;

            // Act
            hashTable.Set(key, value);
            long result = hashTable.Get(key);

            // Assert
            Assert.AreEqual(value, result);
        }

        [Test]
        public void Set_ExistingKey_UpdatesValue()
        {
            // Arrange
            ulong key = 42;
            long initialValue = 123;
            long updatedValue = 456;

            hashTable.Set(key, initialValue);

            // Act
            hashTable.Set(key, updatedValue);
            long result = hashTable.Get(key);

            // Assert
            Assert.AreEqual(updatedValue, result);
        }

        [Test]
        public void Increment_ExistingKey_IncrementsValue()
        {
            // Arrange
            ulong key = 42;
            long initialValue = 123;
            int delta = 10;

            hashTable.Set(key, initialValue);

            // Act
            hashTable.Increment(key, delta);
            long result = hashTable.Get(key);

            // Assert
            Assert.AreEqual(initialValue + delta, result);
        }

        [Test]
        public void Increment_NonExistingKey_AddsKeyValue()
        {
            // Arrange
            ulong key = 42;
            int delta = 10;

            // Act
            hashTable.Increment(key, delta);
            long result = hashTable.Get(key);

            // Assert
            Assert.AreEqual(delta, result);
        }
    }
}
