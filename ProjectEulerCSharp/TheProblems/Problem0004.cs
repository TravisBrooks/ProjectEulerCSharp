namespace TheProblems
{
    [Euler("Largest palindrome product",
        @"A palindromic number reads the same both ways. The largest palindrome made from
the product of two 2-digit numbers is 9009 = 91 × 99.

Find the largest palindrome made from the product of two 3-digit numbers.")]
    // ReSharper disable once UnusedMember.Global
    public class Problem0004 : ISolution<int>
    {
        /****************************************************************************************************
         * Largest palindrome product
         *
         * A palindromic number reads the same both ways. The largest palindrome made from the product of two 2-digit numbers is 9009 = 91 × 99.
         * Find the largest palindrome made from the product of two 3-digit numbers.
         ****************************************************************************************************/
        public int BruteForceSolution()
        {
            var max = 0;
            for (var i = 900; i < 1000; i++)
            {
                for (var j = 900; j < 1000; j++)
                {
                    var prod = i * j;
                    if (_IsPalindrome(prod) && prod > max)
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
            throw new System.NotImplementedException();
        }

        public int ExpectedSolution()
        {
            return 906609;
        }

        private static bool _IsPalindrome(int candidate)
        {
            return candidate == _ReverseInt(candidate);
        }

        private static int _ReverseInt(int number)
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