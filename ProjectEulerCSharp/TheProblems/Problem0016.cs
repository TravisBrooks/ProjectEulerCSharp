using System;
using System.Numerics;

namespace TheProblems
{
    [Euler(
title: "Power digit sum",
description: @"2^15 = 32768 and the sum of its digits is 3 + 2 + 7 + 6 + 8 = 26.

What is the sum of the digits of the number 2^1000?"
)]
    // ReSharper disable once UnusedMember.Global
    public class Problem0016 : ISolution<int>
    {
        public bool HaveImplementedAnalyticSolution => false;

        public int BruteForceSolution()
        {
            // Because we're dealing with powers of 2 its very easy to represent this number in binary: 1 "1" and 1000 "0s", a total of 1001 bits.
            // I'll lean on the BigInteger class to go from base10 to base2 so the binary will be little endian.
            var numberOfBytesNeeded = (int)Math.Ceiling(1001.0 / 8.0);
            var byteArr = new byte[numberOfBytesNeeded];
            byteArr[numberOfBytesNeeded - 1] = 1;
            var bigInt = new BigInteger(byteArr);
            int sum = 0;
            foreach (var intChar in bigInt.ToString())
            {
                var i = int.Parse(intChar.ToString());
                sum += i;
            }

            return sum;
        }

        public int AnalyticSolution()
        {
            throw new NotImplementedException();
        }

        public int ExpectedSolution()
        {
            return 1366;
        }
    }
}
