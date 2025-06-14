using System;
using System.Collections.Generic;
using System.Linq;
using ProjectEulerCSharp.EulerMath;

namespace ProjectEulerCSharp.EulerProblems._0060s
{
	[Euler(
			title:"Problem 60: Prime pair sets",
			description: @"The primes 3, 7, 109, and 673, are quite remarkable. By taking any two primes and concatenating them in any order the result will always be prime. For example, taking 7 and 109, both 7109 and 1097 are prime. The sum of these four primes, 792, represents the lowest sum for a set of four primes with this property.

Find the lowest sum for a set of five primes for which any two primes concatenate to produce another prime."
		)
	]
	// ReSharper disable once UnusedType.Global
	internal class Problem0060 : ISolution<int>
	{
		private const int MaxCalculatedPrime = 10_000_000;

		public bool HaveImplementedAnalyticSolution => false;

		public int BruteForceSolution()
		{
			var primeSet = Primes.PreCalculated(MaxCalculatedPrime);
			var primesInRange = primeSet.TakeWhile(p => p < 10_000).ToList();

			foreach (var prime in primesInRange)
			{
				foreach (var otherPrime in primesInRange.SkipWhile(p => p <= prime))
				{
					if (PrimeCheck(prime, otherPrime, primeSet))
					{
						var goodPrimes = new HashSet<int> { prime, otherPrime };
						foreach (var anotherPrime in primesInRange.SkipWhile(p => p <= otherPrime))
						{
							if (ConcatAllNumbersArePrime(goodPrimes, anotherPrime, primeSet))
							{
								goodPrimes.Add(anotherPrime);
								if (goodPrimes.Count == 5)
								{
									return goodPrimes.Sum();
								}
							}
						}
					}
				}
			}

			return -1; // if we get here, we didn't find a solution
		}

		private static bool ConcatAllNumbersArePrime(HashSet<int> primesToConcat, long primeToConcat, HashSet<int> primeSet)
		{
			foreach (var lhsPrime in primesToConcat)
			{
				if (!PrimeCheck(lhsPrime, primeToConcat, primeSet))
				{
					return false;
				}
			}
			return true;
		}

		private static bool PrimeCheck(long lhsPrim, long rhsPrime, HashSet<int> primeSet)
		{
			checked
			{
				var longOne = lhsPrim * (long)Math.Pow(10, rhsPrime.GetCountOfDigits()) + rhsPrime;
				if (longOne <= MaxCalculatedPrime)
				{
					if (!primeSet.Contains((int)longOne))
					{
						return false;
					}
				}
				else
				{
					// use an approximate primality test for numbers larger than MaxCalculatedPrime
					if (!Primes.IsProbablyPrime(longOne))
					{
						return false;
					}
				}
				var longTwo = rhsPrime * (int)Math.Pow(10, lhsPrim.GetCountOfDigits()) + lhsPrim;
				if (longTwo <= MaxCalculatedPrime)
				{
					return primeSet.Contains((int)longTwo);
				}
				// use an approximate primality test for numbers larger than MaxCalculatedPrime
				return Primes.IsProbablyPrime(longTwo);
			}
		}

		public int AnalyticSolution()
		{
			throw new NotImplementedException();
		}

		public int ExpectedSolution()
		{
			return 26_033;
		}
	}
}
