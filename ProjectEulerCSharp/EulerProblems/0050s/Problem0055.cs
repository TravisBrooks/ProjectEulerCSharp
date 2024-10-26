using System;
using ProjectEulerCSharp.EulerMath;

namespace ProjectEulerCSharp.EulerProblems._0050s
{
	[Euler(
		title: "Problem 55: Lychrel Numbers",
		description: @"If we take 47, reverse and add, 47+74=121, which is palindromic.

Not all numbers produce palindromes so quickly. For example,
    349 + 943 = 1292
    1292 + 2921 = 4213
    4213 + 3124 = 7337
 
That is, 349 took three iterations to arrive at a palindrome.

Although no one has proved it yet, it is thought that some numbers, like 196, never produce a palindrome. A number that never forms a palindrome through the reverse and add process is called a Lychrel number. Due to the theoretical nature of these numbers, and for the purpose of this problem, we shall assume that a number is Lychrel until proven otherwise. In addition you are given that for every number below ten-thousand, it will either (i) become a palindrome in less than fifty iterations, or, (ii) no one, with all the computing power that exists, has managed so far to map it to a palindrome. In fact, 10677 is the first number to be shown to require over fifty iterations before producing a palindrome: 4668731596684224866951378664 (53 iterations, 28-digits).

Surprisingly, there are palindromic numbers that are themselves Lychrel numbers; the first example is 4994.

How many Lychrel numbers are there below ten-thousand?

NOTE: Wording was modified slightly on 24 April 2007 to emphasise the theoretical nature of Lychrel numbers."
		)
	]
	// ReSharper disable once UnusedType.Global
	public class Problem0055 : ISolution<int>
	{
		public bool HaveImplementedAnalyticSolution => false;

		public int BruteForceSolution()
		{
			var lychrelCnt = 0;
			for (var n = 1L; n < 10_000L; n++)
			{
				if (IsLychrel(n))
				{
					lychrelCnt++;
				}
			}

			return lychrelCnt;
		}

		private static bool IsLychrel(long n)
		{
			// Having an ExtensionMethods.Reverse that worked quickly over longs, avoiding string conversion, was the trick to getting this
			// brute force IsLychrel check to work fast. TODO: this gives right answer, but the addition can do an overflow. Would be nice if ExtensionMethods used generic math so I could try UL...
			var newN = n;
			var revNewN = newN.Reverse();
			for (var i = 1; i < 50; i++)
			{
				var maybe = newN + revNewN;
				var revMaybe = maybe.Reverse();
				if (maybe == revMaybe)
				{
					// it's a palindrome, so not Lychrel
					return false;
				}
				newN = maybe;
				revNewN = revMaybe;
			}
			return true;
		}

		public int AnalyticSolution()
		{
			throw new NotImplementedException();
		}

		public int ExpectedSolution()
		{
			return 249;
		}
	}
}
