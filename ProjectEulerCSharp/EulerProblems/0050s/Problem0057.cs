using System;
using System.Numerics;
using ProjectEulerCSharp.EulerMath;

namespace ProjectEulerCSharp.EulerProblems._0050s
{
	[Euler(
			title: "Problem 57: Square root convergents",
			description: "It is possible to show that the square root of two can be expressed as an infinite continued fraction."
		)
	]
	// ReSharper disable once UnusedType.Global
	public class Problem0057 : ISolution<int>
	{
		public bool HaveImplementedAnalyticSolution => false;

		public int BruteForceSolution()
		{
			var fraction = new Fraction(3, 2);
			var cnt = 0;
			for (var i = 1; i < 1000; i++)
			{
				if (fraction.NumeratorMoreDigits())
				{
					cnt++;
				}
				// I noticed this pattern in the fractions 3/2, 7/5, 17/12, 41/29
				var x = fraction.Numerator + fraction.Denominator;
				fraction = new Fraction(x + fraction.Denominator, x);
			}

			return cnt;
		}

		public int AnalyticSolution()
		{
			throw new NotImplementedException();
		}

		public int ExpectedSolution()
		{
			return 153;
		}

		private record Fraction(BigInteger Numerator, BigInteger Denominator)
		{
			public bool NumeratorMoreDigits()
			{
				var nDigits = Numerator.GetCountOfDigits();
				var dDigits = Denominator.GetCountOfDigits();
				return nDigits > dDigits;
			}
		}
	}
}
