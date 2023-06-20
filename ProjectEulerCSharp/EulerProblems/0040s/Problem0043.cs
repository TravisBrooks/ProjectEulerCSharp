using System;
using System.Linq;
using ProjectEulerCSharp.EulerMath;

namespace ProjectEulerCSharp.EulerProblems._0040s
{
    [Euler(
        title: "Problem 43: Sub-string Divisibility",
        description: @"The number, 1406357289, is a 0 to 9 pandigital number because it is made up of each of the digits 0 to 9 in some order, but it also has a rather interesting sub-string divisibility property.

Let d1 be the 1st digit, d2 be the 2nd digit, and so on. In this way, we note the following:

d2d3d4 = 406 is divisible by 2
d3d4d5 = 063 is divisible by 3
d4d5d6 = 635 is divisible by 5
d5d6d7 = 357 is divisible by 7
d6d7d8 = 572 is divisible by 11
d7d8d9 = 728 is divisible by 13
d8d9d10 = 289 is divisible by 17

Find the sum of all 0 to 9 pandigital numbers with this property.")
    ]
    // ReSharper disable once UnusedMember.Global
    public class Problem0043 : ISolution<long>
    {
        public bool HaveImplementedAnalyticSolution => false;

        public long BruteForceSolution()
        {
            var digits = Enumerable.Range(0, 10).ToArray();
            long sum = 0;
            foreach (var panDigits in Permutations.Of(digits, PermutationAlgorithm.Heap))
            {
                if (panDigits[0] == 0)
                {
                    continue;
                }

                if (HasRatherInterestingProperty(panDigits))
                {
                    var number = panDigits.ToLong();
                    sum += number;
                }
            }
            return sum;
        }

        public long AnalyticSolution()
        {
            throw new NotImplementedException();
        }

        private static bool HasRatherInterestingProperty(ReadOnlySpan<int> panDigits)
        {
            if (panDigits.Slice(1, 3).ToInt() % 2 == 0)
            {
                if (panDigits.Slice(2, 3).ToInt() % 3 == 0)
                {
                    if (panDigits.Slice(3, 3).ToInt() % 5 == 0)
                    {
                        if (panDigits.Slice(4, 3).ToInt() % 7 == 0)
                        {
                            if (panDigits.Slice(5, 3).ToInt() % 11 == 0)
                            {
                                if (panDigits.Slice(6, 3).ToInt() % 13 == 0)
                                {
                                    if (panDigits.Slice(7, 3).ToInt() % 17 == 0)
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }
        
        public long ExpectedSolution()
        {
            return 16_695_334_890;
        }
    }
}
