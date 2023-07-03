using System;
using System.Collections.Generic;
using ProjectEulerCSharp.EulerMath;

namespace ProjectEulerCSharp.EulerProblems._0040s
{
    [Euler(
        title: "Problem 47: Distinct Primes Factors",
        description: @"The first two consecutive numbers to have two distinct prime factors are:
            14 = 2 * 7
            15 = 3 * 5

The first three consecutive numbers to have three distinct prime factors are:
            644 = 2^2 * 7 * 23
            645 = 3 * 5 * 43
            646 = 2 * 17 * 19

Find the first four consecutive integers to have four distinct prime factors each. What is the first of these numbers?")
    ]
    // ReSharper disable once UnusedMember.Global
    public class Problem0047 : ISolution<int>
    {
        public bool HaveImplementedAnalyticSolution => false;

        // this takes ~4.5 seconds, which seems a bit too slow
        public int BruteForceSolution()
        {
            var fourPrimeHistory = new List<int>();
            var n = 647;
            while (true)
            {
                if (fourPrimeHistory.Count == 4)
                {
                    return fourPrimeHistory[0];
                }

                var factors = PrimeFactor32.Factors(n);
                if (factors.Count == 4)
                {
                    fourPrimeHistory.Add(n);
                }
                else
                {
                    fourPrimeHistory.Clear();
                }

                n++;
            }
        }

        public int AnalyticSolution()
        {
            throw new NotImplementedException();
        }

        public int ExpectedSolution()
        {
            return 134_043;
        }
    }
}
