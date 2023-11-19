using System;

namespace ProjectEulerCSharp.EulerProblems._0001s
{
    [Euler(
        title: "Problem 4: Largest palindrome product",
        description: @"A palindromic number reads the same both ways. The largest palindrome made from the product of two 2-digit numbers is 9009 = 91 × 99.

Find the largest palindrome made from the product of two 3-digit numbers.")
    ]
    // ReSharper disable once UnusedType.Global
    public class Problem0004 : ISolution<int>
    {
        public int BruteForceSolution()
        {
            var max = 0;
            for (var i = 900; i < 1000; i++)
            {
                // can set j to i rather than starting from 900 each time. when i = 900 j will on 2nd loop be 901, so no reason to recalc 901 * 900 on 2nd loop of i
                for (var j = i; j < 1000; j++)
                {
                    var prod = i * j;
                    if (IsPalindrome(prod) && prod > max)
                    {
                        max = prod;
                    }
                }
            }

            return max;
        }

        public bool HaveImplementedAnalyticSolution => false;

        public int AnalyticSolution()
        {
            /****************************************************************************************************
             * If we take the largest possible product of two 3-digit-numbers we have 999^2=998_001, so we know
             * we're dealing with a 6 digit number. We can represent the digits as xyzzyx, or multiplying each
             * digit by the necessary power of 10 for each digit's position:
             *      = x*10^5 + y*10^4 + z*10^3 + z*10^2 + y*10^1 + x*10^0
             *      = 100_000x + 10_000y + 1000z + 100z + 10y + x
             *      = 100_001x + 10_010y + 1100z
             *      = 11(9091x + 910y + 100z)
             * This formula itself doesn't tell us much useful beyond the fact that 11 is a factor of 1 of the
             * 2 numbers in the product.
            ****************************************************************************************************/
            throw new NotImplementedException("Not sure I have a good/reasonable proof yet for an analytical solution. Not anything better than the brute force solution.");
        }

        public int ExpectedSolution()
        {
            return 906609;
        }

        private static bool IsPalindrome(int candidate)
        {
            return candidate == ReverseInt(candidate);
        }

        private static int ReverseInt(int number)
        {
            var result = 0;
            while (number > 0)
            {
                var digit = number % 10;
                result = result * 10 + digit;
                number /= 10;
            }

            return result;
        }
    }
}