using System;
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

        public int BruteForceSolution()
        {
            // found the upperBounds after solving it with a larger upper bound
            const int upperBounds = 135_000;
            const int factorCount = 4;
            // 2 * 3 * 5 * 7 = 210, so thats the smallest number that can have 4 prime factors
            var n = 210;
            var primes = Primes.PreCalculated(upperBounds);
            // The strategy here is to try and skip ahead as much as possible to examine the next 4 numbers
            while (n <= upperBounds - 3)
            {
                var indexA = n + 3;
                if(primes.Contains(indexA) || PrimeFactor32.Factors(indexA).Count != factorCount)
                {
                    n += 4;
                    continue;
                }
                var indexB = n + 2;
                if (primes.Contains(indexB) || PrimeFactor32.Factors(indexB).Count != factorCount)
                {
                    n += 3;
                    continue;
                }
                var indexC = n + 1;
                if (primes.Contains(indexC) || PrimeFactor32.Factors(indexC).Count != factorCount)
                {
                    n += 2;
                    continue;
                }
                if (primes.Contains(n) || PrimeFactor32.Factors(n).Count != factorCount)
                {
                    n++;
                    continue;
                }

                return n;
            }
            // if we got to here something bad happened
            return -1;
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
