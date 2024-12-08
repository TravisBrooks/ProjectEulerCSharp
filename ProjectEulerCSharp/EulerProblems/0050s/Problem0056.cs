using System;
using System.Linq;
using System.Numerics;
using ProjectEulerCSharp.EulerMath;

namespace ProjectEulerCSharp.EulerProblems._0050s
{
	[Euler(
			title: "Problem 56: Powerful Digit Sum",
			description: @"A googol (10^1000) is a massive number: one followed by one-hundred zeros; 100^100 is almost unimaginably large: one followed by two-hundred zeros. Despite their size, the sum of the digits in each number is only 1.

Considering natural numbers of the form, a^b, where a,b ≤ 99, what is the maximum digital sum?")
	]
	// ReSharper disable once UnusedType.Global
	public class Problem0056 : ISolution<int>
	{
		public bool HaveImplementedAnalyticSolution => false;

		public int BruteForceSolution()
		{
			var maxSum = 0;
			// The only way I saw to speed up the brute force was to do fewer loops so they both start at 95
			for (var a = 95; a < 100; a++)
			{
				for (var b = 95; b < 100; b++)
				{
					var n = BigInteger.Pow(a, b);
					var digitSum = n.GetDigits().Sum();
					if (digitSum > maxSum)
					{
						maxSum = digitSum;
					}
				}
			}

			return maxSum;
		}

		public int AnalyticSolution()
		{
			throw new NotImplementedException();
		}

		public int ExpectedSolution()
		{
			return 972;
		}
	}
}
