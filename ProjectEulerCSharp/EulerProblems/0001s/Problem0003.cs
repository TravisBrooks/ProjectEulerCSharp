using System;
using System.Linq;
using ProjectEulerCSharp.EulerMath;

namespace ProjectEulerCSharp.EulerProblems._0001s
{
    [Euler(
        title: "Problem 3: Largest prime factor",
        description: @"The prime factors of 13195 are 5, 7, 13 and 29.

What is the largest prime factor of the number 600851475143 ?")
    ]
    // ReSharper disable once UnusedMember.Global
    public class Problem0003 : ISolution<int>
    {
        public int BruteForceSolution()
        {
            const long number = 600851475143L;
            var sqrt = (int)Math.Sqrt(number);

            return Primes.Calculate(sqrt)
                .Where(p => number % p == 0)
                .Max();
        }

        public bool HaveImplementedAnalyticSolution => true;

        public int AnalyticSolution()
        {
            /****************************************************************************************************
             * This solution for finding the factors isn't really "analytical", its still pretty brute force but
             * its a smarter brute force. Instead of finding all the primes in a range and seeing if they are factors
             * of the given number we just find all factors of the given number. The alg first checks if the number
             * is even, if so it whittles n down by factors of 2 until it is no longer divisible by 2. Then it does
             * the same for 3, then after that we only need to check odd numbers because we've already removed all
             * factors of 2 from n. We don't actually need to check if the given factor we're looking at is prime
             * because if it divides the whittled down value of n we know the current factor must be prime. Because
             * the factor list returned is built up incrementally we also know the last value mut be the largest
             * prime.
             ****************************************************************************************************/
            const long number = 600851475143L;
            var factors = PrimeFactor64.Factors(number);
            return (int)factors[^1].Factor;
        }

        public int ExpectedSolution()
        {
            return 6857;
        }
    }
}