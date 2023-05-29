/* 
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
        public int val;
        public StreamPair(ulong x, int val)
        {
            this.x = x;
            this.val = val;
        }
    }
    public class HashTable_chaining
    {

        private int l; // Size of the hash table
        private List<StreamPair>[] buckets; // Array of linked lists to store key-value pairs
        private IHashing hashFunction; // Delegate for the hash function

        public HashTable_chaining(int l, IHashing hashFunction)
        {
            this.l = l;
            this.buckets = new List<StreamPair>[(1UL << l)];
            this.hashFunction = hashFunction;

            // initialize the array
            for (int i = 0; i < l; i++)
            {
                buckets[i] = new List<StreamPair>();
            }
        }

        public int Get(ulong x)
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
            return 0; // Key not found, return 0
        }

        public void Set(ulong x, int value)
        {
            ulong index = CalculateIndex(x);

            // iterate through the list using foreach instead
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
                    pair.val += delta; // Calculate updated value
                    return;
                }
            }
            // Key not found, add it to the list
            buckets[index].Add(new StreamPair(x, delta));
        }

        private ulong CalculateIndex(ulong x)
        {
            return hashFunction.Hash(x); // Calculate the index using the provided hash function
        }

        public IEnumerable<int> GetValues()
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
