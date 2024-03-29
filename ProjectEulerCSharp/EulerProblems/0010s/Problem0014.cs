﻿using System;
using System.Collections.Generic;

namespace ProjectEulerCSharp.EulerProblems._0010s
{
    [Euler(
        title: "Problem 14: Longest Collatz sequence",
        description: @"The following iterative sequence is defined for the set of positive integers:
        n → n/2 (n is even)
        n → 3n + 1 (n is odd)
Using the rule above and starting with 13, we generate the following sequence:
        13 → 40 → 20 → 10 → 5 → 16 → 8 → 4 → 2 → 1
It can be seen that this sequence (starting at 13 and finishing at 1) contains 10 terms. Although it has not been proved yet (Collatz Problem), it is thought that all starting numbers finish at 1.

Which starting number, under one million, produces the longest chain?

NOTE: Once the chain starts the terms are allowed to go above one million")
    ]
    // ReSharper disable once UnusedType.Global
    public class Problem0014 : ISolution<int>
    {
        public bool HaveImplementedAnalyticSolution => false;

        public int BruteForceSolution()
        {
            var collatzCache = new Dictionary<long, long> { [1] = 1 };
            long maxChainLen = 0;
            var number = 0;
            for (var n = 10_000; n < 1_000_000; n++)
            {
                var len = CollatzLen(n, collatzCache);
                if (len > maxChainLen)
                {
                    maxChainLen = len;
                    number = n;
                }
            }

            return number;
        }

        private static long CollatzLen(long n, IDictionary<long, long> cache)
        {
            if (cache.TryGetValue(n, out var value))
            {
                return value;
            }

            if (n % 2 == 0)
            {
                cache[n] = 1 + CollatzLen(n / 2, cache);
            }
            else
            {
                // This mult by 3 can get big if it keeps making odd numbers, that's why it needs to be a long instead of int
                // 56991483520 : the biggest cache key value
                //  2147483647 : Int32.MaxValue
                cache[n] = 1 + CollatzLen(3 * n + 1, cache);
            }

            return cache[n];
        }

        public int AnalyticSolution()
        {
            throw new NotImplementedException();
        }

        public int ExpectedSolution()
        {
            return 837_799;
        }
    }
}