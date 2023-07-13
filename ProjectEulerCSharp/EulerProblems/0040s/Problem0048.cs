using System;
using System.Linq;
using System.Numerics;

namespace ProjectEulerCSharp.EulerProblems._0040s
{
    [Euler(
        title: "Problem 48: Self Powers",
        description: @"The series, 1^1 + 2^2 + 3^3 + ... + 10^10 = 10405071317.

Find the last ten digits of the series, 1^1 + 2^2 + 3^3 + ... + 1000^1000.")
    ]
    // ReSharper disable once UnusedMember.Global
    public class Problem0048 : ISolution<ulong>
    {
        public bool HaveImplementedAnalyticSolution => false;

        public ulong BruteForceSolution()
        {
            // If you have large integer support this problem is quite easy.
            var acc = Enumerable.Range(1, 1000)
                .Select(i => BigInteger.Pow(BigInteger.Parse(i.ToString()), i))
                .Aggregate((a, b) => a + b);
            var lastTen = acc.ToString()[^10..];
            return ulong.Parse(lastTen);
        }

        public ulong AnalyticSolution()
        {
            throw new NotImplementedException();
        }

        public ulong ExpectedSolution()
        {
            return 9_110_846_700;
        }
    }
}
