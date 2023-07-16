using ProjectEulerCSharp.EulerMath;
using System;
using System.Linq;

namespace ProjectEulerCSharp.EulerProblems._0050s
{
    [Euler(
        title: "Problem 50: Consecutive Prime Sum",
        description: @"The prime 41, can be written as the sum of six consecutive primes:
41 = 2 + 3 + 5 + 7 + 11 + 13
This is the longest sum of consecutive primes that adds to a prime below one-hundred.

The longest sum of consecutive primes below one-thousand that adds to a prime, contains 21 terms, and is equal to 953.

Which prime, below one-million, can be written as the sum of the most consecutive primes?")
    ]
    // ReSharper disable once UnusedMember.Global
    public class Problem0050 : ISolution<int>
    {
        public bool HaveImplementedAnalyticSolution => false;

        public int BruteForceSolution()
        {
            var primeSet = Primes.PreCalculated(1_000_000);
            var primes = primeSet.OrderBy(p => p).ToList();
            var primeWithMaxSumCount = 0;
            var maxSumCount = 0;
            // Note extra loop condition, if the starting number is over half of the max value then there is no point continuing
            for (var i = 0; i < primes.Count && primes[i] < 500_000; i++)
            {
                var sum = primes[i];
                var sumCount = 1;
                for (var j = i + 1; j < primes.Count; j++)
                {
                    sum += primes[j];
                    // checking that we don't go over 10^6 is the trick to drastically reducing how far we go in inner loop thus getting this alg to run quickly
                    if (sum >= 1_000_000)
                    {
                        break;
                    }
                    sumCount++;
                    if (primeSet.Contains(sum) && sumCount > maxSumCount)
                    {
                        maxSumCount = sumCount;
                        primeWithMaxSumCount = sum;
                    }
                }
            }

            return primeWithMaxSumCount;
        }


        public int AnalyticSolution()
        {
            throw new NotImplementedException();
        }

        public int ExpectedSolution()
        {
            return 997_651;
        }
    }
}
