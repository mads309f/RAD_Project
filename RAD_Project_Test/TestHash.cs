using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace RAD_Project_Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

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

            Assert.AreEqual(1962, result);

            // Call CalculateSum() method or perform assertions
            // based on your testing requirements

            List<Tuple<ulong, int>> stream2 = new List<Tuple<ulong, int>>();

            // // Populate the stream with tuples, use a for loop to add 1000 tuples
            int l2 = 20;
            int elements = 1 << l2;
            for (int i = 0; i < elements; i++)
            {
                stream2.Add(new Tuple<ulong, int>((ulong)i, i));
            }

            // calculate l and the actual sum

            ulong result2 = SquaredSum.CalculateSquaredSum(stream2, new MultiplyShift(), stream2.Count, l2);
            // ulong actualSum = 384307717958270976;

            ulong actualSum = 384306618446643200;

            Assert.AreEqual(actualSum, result2);


            // Call CalculateSquaredSum() method or perform assertions
            // based on your testing requirements
        }
    }
}
