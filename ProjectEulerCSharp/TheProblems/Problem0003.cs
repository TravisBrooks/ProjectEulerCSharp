using System;
using System.Linq;

namespace TheProblems
{
    [Euler(title: "Largest prime factor",
        description: @"The prime factors of 13195 are 5, 7, 13 and 29.

What is the largest prime factor of the number 600851475143 ?")]
    // ReSharper disable once UnusedMember.Global
    public class Problem0003 : ISolution<int>
    {
        /****************************************************************************************************
         * Largest prime factor
         *
         * The prime factors of 13195 are 5, 7, 13 and 29.
         * What is the largest prime factor of the number 600851475143 ?
         ****************************************************************************************************/
        public int BruteForceSolution()
        {
            var number = 600851475143L;
            var sqrt = (int)Math.Sqrt(number);

            return Primes.Calculate(sqrt)
                .Where(p => number % p == 0)
                .Max();
        }

        public bool HaveImplementedAnalyticSolution => true;

        public int AnalyticSolution()
        {
            var number = 600851475143L;
            var factors = PrimeFactor64.Factors(number);
            return (int)factors[^1].Factor;
        }

        public int ExpectedSolution()
        {
            return 6857;
        }

    }
}