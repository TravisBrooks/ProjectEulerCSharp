namespace ProjectEulerCSharp.EulerProblems
{
    [Euler("Multiples of 3 or 5",
        @"If we list all the natural numbers below 10 that are multiples of 3 or 5, we get
3, 5, 6 and 9. The sum of these multiples is 23.

Find the sum of all the multiples of 3 or 5 below 1000.")
    ]
    // ReSharper disable once UnusedMember.Global
    public class Problem0001 : ISolution<int>
    {
        /****************************************************************************************************
         * Multiples of 3 or 5
         *
         * If we list all the natural numbers below 10 that are multiples of 3 or 5, we get 3, 5, 6 and 9. The sum of these multiples is 23.
         * Find the sum of all the multiples of 3 or 5 below 1000.
         ****************************************************************************************************/


        public int BruteForceSolution()
        {
            var answer = 0;
            for (var i = 3; i < 1000; i++)
            {
                if (i % 3 == 0 || i % 5 == 0)
                {
                    answer += i;
                }
            }

            return answer;
        }

        public bool HaveImplementedAnalyticSolution => true;

        public int AnalyticSolution()
        {
            /****************************************************************************************************
             * The key analysis for this problem is to note that for out target of numbers less than 1000 we want to have the sum of all
             * divisible by 3, plus the sum of those divisible by 5, and because multiple of 15 would be included in both of these sums 
             * we must subtract the sum of multiples of 15. So given a function SumOfDivisors(n, upperLimit) the solution could be expressed:
             *      SumOfDivisors(3, 999) + SumOfDivisors(5, 999) - SumOfDivisors(15, 999)
             * It would be pretty easy to fall into the trap of just once again coming up with a brute force implementation of SumOfDivisors
             * but this is where the real analysis happens. If we show some of the divisors for 3 and 5 a pattern can be seen:
             *      3+ 6+ 9+12+....+999 = 3*(1+2+3+4+...+333)
             *      5+10+15+20+....+995 = 5*(1+2+3+4+...+199)
             * The pattern for both cases on the right hand side is:
             *	  n*(1+2+3+4+...+maxDivisor)
             * where maxDivisor is the last multiple of n less than upperLimit.
             *
             * What we would like to do is have a closed form of the series (1+2+3+4+...+maxDivisor) without having to do all the tedious
             * addition. This link has an excellent explanation of how this can be derived:
             *      http://mathandmultimedia.com/2010/09/15/sum-first-n-positive-integers/
             * The formula proven in the above link is that the closed for the addition of 1+2+3+4...+p is:
             *      (p * (p + 1))/2
             * From that formula we can simplify the SumOfDivisors series to its closed form:
             *      n * (maxDivisor * (maxDivisor + 1))/2
             ****************************************************************************************************/

            static int SumOfDivisors(int n, int upperLimit)
            {
                var maxDivisor = upperLimit / n;
                return n * (maxDivisor * (maxDivisor + 1)) / 2;
            }

            return SumOfDivisors(3, 999) + SumOfDivisors(5, 999) - SumOfDivisors(15, 999);
        }

        public int ExpectedSolution()
        {
            return 233168;
        }
    }
}