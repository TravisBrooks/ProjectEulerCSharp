using System.Collections.Generic;
using System.Linq;

namespace ProjectEulerCSharp.EulerProblems
{
    [Euler(
        title: "Problem 6: Sum square difference",
        description: @"The sum of the squares of the first ten natural numbers is,
     1^2 + 2^2 + ... + 10^2 = 385
The square of the sum of the first ten natural numbers is,
     (1 + 2 + ... + 10)^2 = 3025
Hence the difference between the sum of the squares of the first ten natural numbers and the square of the sum is
     3025 - 385 = 2640

Find the difference between the sum of the squares of the first one hundred natural numbers and the square of the sum.")
    ]
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
            return _SquareOfSum(range) - _SumOfSquares(range);
        }

        public bool HaveImplementedAnalyticSolution => true;

        public int AnalyticSolution()
        {
            // I found the formulas for the analytic solution first at:
            // https://en.wikipedia.org/wiki/1_%2B_2_%2B_3_%2B_4_%2B_%E2%8B%AF
            // for the _AnalyticSumOfInts, there were enough keywords in the wiki article to lead me to the better guide that also
            // gave the formula for _AnalyticSumOfSquares
            // https://brilliant.org/wiki/sum-of-n-n2-or-n3/
            // The wiki link does a good job of how the sum relates to triangle numbers, while the brilliant.org link describes the 
            // generalization of the sum of a series raised to whatever power (here we did sum of ints to power of 1 and to 2)
            var sum = _AnalyticSumOfInts(100);
            var sumOfSquares = _AnalyticSumOfSquares(100);
            return sum * sum - sumOfSquares;
        }

        public int ExpectedSolution()
        {
            return 25164150;
        }

        private static int _SumOfSquares(IEnumerable<int> range)
        {
            return range.Select(n => n * n).Sum();
        }

        private static int _SquareOfSum(IEnumerable<int> range)
        {
            var sum = range.Sum();
            return sum * sum;
        }

        private int _AnalyticSumOfInts(int n)
        {
            // this formula was apparently known by the Pythagoreans in 600BC
            return n * (n + 1) >> 1;
        }

        private int _AnalyticSumOfSquares(int n)
        {
            return n * (n + 1) * (2 * n + 1) / 6;
        }
    }
}