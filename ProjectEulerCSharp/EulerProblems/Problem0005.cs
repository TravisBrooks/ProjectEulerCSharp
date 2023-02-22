using System.Linq;

namespace ProjectEulerCSharp.EulerProblems
{
    [Euler("Smallest multiple",
        @"2520 is the smallest number that can be divided by each of the numbers from 1 to
10 without any remainder.

What is the smallest positive number that is evenly divisible by all of the
numbers from 1 to 20?")]
    // ReSharper disable once UnusedMember.Global
    public class Problem0005 : ISolution<int>
    {
        // This problem is kind of dumb to do brute force, its more of a pen and paper problem. I tried to come up with a few
        // optimizations to the brute force method below to reduce the search space but its still pretty slow.

        /****************************************************************************************************
         * Smallest multiple
         *
         * 2520 is the smallest number that can be divided by each of the numbers from 1 to 10 without any remainder.
         * What is the smallest positive number that is evenly divisible by all of the numbers from 1 to 20?
         ****************************************************************************************************/
        public int BruteForceSolution()
        {
            // reverse does slight optimization in divisor test, many numbers pass beginning of range but not many are divisible by 19 and 17
            var divisors = new[] { 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            // initial guess is product of all primes less than 20
            var guess = 2 * 3 * 5 * 7 * 11 * 13 * 17 * 19;
            // Lets increment the guess until it is evenly divisible by 20, then we can increment it by 20 in loop below saving us from testing a bunch of numbers.
            while (guess % 20 != 0)
            {
                guess += 1;
            }

            while (true)
            {
                var passesTest = divisors.All(div => guess % div == 0);
                if (passesTest)
                {
                    break;
                }

                guess += 20;
            }

            return guess;
        }

        public bool HaveImplementedAnalyticSolution => false;

        public int AnalyticSolution()
        {
            throw new System.NotImplementedException();
        }

        public int ExpectedSolution()
        {
            return 232792560;
        }
    }
}