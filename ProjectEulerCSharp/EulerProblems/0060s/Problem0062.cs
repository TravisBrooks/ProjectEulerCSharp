using System;
using System.Collections.Generic;
using System.Linq;
using ProjectEulerCSharp.EulerMath;

namespace ProjectEulerCSharp.EulerProblems._0060s
{
	[Euler(
			title: "Problem 62: Cubic permutations",
			description: @"The cube, 41063625 (345^3), can be permuted to produce two other cubes: 56623104 (384^3) and 66430125 (405^3). In fact, 345, 384, and 405 are the only three cubes that can be permuted to produce three other cubes.

Find the smallest cube for which exactly five permutations of its digits are cube."
		)
	]
	// ReSharper disable once UnusedType.Global
	internal class Problem0062 : ISolution<long>
	{
		public bool HaveImplementedAnalyticSolution => false;

		public long BruteForceSolution()
		{
			// started off with a million then lowered MaxNumberToCube to number that still gave right answer
			const int MaxNumberToCube = 10_000;
			var n = 2;
			var c = (long)Math.Pow(n, 3);
			var cubes = new List<long>();
			while (n <= MaxNumberToCube)
			{
				cubes.Add(c);
				n++;
				c = (long)Math.Pow(n, 3);
			}

			var lookup = cubes.ToLookup(i =>
			{
				// sort the digits in the number then convert to string, all permutation will have this same string key
				var digits = i.GetDigits().OrderBy(d => d);
				return string.Join(string.Empty, digits);
			});
			var candidateGroups = lookup.Where(g => g.Count() == 5).ToList();
			var groupMinimums = candidateGroups.Select(g => g.Min()).ToList();
			return groupMinimums.Min();
		}

		public long AnalyticSolution()
		{
			throw new NotImplementedException();
		}

		public long ExpectedSolution()
		{
			return 127_035_954_683;
		}
	}
}
