﻿using System;
using System.Linq;
using ProjectEulerCSharp.EulerMath;

namespace ProjectEulerCSharp.EulerProblems._0010s
{
    [Euler(
        title: "Problem 10: Summation of primes",
        description: @"The sum of the primes below 10 is 2 + 3 + 5 + 7 = 17.

Find the sum of all the primes below two million.")
    ]
    // ReSharper disable once UnusedType.Global
    public class Problem0010 : ISolution<long>
    {
        /****************************************************************************************************
         * Summation of primes
         *
         * The sum of the primes below 10 is 2 + 3 + 5 + 7 = 17.
         * Find the sum of all the primes below two million.
         ****************************************************************************************************/
        public long BruteForceSolution()
        {
            var primes = Primes.Calculate(2_000_000).Select(Convert.ToInt64);
            var sum = primes.Aggregate((acc, prime) => acc + prime);

            return sum;
        }

        public bool HaveImplementedAnalyticSolution => false;

        public long AnalyticSolution()
        {
            throw new NotImplementedException();
        }

        public long ExpectedSolution()
        {
            return 142913828922;
        }
    }
}