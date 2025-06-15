using System;
using System.Linq;
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
		public bool HaveImplementedAnalyticSolution => false;

		public int BruteForceSolution()
		{
			var cnt = 0;
			for(var p = 1; p <= 25; p++)
			{
				for(var n = 1; n <= 9; n++)
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
			throw new NotImplementedException();
		}

		public int ExpectedSolution()
		{
			return 49;
		}
	}
}
