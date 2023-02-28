using System;
using System.Collections.Generic;
using System.Linq;
using ProjectEulerCSharp.EulerMath;

namespace ProjectEulerCSharp.EulerProblems
{
    [Euler(
title: "Problem 23: Non-abundant sums",
description: @"A perfect number is a number for which the sum of its proper divisors is exactly equal to the number. For example, the sum of the proper divisors of 28 would be 1 + 2 + 4 + 7 + 14 = 28, which means that 28 is a perfect number.

A number n is called deficient if the sum of its proper divisors is less than n and it is called abundant if this sum exceeds n.

As 12 is the smallest abundant number, 1 + 2 + 3 + 4 + 6 = 16, the smallest number that can be written as the sum of two abundant numbers is 24. By mathematical analysis, it can be shown that all integers greater than 28123 can be written as the sum of two abundant numbers. However, this upper limit cannot be reduced any further by analysis even though it is known that the greatest number that cannot be expressed as the sum of two abundant numbers is less than this limit.

Find the sum of all the positive integers which cannot be written as the sum of two abundant numbers.")
    ]
    // ReSharper disable once UnusedMember.Global
    public class Problem0023 : ISolution<int>
    {
        public bool HaveImplementedAnalyticSolution => false;

        public int BruteForceSolution()
        {
            var allAbundantNums = new HashSet<int> { 12 };
            for (var i = 13; i <= 28123; i++)
            {
                var sumOfDivisors = ProperDivisors.Of(i).Sum();
                if (sumOfDivisors > i)
                {
                    allAbundantNums.Add(i);
                }
            }

            // the problem said 24 was the first so sum all integers until then
            var sum = Enumerable.Range(1, 23).Sum();
            for (var i = 25; i <= 28123; i++)
            {
                if (!IsSumOfTwoAbundantNums(allAbundantNums, i))
                {
                    sum += i;
                }
            }
            return sum;
        }

        private bool IsSumOfTwoAbundantNums(HashSet<int> allAbundantNums, int n)
        {
            return allAbundantNums
                .Where(a => a < n)
                .Any(a => allAbundantNums.Contains(n - a));
        }

        public int AnalyticSolution()
        {
            throw new NotImplementedException();
        }

        public int ExpectedSolution()
        {
            return 4_179_871;
        }
    }
}
