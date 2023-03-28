using System.Collections.Generic;
using System.Numerics;
using ProjectEulerCSharp.EulerMath;

namespace ProjectEulerCSharp.EulerProblems._0010s
{
    [Euler(
        title: "Problem 15: Lattice paths",
        description: @"Starting in the top left corner of a 2×2 grid, and only being able to move to the right and down, there are exactly 6 routes to the bottom right corner.

How many such routes are there through a 20×20 grid?")
    ]
    // ReSharper disable once UnusedMember.Global
    public class Problem0015 : ISolution<long>
    {
        public bool HaveImplementedAnalyticSolution => true;

        public long BruteForceSolution()
        {
            var cache = new Dictionary<(int, int), long>();
            var answer = BacktrackAllRoutes((20, 20), cache);
            return answer;
        }

        /// <summary>
        /// starting from the bottom right this alg backtracks to count all the routes to get to the upper left
        /// </summary>
        /// <param name="currentPos"></param>
        /// <param name="cache"></param>
        /// <returns></returns>
        private static long BacktrackAllRoutes((int, int) currentPos, IDictionary<(int, int), long> cache)
        {
            // Thanks to the caching this alg is a surprisingly fast to way to count the number of routes. The basic idea is we say
            // from my current position (or initially the goal position) if I went left one step and calculated all the paths to get home (0, 0),
            // or if I went up 1 step and counted all those ways to get home, then the sum of those 2 route counts must be the total count of
            // all routes.

            var (x, y) = currentPos;
            if (x == 0 && y == 0)
            {
                // we made it home so increment the total route count by 1
                return 1;
            }

            long cnt = 0;
            // make sure we haven't gone all the way up already
            if (y > 0)
            {
                // find all routes 1 above current position
                var upOne = (x, y - 1);
                if (!cache.ContainsKey(upOne))
                {
                    cache[upOne] = BacktrackAllRoutes(upOne, cache);
                }

                cnt += cache[upOne];
            }

            // make sure we haven't gone all the way left already
            if (x > 0)
            {
                // find all routes 1 left from current position
                var leftOne = (x - 1, y);
                if (!cache.ContainsKey(leftOne))
                {
                    cache[leftOne] = BacktrackAllRoutes(leftOne, cache);
                }

                cnt += cache[leftOne];
            }

            return cnt;
        }

        public long AnalyticSolution()
        {
            // This seems like a hard problem, unless you took some discrete math courses. We know that whatever path we take it will have
            // to involve 20 moves left and 20 moves down, 40 moves all together. So its all the permutations of 20 Lefts and 20 downs, or:
            //   40! / (20! * (40 - 20)!)
            // = 40! / (20! * 20!)
            var dividend = Factorial.Of(40);
            var divisor = BigInteger.Multiply(Factorial.Of(20), Factorial.Of(20));
            var bigAnswer = BigInteger.Divide(dividend, divisor);
            var answer = long.Parse(bigAnswer.ToString());
            return answer;
        }

        public long ExpectedSolution()
        {
            return 137_846_528_820;
        }
    }
}