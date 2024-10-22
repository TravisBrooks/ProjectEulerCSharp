using System;
using System.Numerics;
using ProjectEulerCSharp.EulerMath;

namespace ProjectEulerCSharp.EulerProblems._0050s
{
    [Euler(
            title: "Problem 53: Combinatoric Selections",
            description: @"There are exactly ten ways of selecting three from five, 12345:

123, 124, 125, 134, 135, 145, 234, 235, 245, and 345

In combinatorics, we use the notation, (5 3)=10.

In general, (n r) = n!/r!(n-r)!, where r≤n, n! = n x (n - 1) X ... 3 x 2 x 1, and 0!=1.

It is not until n = 23, that a value exceeds one-million: (23 10) = 1,144,066.

How many, not necessarily distinct, values of (n r) for 1 ≤ n ≤ 100, are greater than one-million?"
        )
    ]
    // ReSharper disable once UnusedType.Global
    public class Problem0053 : ISolution<int>
    {
        public bool HaveImplementedAnalyticSolution => false;

        public int BruteForceSolution()
        {
            var cnt = 0;
            for (var n = 1; n<=100; n++)
            {
                for (var r = 1; r <= n; r++)
                {
                    var comb = Combinatoric(n, r);
                    if (comb > 1_000_000)
                    {
                        cnt++;
                    }
                }
            }
            return cnt;
        }

        private static BigInteger Combinatoric(int n, int r)
        {
            var nFact = Factorial.Of(n);
            var rFact = Factorial.Of(r);
            var nMinusRfact = Factorial.Of(n - r);
            var sln = BigInteger.Divide(nFact, BigInteger.Multiply(rFact, nMinusRfact));
            return sln;
        }

        public int AnalyticSolution()
        {
            throw new NotImplementedException();
        }

        public int ExpectedSolution()
        {
            return 4075;
        }
    }
}
