using System;
using ProjectEulerCSharp.EulerMath;

namespace ProjectEulerCSharp.EulerProblems._0030s
{
    [Euler(
        title: "Problem 34: Digit factorials",
        description: @"145 is a curious number, as 1! + 4! + 5! = 1 + 24 + 120 = 145.

Find the sum of all numbers which are equal to the sum of the factorial of their digits.

Note: As 1! = 1 and 2! = 2 are not sums they are not included.")
    ]
    // ReSharper disable once UnusedMember.Global
    public class Problem0034 : ISolution<int>
    {
        public bool HaveImplementedAnalyticSolution => false;

        public int BruteForceSolution()
        {
            var sum = 0;
            // Trial and error figured out an upper bound, later I looked up factorion and 41k turned out to be surprisingly close to the true bound.
            // Lower bound starting at 10 so there are at least 2 digits to sum (last sentence of problem description).
            for (var n = 10; n < 41_000; n++)
            {
                if (IsCuriousNumber(n))
                {
                    sum += n;
                }
            }
            return sum;
        }

        private static bool IsCuriousNumber(int n)
        {
            var factorials = new[]
            {
                1, // 0!
                1, // 1!
                2, // 2!
                6, // 3!
                24, // 4!
                120, // 5!
                720, // 6!
                5_040, // 7!
                40_320, // 8!
                362_880, // 9!
            };
            // did some testing and the foreach and manual sum is a fair bit faster than a linq sum
            var curiousSum = 0;
            foreach(var d in n.GetDigits())
            {
                curiousSum += factorials[d];
            }
            return n == curiousSum;
        }

        public int AnalyticSolution()
        {
            throw new NotImplementedException();
        }

        public int ExpectedSolution()
        {
            return 40_730;
        }
    }
}
