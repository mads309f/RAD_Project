using System;
using System.Diagnostics;
using System.Numerics;
using Utility;

namespace Rad_Project
{
    

    public class Program
    {
        
        static void Main(string[] args)
        {
            TestHashFunction.RunAvgTest(new MultiplyShift(), "multiply-shift");
            TestHashFunction.RunAvgTest(new MultiplyModPrime(), "multiply-mod-prime");
        }


    }
}
