using System.Collections.Generic;
using System.Linq;

namespace TheProblems
{
    [Euler("Sum square difference",
        @"The sum of the squares of the first ten natural numbers is,
     1^2 + 2^2 + ... + 10^2 = 385
The square of the sum of the first ten natural numbers is,
     (1 + 2 + ... + 10)^2 = 3025
Hence the difference between the sum of the squares of the first ten natural numbers and the square of the sum is
     3025 - 385 = 2640
Find the difference between the sum of the squares of the first one hundred natural numbers and the square of the sum.")]
    // ReSharper disable once UnusedMember.Global
    public class Problem0006 : ISolution<int>
    {
        /****************************************************************************************************
         * Sum square difference
         *
         * The sum of the squares of the first ten natural numbers is,
         *      1^2 + 2^2 + ... + 10^2 = 385
         * The square of the sum of the first ten natural numbers is,
         *      (1 + 2 + ... + 10)^2 = 3025
         * Hence the difference between the sum of the squares of the first ten natural numbers and the square of the sum is
         *      3025 - 385 = 2640
         * Find the difference between the sum of the squares of the first one hundred natural numbers and the square of the sum.
         ****************************************************************************************************/
        public int BruteForceSolution()
        {
            var range = Enumerable.Range(1, 100).ToArray();
            return SquareOfSum(range) - SumOfSquares(range);
        }

        public bool HaveImplementedAnalyticSolution => false;

        public int AnalyticSolution()
        {
            throw new System.NotImplementedException();
        }

        public int ExpectedSolution()
        {
            return 25164150;
        }

        private static int SumOfSquares(IEnumerable<int> range)
        {
            return range.Select(n => n * n).Sum();
        }

        private static int SquareOfSum(IEnumerable<int> range)
        {
            var sum = range.Sum();
            return sum * sum;
        }
    }
}