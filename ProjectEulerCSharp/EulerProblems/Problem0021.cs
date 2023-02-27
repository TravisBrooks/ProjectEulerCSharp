using System;
using System.Collections.Generic;
using System.Linq;
using ProjectEulerCSharp.EulerMath;

namespace ProjectEulerCSharp.EulerProblems
{
    [Euler(
title: "Problem 21: Amicable numbers",
description: @"Let d(n) be defined as the sum of proper divisors of n (numbers less than n which divide evenly into n). If d(a) = b and d(b) = a, where a ≠ b, then a and b are an amicable pair and each of a and b are called amicable numbers.

For example, the proper divisors of 220 are 1, 2, 4, 5, 10, 11, 20, 22, 44, 55 and 110; therefore d(220) = 284. The proper divisors of 284 are 1, 2, 4, 71 and 142; so d(284) = 220.

Evaluate the sum of all the amicable numbers under 10000."
        )]
    // ReSharper disable once UnusedMember.Global
    public class Problem0021 : ISolution<int>
    {
        public bool HaveImplementedAnalyticSolution => false;

        public int BruteForceSolution()
        {
            var nToDivisorSum = new Dictionary<int, int>();
            // first build lookup table of all the divisor sums in the range
            for (var i = 2; i < 10000; i++)
            {
                nToDivisorSum[i] = ProperDivisors.Of(i).Sum();
            }
            // now find all amicable numbers
            var amicableNumbers = new HashSet<int>();
            foreach (var kvp in nToDivisorSum)
            {
                var (number, sum) = kvp;
                if (number != sum && nToDivisorSum.ContainsKey(sum) && nToDivisorSum[sum] == number)
                {
                    // Could just add number here to a running total, but surprisingly this is no faster
                    // than the HashSet Sum, which seems like a more obvious solution that matches how the
                    // problem is stated in english.
                    amicableNumbers.Add(number);
                    amicableNumbers.Add(sum);
                }
            }

            return amicableNumbers.Sum();
        }

        public int AnalyticSolution()
        {
            throw new NotImplementedException();
        }

        public int ExpectedSolution()
        {
            return 31626;
        }
    }
}
