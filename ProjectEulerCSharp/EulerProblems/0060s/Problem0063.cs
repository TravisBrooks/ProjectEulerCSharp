using System;
using System.Numerics;
using ProjectEulerCSharp.EulerMath;

namespace ProjectEulerCSharp.EulerProblems._0060s
{
	[Euler(
			title: "Problem 63: Powerful Digit Counts",
			description: @"The 5-digit number, 16807, is a fifth power: 27^5 = 16807. Similarly, the 9-digit number, 134217728 = 8^9,, is a ninth power.

How many n-digit positive integers exist which are also an nth power?"
		)
	]
	// ReSharper disable once UnusedType.Global
	internal class Problem0063 : ISolution<int>
	{
		public bool HaveImplementedAnalyticSolution => true;

		public int BruteForceSolution()
		{
			var cnt = 0;
			for(var p = 1; p <= 25; p++)
			{
				for(var n = 1; n <= 10; n++)
				{
					// have to use BigInteger because n^p can be larger than long.MaxValue
					var nToP = BigInteger.Pow(new BigInteger(n), p);
					if (nToP.GetCountOfDigits() == p)
					{
						cnt++;
					}
				}
			}

			return cnt;
		}

		public int AnalyticSolution()
		{
			// This proof took a bit of effort, I'd forgotten a lot of the rules for equivalent expression wrt logarithms
			// and inequalities so needed a bit of research.

			// A function for the number of digits in a number n is:
			// d(n) = floor(log10(n)) + 1
			// This problem is to find when a number n^p has exactly p digits. Using the previous function:
			// d(n^p) = floor(log10(n^p)) + 1
			// log10(n^p) is equivalent to p * log10(n), so:
			// d(n^p) = floor(p * log10(n)) + 1
			// We want d(n^p) = p, so:
			// floor(p * log10(n)) + 1 = p
			// floor(p * log10(n)) = p - 1
			// the floor function rounds down, so p * log10(n) must be less than p but greater than or equal to p - 1:
			// p - 1 <= p * log10(n) < p
			// Dividing by p gives us:
			// (p - 1) / p <= log10(n) < 1
			// or
			// 1 - 1/p <= log10(n) < 1
			// Given that log10(10) is 1 and log10(n) < 1 we know n must be less than 10, so focusing on left side of
			// the inequality:
			// 1 - 1/p <= log10(n)
			// subtract 1 from both sides:
			// -1/p <= log10(n) - 1
			// multiply both sides by -1 (which flips the inequality):
			// 1/p >= 1 - log10(n)
			// inverting both sides gives us:
			// p >= 1 / (1 - log10(n))
			var sum = 0;
			for (var n = 1; n < 10; n++)
			{
				sum += (int)Math.Floor(1 / (1 - Math.Log10(n)));
			}

			return sum;
		}

		public int ExpectedSolution()
		{
			return 49;
		}
	}
}
