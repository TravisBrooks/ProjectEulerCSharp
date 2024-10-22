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
        public bool HaveImplementedAnalyticSolution => true;

        public int BruteForceSolution()
        {
            var cnt = 0;
            for (var n = 1; n <= 100; n++)
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
            var ans = Factorial.Of(n) / (Factorial.Of(r) * Factorial.Of(n - r));
            return ans;
        }

        public int AnalyticSolution()
        {
            // Solution based on Pascal's triangle. Tbh this came from the https://projecteuler.net/overview=0053 Program C. I doubt I'd ever come up with this on my own.
            // The overview first builds a table of some values of n and r then notes the numbers are exactly the same as a Pascal triangle. Enter some loops over a 2d
            // array o build the triangle. Second, and extra cleverly, its realized that you don't actually need to store BigInteger values, you just need the count for
            // when it exceeds a million. This keeps the values in the array within the bounds of an Int32.
            // The overview also lists other solutions, but for simplicity and efficiency I really liked this solution.
            var cr = new int[101, 101];
            var cnt = 0;
            for (var n = 1; n <= 100; n++)
            {
                cr[n, 0] = cr[n, n] = 1;
                for (var r = 1; r <= n - 1; r++)
                {
                    cr[n, r] = cr[n - 1, r - 1] + cr[n - 1, r];
                    if (cr[n, r] > 1_000_000)
                    {
                        cnt++;
                        cr[n, r] = 1_000_000;
                    }
                }
            }

            return cnt;
        }

        public int ExpectedSolution()
        {
            return 4075;
        }
    }
}
