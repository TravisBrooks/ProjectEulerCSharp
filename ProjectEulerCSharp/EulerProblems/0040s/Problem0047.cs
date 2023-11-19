using System;
using System.Collections.Generic;
using System.Linq;
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
    // ReSharper disable once UnusedType.Global
    public class Problem0047 : ISolution<int>
    {
        public bool HaveImplementedAnalyticSolution => false;

        public int BruteForceSolution()
        {
            var primes = Primes.Calculate(700).ToArray();
            // 2 * 3 * 5 * 7 = 210, so thats the smallest number that can have 4 prime factors
            var n = 210;
            var numbersWith4Factors = new List<int>();
            while (true)
            {
                var factorCount = 0;
                foreach (var p in primes)
                {
                    // I discovered that trying to check and break on conditions like if the prime is greater than n it was actually *slower*. Seems dotnet doesn't like jumping out of a loop.
                    if (n % p == 0)
                    {
                        factorCount++;
                    }
                }

                if (factorCount == 4)
                {
                    numbersWith4Factors.Add(n);
                }
                else
                {
                    numbersWith4Factors.Clear();
                }

                if (numbersWith4Factors.Count == 4)
                {
                    return numbersWith4Factors[0];
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
