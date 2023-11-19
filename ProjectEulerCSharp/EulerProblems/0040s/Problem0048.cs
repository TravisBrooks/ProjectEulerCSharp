using System.Linq;
using System.Numerics;

namespace ProjectEulerCSharp.EulerProblems._0040s
{
    [Euler(
        title: "Problem 48: Self Powers",
        description: @"The series, 1^1 + 2^2 + 3^3 + ... + 10^10 = 10405071317.

Find the last ten digits of the series, 1^1 + 2^2 + 3^3 + ... + 1000^1000.")
    ]
    // ReSharper disable once UnusedType.Global
    public class Problem0048 : ISolution<long>
    {
        public bool HaveImplementedAnalyticSolution => true;

        public long BruteForceSolution()
        {
            // If you have large integer support this problem is quite easy.
            var acc = Enumerable.Range(1, 1000)
                .Select(i => BigInteger.Pow(BigInteger.Parse(i.ToString()), i))
                .Aggregate((a, b) => a + b);
            var lastTen = acc.ToString()[^10..];
            return long.Parse(lastTen);
        }

        public long AnalyticSolution()
        {
            // not much of an analytic solution, just rolled my own Pow fn that drops everything but last 10 digits so math could be
            // done in 64 bits
            var acc = Enumerable.Range(1, 1000)
                .Select(i => PowLeastSignificantDigits(i, i))
                // The quirks of linq, doing the sum in Aggregate is faster than calling Sum, no idea why...
                .Aggregate((a, b) => a + b);
            return acc % 10000000000;
        }

        private static long PowLeastSignificantDigits(long n, long power)
        {
            var originalN = n;
            while (true)
            {
                if (power == 1)
                {
                    return n;
                }
                // The modulo here is the secret to not getting an overflow. The result only needs last 10 digits so can discard the rest.
                n = (n % 10000000000) * originalN;
                power -= 1;
            }
        }

        public long ExpectedSolution()
        {
            return 9_110_846_700;
        }
    }
}
