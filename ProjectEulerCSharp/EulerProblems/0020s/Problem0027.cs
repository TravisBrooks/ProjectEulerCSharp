using System;
using ProjectEulerCSharp.EulerMath;

namespace ProjectEulerCSharp.EulerProblems._0020s
{
    [Euler(
        title: "Problem 27: Quadratic primes",
        description: @"Euler discovered the remarkable quadratic formula:
    n^2 + n + 41
It turns out that the formula will produce 40 primes for the consecutive integer values 0 ≤ n ≤ 39. However, when n=40, 40^2 + 40 + 41 = 40(40 + 1) + 41 is divisible by 41, and certainly when n=41, 41^2 + 41 + 41 is clearly divisible by 41.

The incredible formula n^2 - 79n + 1601 was discovered, which produces 80 primes for the consecutive values 0 ≤ n ≤ 79. The product of the coefficients, −79 and 1601, is −126479.

Considering quadratics of the form:
n^2 + an + b, where |a| less-than 1000 and |b|≤1000
where |n| is the modulus/absolute value of n
e.g. |11|=11 and |-4|=4

Find the product of the coefficients, a and b, for the quadratic expression that produces the maximum number of primes for consecutive values of n, starting with n=0.")
    ]
    // ReSharper disable once UnusedMember.Global
    public class Problem0027 : ISolution<int>
    {
        public bool HaveImplementedAnalyticSolution => false;

        public int BruteForceSolution()
        {
            // The value for upper bounds is pretty arbitrary, just picked a number I guessed was big enough
            var isPrime = Primes.IsPrime(upperBounds: 15_000);
            var maxPrimes = 1;
            var maxProduct = 0;
            
            for (var a = -999; a < 1000; a++)
            {
                for (var b = -1000; b <= 1000; b++)
                {
                    var primeCount = 0;
                    // The value for n is somewhat arbitrary, just picked a number I guessed would have to be bigger than the biggest sequence of primes. The loop is broken as soon as a non-prime is found.
                    for (var n = 0; n < 500; n++)
                    {
                        var x = n * n + a * n + b;
                        if (isPrime(x))
                        {
                            primeCount++;
                        }
                        else
                        {
                            if (primeCount > maxPrimes)
                            {
                                maxPrimes = primeCount;
                                maxProduct = a * b;
                            }
                            break;
                        }
                    }
                }
            }
            
            return maxProduct;
        }

        public int AnalyticSolution()
        {
            throw new NotImplementedException();
        }

        public int ExpectedSolution()
        {
            return -59231;
        }
    }
}
