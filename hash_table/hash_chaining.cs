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

namespace Hashing
{
    using System;
    using System.Collections.Generic;

    namespace Utility
    {

        public class HashTable_chaining
        {

            private int l; // Size of the hash table
            private List<KeyValuePair<ulong, int>>[] buckets; // Array of linked lists to store key-value pairs
            private Func<ulong, int, ulong> hashFunction; // Delegate for the hash function

            public HashTable_chaining(int l, Func<ulong, int, ulong> hashFunction)
            {
                this.l = l;
                this.buckets = new List<KeyValuePair<ulong, int>>[l];
                this.hashFunction = hashFunction;
            }

            public int Get(ulong x)
            {
                ulong index = CalculateIndex(x);
                if (buckets[index] != null)
                {
                    foreach (var pair in buckets[index])
                    {
                        if (pair.Key == x)
                        {
                            return pair.Value;
                        }
                    }
                }
                return 0; // Key not found, return 0
            }

            public void Set(ulong x, int value)
            {
                ulong index = CalculateIndex(x);
                if (buckets[index] == null)
                {
                    buckets[index] = new List<KeyValuePair<ulong, int>>();
                }
                // Check if key already exists in the list
                for (int i = 0; i < buckets[index].Count; i++)
                {
                    if (buckets[index][i].Key == x)
                    {
                        buckets[index][i] = new KeyValuePair<ulong, int>(x, value); // Update the value if key exists
                        return;
                    }
                }
                // Key not found, add it to the list
                buckets[index].Add(new KeyValuePair<ulong, int>(x, value));
            }

            public void Increment(ulong x, int delta)
            {
                ulong index = CalculateIndex(x);
                if (buckets[index] == null)
                {
                    buckets[index] = new List<KeyValuePair<ulong, int>>();
                    buckets[index].Add(new KeyValuePair<ulong, int>(x, delta));
                }
                else
                {
                    // Check if key already exists in the list
                    for (int i = 0; i < buckets[index].Count; i++)
                    {
                        if (buckets[index][i].Key == x)
                        {
                            int updatedValue = buckets[index][i].Value + delta; // Calculate updated value
                            buckets[index][i] = new KeyValuePair<ulong, int>(x, updatedValue); // Update the value if key exists
                            return;
                        }
                    }
                    // Key not found, add it to the list
                    buckets[index].Add(new KeyValuePair<ulong, int>(x, delta));
                }
            }

            private ulong CalculateIndex(ulong x)
            {
                return hashFunction(x, l); // Calculate the index using the provided hash function
            }
        }
    }


}
