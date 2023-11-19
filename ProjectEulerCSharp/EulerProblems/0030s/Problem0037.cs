using System;
using System.Collections.Generic;
using ProjectEulerCSharp.EulerMath;

namespace ProjectEulerCSharp.EulerProblems._0030s
{
    [Euler(
        title: "Problem 37: Truncatable primes",
        description: @"The number 3797 has an interesting property. Being prime itself, it is possible to continuously remove digits from left to right, and remain prime at each stage: 3797, 797, 97, and 7. Similarly we can work from right to left: 3797, 379, 37, and 3.

Find the sum of the only eleven primes that are both truncatable from left to right and right to left.

NOTE: 2, 3, 5, and 7 are not considered to be truncatable primes.")
    ]
    // ReSharper disable once UnusedType.Global
    public class Problem0037 : ISolution<int>
    {
        public bool HaveImplementedAnalyticSolution => false;

        public int BruteForceSolution()
        {
            var sum = 0;
            // took a few guesses and 740k was the smallest bound to get the correct answer
            var primes = Primes.PreCalculated(740_000);
            foreach (var prime in primes)
            {
                // is just figured out first few primes and noticed 23 was the first truncatable one
                if (prime < 23)
                {
                    continue;
                }
                if (IsTruncatableFromLeft(prime, primes) && IsTruncatableFromRight(prime, primes))
                {
                    sum += prime;
                }
            }

            return sum;
        }

        private static bool IsTruncatableFromLeft(int somePrime, HashSet<int> primes)
        {
            while (somePrime > 9)
            {
                somePrime = RemoveLeftmostDigit(somePrime);
                if (!primes.Contains(somePrime))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsTruncatableFromRight(int somePrime, HashSet<int> primes)
        {
            while (somePrime > 9)
            {
                somePrime = RemoveRightmostDigit(somePrime);
                if (!primes.Contains(somePrime))
                {
                    return false;
                }
            }
            return true;
        }

        private static int RemoveLeftmostDigit(int somePrime)
        {
            var len = somePrime.GetCountOfDigits();
            return somePrime % (int)Math.Pow(10, len - 1);
        }

        private static int RemoveRightmostDigit(int somePrime)
        {
            return somePrime / 10;
        }

        public int AnalyticSolution()
        {
            throw new NotImplementedException();
        }

        public int ExpectedSolution()
        {
            return 748_317;
        }
    }
}
