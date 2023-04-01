using System;

namespace ProjectEulerCSharp.EulerProblems._0030s
{
    [Euler(
        title: "Problem 36: Double-base palindromes",
        description: @"The decimal number, 585 = 1001001001 (binary), is palindromic in both bases.

Find the sum of all numbers, less than one million, which are palindromic in base 10 and base 2.

(Please note that the palindromic number, in either base, may not include leading zeros.)")
    ]
    // ReSharper disable once UnusedMember.Global
    public class Problem0036 : ISolution<uint>
    {
        public bool HaveImplementedAnalyticSolution => false;

        public uint BruteForceSolution()
        {
            uint sum = 0;
            // if the number cannot end in 0 in base 2 it has to be an odd number and cannot be divisible by 10
            for (uint n = 1; n< 1_000_000; n+=2)
            {
                if (n % 10 == 0)
                {
                    continue;
                }

                if (IsPalindromeBase10(n) && IsPalindromeBase2(n))
                {
                    sum += n;
                }
            }

            return sum;
        }

        private static bool IsPalindromeBase10(uint number)
        {
            uint reversed = 0;
            var original = number;
            while (original > 0)
            {
                reversed = reversed * 10 + original % 10;
                original /= 10;
            }
            return number == reversed;
        }

        private static bool IsPalindromeBase2(uint number)
        {
            uint reversed = 0;
            var original = number;
            while(original > 0)
            {
                reversed <<= 1;
                reversed |= original & 1;
                original >>= 1;
            }

            return number == reversed;
        }

        public uint AnalyticSolution()
        {
            throw new NotImplementedException();
        }

        public uint ExpectedSolution()
        {
            return 872_187;
        }
    }
}
