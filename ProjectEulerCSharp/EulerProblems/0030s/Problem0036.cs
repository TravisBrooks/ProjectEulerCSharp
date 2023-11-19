namespace ProjectEulerCSharp.EulerProblems._0030s
{
    [Euler(
        title: "Problem 36: Double-base palindromes",
        description: @"The decimal number, 585 = 1001001001 (binary), is palindromic in both bases.

Find the sum of all numbers, less than one million, which are palindromic in base 10 and base 2.

(Please note that the palindromic number, in either base, may not include leading zeros.)")
    ]
    // ReSharper disable once UnusedType.Global
    public class Problem0036 : ISolution<uint>
    {
        public bool HaveImplementedAnalyticSolution => true;

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

                if (IsPalindrome(n, 10) && IsPalindrome(n, 2))
                {
                    sum += n;
                }
            }

            return sum;
        }

        private static bool IsPalindrome(uint number, uint baseN)
        {
            uint reversed = 0;
            var original = number;
            while (original > 0)
            {
                var digit = original % baseN;
                reversed = reversed * baseN + digit;
                original /= baseN;
            }
            return number == reversed;
        }

        /// <summary>
        /// This method isn't used in the solution, its here only to illustrate how the solution works.
        /// I took the MakePalindrome method provided by the Euler overview and hardcoded it to base 10 because that's more intuitive to
        /// follow. It still took a bit of squinting to see how this alg works so I'm commenting it to make it more obvious.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="palindromeHasOddDigitCount">If the number is 12 and this param is true the palindrome is 121, if its false its 1221.</param>
        /// <returns></returns>
        private static uint MakePalindromeBase10(uint number, bool palindromeHasOddDigitCount)
        {
            var palindrome = number;
            if (palindromeHasOddDigitCount)
            {
                // this drops the least significant digit from number so it is not included in the palindrome, ie 12=>121 instead of 1221
                number /= 10;
            }

            while (number > 0)
            {
                // get the rightmost digit from number
                var digit = number % 10;
                // the mult by base is basically a left shift, then add the digit to the empty slot that was created
                palindrome = 10 * palindrome + digit;
                // the div by base is a right shift, dropping the digit we added to the palindrome above
                number /= 10;
            }
            return palindrome;
        }

        private static uint MakePalindromeBase2(uint number, bool palindromeHasOddDigitCount)
        {
            var palindrome = number;
            if (palindromeHasOddDigitCount)
            {
                number >>= 1;
            }

            while (number > 0)
            {
                uint digit = number & 1;
                palindrome <<= 1;
                palindrome += digit;
                number >>= 1;
            }

            return palindrome;
        }

        /// <summary>
        /// The analytic solution, provided by the overview pdf, is quite clever. Instead of searching over all integers in the range
        /// and seeing which are palindromes, use a method to generate all the palindromes in a base in the range and see if those
        /// palindromes are also palindromes in the other base.
        /// </summary>
        /// <returns></returns>
        public uint AnalyticSolution()
        {
            uint sum = 0;
            var upperBound = 1_000_000;
            uint n = 1;
            var palindrome = MakePalindromeBase2(n, true);
            // when n is 999 its odd length palindrome is 99_999 so the palindrome grows much faster than n
            while (palindrome < upperBound)
            {
                if (IsPalindrome(palindrome, 10))
                {
                    sum+=palindrome;
                }
                n++;
                palindrome = MakePalindromeBase2(n, true);
            }
            n = 1;
            palindrome = MakePalindromeBase2(n, false);
            // when n is 999 its even length palindrome is 999_999 so the palindrome grows much faster than n
            while (palindrome < upperBound)
            {
                if (IsPalindrome(palindrome, 10))
                {
                    sum+=palindrome;
                }
                n++;
                palindrome = MakePalindromeBase2(n, false);
            }

            return sum;
        }

        public uint ExpectedSolution()
        {
            return 872_187;
        }
    }
}
