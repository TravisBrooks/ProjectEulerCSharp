﻿using System;

namespace TheProblems
{
    [Euler("Highly divisible triangular number",
        @"The sequence of triangle numbers is generated by adding the natural numbers. So
the 7th triangle number would be 
         1 + 2 + 3 + 4 + 5 + 6 + 7 = 28
The first ten terms would be:
         1, 3, 6, 10, 15, 21, 28, 36, 45, 55, ...
 1: 1
 3: 1, 3
 6: 1, 2, 3, 6
10: 1, 2, 5, 10
15: 1, 3, 5, 15
21: 1, 3, 7, 21
28: 1, 2, 4, 7, 14, 28
We can see that 28 is the first triangle number to have over five divisors.

What is the value of the first triangle number to have over five hundred
divisors?")]
    // ReSharper disable once UnusedMember.Global
    public class Problem0012 : ISolution<int>
    {
        /****************************************************************************************************
         * Highly divisible triangular number
         *
         * The sequence of triangle numbers is generated by adding the natural numbers. So the 7th triangle
         * number would be 1 + 2 + 3 + 4 + 5 + 6 + 7 = 28. The first ten terms would be:
         *          1, 3, 6, 10, 15, 21, 28, 36, 45, 55, ...
         * Let us list the factors of the first seven triangle numbers:
         *  1: 1
         *  3: 1,3
         *  6: 1,2,3,6
         * 10: 1,2,5,10
         * 15: 1,3,5,15
         * 21: 1,3,7,21
         * 28: 1,2,4,7,14,28
         * We can see that 28 is the first triangle number to have over five divisors.
         * What is the value of the first triangle number to have over five hundred divisors?
         ****************************************************************************************************/
        public int BruteForceSolution()
        {
            var currentTriangle = 0;
            var loopCnt = 1;
            var keepGoing = true;

            while (keepGoing)
            {
                currentTriangle += loopCnt;
                keepGoing = _DivisorCount(currentTriangle) <= 500;
                loopCnt++;
            }

            return currentTriangle;
        }

        public bool HaveImplementedAnalyticSolution => false;

        public int AnalyticSolution()
        {
            throw new NotImplementedException();
        }

        public int ExpectedSolution()
        {
            return 76_576_500;
        }

        private static int _DivisorCount(int currentTriangle)
        {
            var cnt = 2;
            int currentDivisor;
            var currentDivisorSquared = 0;
            var maxDivisor = Math.Floor(Math.Sqrt(currentTriangle));

            for (currentDivisor = 2; currentDivisor <= maxDivisor; currentDivisor++)
            {
                currentDivisorSquared = currentDivisor * currentDivisor;
                if (currentDivisorSquared >= currentTriangle)
                {
                    break;
                }

                if (currentTriangle % currentDivisor == 0)
                {
                    cnt += 2;
                }
            }

            if (currentDivisorSquared == currentTriangle)
            {
                cnt += 1;
            }

            return cnt;
        }
    }
}