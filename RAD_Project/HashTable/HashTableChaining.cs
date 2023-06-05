/* 
Opgave 2. Implementering af hashtabel med chaining.
We will again simplify the problem and assume that the size of the hash table is a power of two. 
The goal is to implement a hash table using chaining, which is parameterized by a 
hash function, h, and a positive integer l, where the image quantity for h is [2^l]. 

The hash table must handle the following operations.
(a) get(x): Must return the value belonging to the key x. If x is not in the table shall
0 is returned.
(b) set(x, v): Must set the key x to have the value v. If x is not already in the table
then it is added to the table with the value v.
(c) increment(x, d): Must add d to the value of x. If x is not in the table, must
x is added to the table with the value d.
 */

using System;
using System.Collections.Generic;
using Utility;

namespace Hashing
{
    public class StreamPair
    {
        public ulong x;
        public long val;
        public StreamPair(ulong x, long val)
        {
            this.x = x;
            this.val = val;
        }
    }
    public class HashTableChaining
    {
        private int l; // size of the hash table
        private List<StreamPair>[] buckets; // array of linked lists to store key-value pairs
        private IHashing hashFunction; // delegate for the hash function

        public HashTableChaining(int l, IHashing hashFunction)
        {
            this.l = l;
            ulong len = (1UL << l);
            this.buckets = new List<StreamPair>[len];
            this.hashFunction = hashFunction;

            // Initialize the array
            for (ulong i = 0; i < len; i++)
            {
                buckets[i] = new List<StreamPair>();
            }
        }

        public long Get(ulong x)
        {
            ulong index = CalculateIndex(x);
            if (buckets[index] != null)
            {
                foreach (var pair in buckets[index])
                {
                    if (pair.x == x)
                    {
                        return pair.val;
                    }
                }
            }
            return 0; // key not found, return 0
        }

        public void Set(ulong x, long value)
        {
            ulong index = CalculateIndex(x);

            // Iterate through the list using foreach instead
            foreach (var pair in buckets[index])
            {
                if (pair.x == x)
                {
                    // pair = new KeyValuePair<ulong, int>(x, value);
                    pair.val = value;
                    return;
                }
            }
            // Key not found, add it to the list
            buckets[index].Add(new StreamPair(x, value));
        }

        public void Increment(ulong x, int delta)
        {
            ulong index = CalculateIndex(x);

            // Check if key already exists in the list
            foreach (var pair in buckets[index])
            {
                if (pair.x == x)
                {
                    pair.val += delta; // calculate updated value
                    return;
                }
            }
            buckets[index].Add(new StreamPair(x, delta)); // key not found, add it to the list
        }

        private ulong CalculateIndex(ulong x)
        {
            return hashFunction.Hash(x, l); // calculate the index using the provided hash function
        }

        public IEnumerable<long> GetValues()
        {
            for (int i = 0; i < buckets.Length; i++)
            {
                foreach (var bucket in buckets[i])
                {
                    yield return bucket.val;
                }
            }
        }

    }

}
