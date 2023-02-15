using System;
using System.Numerics;

namespace TheProblems
{
    [Euler(
title: "Lattice paths",
description: @"Starting in the top left corner of a 2×2 grid, and only being able to move to the right and down, there are exactly 6 routes to the bottom right corner.
How many such routes are there through a 20×20 grid?"
        )]
    // ReSharper disable once UnusedMember.Global
    public class Problem0015 : ISolution<long>
    {
        public bool HaveImplementedAnalyticSolution => false;

        public long BruteForceSolution()
        {
            // this is a problem where coming up with some brute force way to solve it is way harder than 
            throw new NotImplementedException();
        }
        
        public long AnalyticSolution()
        {
            // This seems like a hard problem, unless you took some discrete math courses. We know that whatever path we take it will have
            // to involve 20 moves left and 20 moves down, 40 moves all together. So its all the permutations of 20 Lefts and 20 downs, or:
            // 40! / (20! * (40 - 20)!) = 40! / (20! * 20!)
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
