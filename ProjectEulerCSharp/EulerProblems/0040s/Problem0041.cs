using System.Collections.Generic;
using System.Linq;
using ProjectEulerCSharp.EulerMath;

namespace ProjectEulerCSharp.EulerProblems._0040s
{
    [Euler(
        title: "Problem 41: Pandigital Prime",
        description: @"We shall say that an n-digit number is pandigital if it makes use of all the digits 1 to n exactly once. For example, 2143 is a 4-digit pandigital and is also prime.

What is the largest n-digit pandigital prime that exists?")
    ]
    // ReSharper disable once UnusedMember.Global
    public class Problem0041 : ISolution<int>
    {
        public bool HaveImplementedAnalyticSolution => false;

        public int BruteForceSolution()
        {
            // I sort of randomly picked 7 digits because 8 and 9 digit numbers seemed too big to do a prime check in a reasonable time
            var setOfPrimes = Primes.PreCalculated(9_999_999);
            var allFoundPrimes = new List<int>();
            var permutations = Permutations.Of(Enumerable.Range(1, 7).ToArray(), PermutationAlgorithm.Heap);
            foreach (var possiblePrimeArr in permutations)
            {
                var possiblePrime = possiblePrimeArr.ToInt();
                if (setOfPrimes.Contains(possiblePrime))
                {
                    allFoundPrimes.Add(possiblePrime);
                }
            }

            var answer = allFoundPrimes.Max();
            return answer;
        }

        public int AnalyticSolution()
        {
            throw new System.NotImplementedException();
        }

        public int ExpectedSolution()
        {
            return 7_652_413;
        }
    }
}
