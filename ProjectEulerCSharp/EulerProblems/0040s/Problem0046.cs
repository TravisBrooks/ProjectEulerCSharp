using System;
using System.Collections.Immutable;
using System.Linq;
using ProjectEulerCSharp.EulerMath;

namespace ProjectEulerCSharp.EulerProblems._0040s
{
    [Euler(
        title: "Problem 46: Goldbach’s Other Conjecture",
        description: @"It was proposed by Christian Goldbach that every odd composite number can be written as the sum of a prime and twice a square.

         9 = 7 + 2 * 1^2
        15 = 7 + 2 * 2^2
        21 = 3 + 2 * 3^2
        25 = 7 + 2 * 3^2
        27 = 19 + 2 * 2^2
        33 = 31 + 2 * 1^2
 
It turns out that the conjecture was false.

What is the smallest odd composite that cannot be written as the sum of a prime and twice a square?")
    ]
    // ReSharper disable once UnusedMember.Global
    public class Problem0046 : ISolution<int>
    {
        public bool HaveImplementedAnalyticSolution => false;

        public int BruteForceSolution()
        {
            var primes = Primes.PreCalculated(upperBounds: 6000);
            var sortedPrimes = primes.OrderBy(i => i).ToImmutableList();
            var n = 35;
            while (true)
            {
                // an easy to miss part of the conjecture, its not over odd numbers its over odd composite numbers (ie not prime)
                if (!primes.Contains(n))
                {
                    var foundIt = false;
                    foreach (var prime in sortedPrimes)
                    {
                        if (prime >= n)
                        {
                            break;
                        }
                        if (!foundIt)
                        {
                            var x = n - prime;
                            if (x % 2 != 0)
                            {
                                continue;
                            }

                            var halfsies = x / 2;
                            var sqrtOfHalfsies = (int)Math.Round(Math.Sqrt(halfsies));
                            var closeToN = prime + 2 * (sqrtOfHalfsies * sqrtOfHalfsies);
                            if (closeToN == n)
                            {
                                foundIt = true;
                            }
                        }
                    }

                    if (!foundIt)
                    {
                        return n;
                    }
                }

                n += 2;
            }
        }

        public int AnalyticSolution()
        {
            throw new NotImplementedException();
        }

        public int ExpectedSolution()
        {
            return 5777;
        }
    }
}
