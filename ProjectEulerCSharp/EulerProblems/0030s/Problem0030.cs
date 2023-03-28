using System;
using ProjectEulerCSharp.EulerMath;

namespace ProjectEulerCSharp.EulerProblems._0030s
{
    [Euler(
        title: "Problem 30: Digit fifth powers",
        description: @"Surprisingly there are only three numbers that can be written as the sum of fourth powers of their digits:

1634 = 1^4 + 6^4 + 3^4 + 4^4
8208 = 8^4 + 2^4 + 0^4 + 8^4
9474 = 9^4 + 4v4 + 7^4 + 4^4
As 1 = 1^4 is not a sum it is not included.

The sum of these numbers is 1634 + 8208 + 9474 = 19316.

Find the sum of all the numbers that can be written as the sum of fifth powers of their digits.")
    ]
    // ReSharper disable once UnusedMember.Global
    public class Problem0030 : ISolution<int>
    {
        public bool HaveImplementedAnalyticSolution => false;

        public int BruteForceSolution()
        {
            var powers = new[] { 0, 1, 32, 243, 1_024, 3_125, 7_776, 16_807, 32_768, 59_049 };
            var answerSum = 0;
            // based on values in powers array I'm making a guess of what the range of the search space is
            for (var n = 1900; n < 200_000; n++)
            {
                var digitSum = 0;
                foreach (var digitChar in n.ToString())
                {
                    digitSum += powers[digitChar.ToIntFast()];
                }

                if (digitSum == n)
                {
                    answerSum += n;
                }
            }

            return answerSum;
        }

        public int AnalyticSolution()
        {
            throw new NotImplementedException();
        }

        public int ExpectedSolution()
        {
            return 443_839;
        }
    }
}
