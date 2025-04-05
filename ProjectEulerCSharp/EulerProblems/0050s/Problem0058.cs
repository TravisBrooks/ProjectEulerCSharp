using System;
using ProjectEulerCSharp.EulerMath;

namespace ProjectEulerCSharp.EulerProblems._0050s
{
	[Euler(
			title: "Problem 58: Spiral Primes",
			description: @"Starting with 1 and spiralling anticlockwise in the following way, a square spiral with side length 7 is formed.

37 36 35 34 33 32 31
38 17 16 15 14 13 30
39 18  5  4  3 12 29
40 19  6  1  2 11 28
41 20  7  8  9 10 27
42 21 22 23 24 25 26
43 44 45 46 47 48 49

It is interesting to note that the odd squares lie along the bottom right diagonal, but what is more interesting is that 8 out of the 13 numbers lying along both diagonals are prime; that is, a ratio of 8/13 = 62%.

If one complete new layer is wrapped around the spiral above, a square spiral with side length 9 will be formed. If this process is continued, what is the side length of the square spiral for which the ratio of primes along both diagonals first falls below 10%?"
		)
	]
	// ReSharper disable once UnusedType.Global
	public class Problem0058 : ISolution<int>
	{
		public bool HaveImplementedAnalyticSolution => false;

		public int BruteForceSolution()
		{
			// This problem is a variation on #28
			var sideLength = 1;
			int a = 1, b = 1, c = 1, d = 1;
			var diagonalDelta = 0;
			var diagonalCount = 1.0;
			var primeCount = 0.0;
			var primeRatio = 1.0;
			while (primeRatio >= 0.1)
			{
				diagonalCount += 4;
				sideLength += 2;
				a += diagonalDelta += 2;
				b += diagonalDelta += 2;
				c += diagonalDelta += 2;
				d += diagonalDelta += 2;
				if (Primes.IsProbablyPrime(a))
				{
					primeCount++;
				}
				if (Primes.IsProbablyPrime(b))
				{
					primeCount++;
				}
				if (Primes.IsProbablyPrime(c))
				{
					primeCount++;
				}
				if (Primes.IsProbablyPrime(d))
				{
					primeCount++;
				}
				primeRatio = primeCount / diagonalCount;
			}
			return sideLength;
		}

		public int AnalyticSolution()
		{
			throw new NotImplementedException();
		}

		public int ExpectedSolution()
		{
			return 26_241;
		}
	}
}
