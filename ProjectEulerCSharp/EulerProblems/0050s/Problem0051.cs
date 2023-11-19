using System;
using System.Collections.Generic;
using System.Linq;
using ProjectEulerCSharp.EulerMath;

namespace ProjectEulerCSharp.EulerProblems._0050s
{
    [Euler(
        title: "Problem 51: Prime Digit Replacements",
        description: @"By replacing the 1st digit of the 2-digit number *3, it turns out that six of the nine possible values: 13, 23, 43, 53, 73, and 83, are all prime.

By replacing the 3rd and 4th digits of 56**3 with the same digit, this 5-digit number is the first example having seven primes among the ten generated numbers, yielding the family: 56003, 56113, 56333, 56443, 56663, 56773, and 56993. Consequently 56003, being the first member of this family, is the smallest prime with this property.

Find the smallest prime which, by replacing part of the number (not necessarily adjacent digits) with the same digit, is part of an eight prime value family.")
    ]
    // ReSharper disable once UnusedType.Global
    public class Problem0051 : ISolution<int>
    {
        public bool HaveImplementedAnalyticSolution => false;

        public int BruteForceSolution()
        {
            var primeSet = Primes.PreCalculated(1_000_000);
            // We're looking for permutations of a pattern like mnp***.
            // If I didn't use strings this would probably be much faster, but its reasonably fast and pretty straightforward.
            // I'm also cheating a bit here, through trial and error I figured out the pattern should be 3 digits and 3 wildcards,
            // it would be a much bigger and slower solution if I went through other possibilities.
            for (var threeDigitNumber = 100; threeDigitNumber <= 999; threeDigitNumber++)
            {
                var charsToPermute = (threeDigitNumber + "***").ToCharArray();
                var permutationsOfPattern = Permutations.Of(charsToPermute, PermutationAlgorithm.QuickPerm03);
                
                foreach (var patternPermute in permutationsOfPattern)
                {
                    var patternStr = new string(patternPermute);
                    var foundPrimes = new HashSet<int>();
                    for (var c = '0'; c <= '9'; c++)
                    {
                        var maybePrime = int.Parse(patternStr.Replace('*', c));
                        if (primeSet.Contains(maybePrime) && maybePrime.GetCountOfDigits() == 6)
                        {
                            foundPrimes.Add(maybePrime);
                        }
                    }

                    if (foundPrimes.Count == 8)
                    {
                        return foundPrimes.Min();
                    }
                }
                
            }

            // didn't find it...
            return -1;
        }

        public int AnalyticSolution()
        {
            throw new NotImplementedException();
        }

        public int ExpectedSolution()
        {
            return 121_313;
        }
    }
}
