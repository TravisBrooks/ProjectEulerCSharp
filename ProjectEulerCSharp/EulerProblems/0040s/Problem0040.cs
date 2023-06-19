using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectEulerCSharp.EulerMath;

namespace ProjectEulerCSharp.EulerProblems._0040s
{
    [Euler(
            title: "Problem 40: Champernowne constant",
            description: @"An irrational decimal fraction is created by concatenating the positive integers:

                0.123456789101112131415161718192021...

It can be seen that the 12th digit of the fractional part is 1.

If dn represents the nth digit of the fractional part, find the value of the following expression.

                d1 × d10 × d100 × d1000 × d10000 × d100000 × d1000000")
    ]
    // ReSharper disable once UnusedMember.Global
    public class Problem0040 : ISolution<int>
    {
        public bool HaveImplementedAnalyticSolution => true;

        public int BruteForceSolution()
        {
            // this alg is very dumb, but surprisingly fast
            const int upperBound = 1_000_000;
            var sb = new StringBuilder(upperBound);
            var i = 0;
            while (sb.Length < upperBound)
            {
                sb.Append(++i);
            }

            var n = 1;
            var m = 1;
            while (n < upperBound)
            {
                var digit = NthChampernowneDigit(n);
                m *= digit;
                n *= 10;
            }

            int NthChampernowneDigit(int nth)
            {
                return sb[nth - 1].ToIntFast();
            }

            return m;
        }

        public int AnalyticSolution()
        {
            // I tried solving this problem with pencil and paper and came up with this table to keep track of the digits and numbers.
            // Its still sort of a brute force solution, but a faster one.
            var champTable = new List<ChampRow>
            {
	            new (1,      1,      9,      9 * 1 * 1,                                                               1,                                                                            9),
	            new (2,     10,     99,     9 * 10 * 2,                                                           9 + 1,                                                               9 + 9 * 10 * 2),
	            new (3,    100,    999,    9 * 100 * 3,                                              9 + 9 * 10 * 2 + 1,                                                 9 + 9 * 10 * 2 + 9 * 100 * 3),
	            new (4,   1000,   9999,   9 * 1000 * 4,                                9 + 9 * 10 * 2 + 9 * 100 * 3 + 1,                                  9 + 9 * 10 * 2 + 9 * 100 * 3 + 9 * 1000 * 4),
	            new (5,  10000,  99999,  9 * 10000 * 5,                 9 + 9 * 10 * 2 + 9 * 100 * 3 + 9 * 1000 * 4 + 1,                  9 + 9 * 10 * 2 + 9 * 100 * 3 + 9 * 1000 * 4 + 9 * 10000 * 5),
	            new (6, 100000, 999999, 9 * 100000 * 6, 9 + 9 * 10 * 2 + 9 * 100 * 3 + 9 * 1000 * 4 + 9 * 10000 * 5 + 1, 9 + 9 * 10 * 2 + 9 * 100 * 3 + 9 * 1000 * 4 + 9 * 10000 * 5 + 9 * 100000 * 6),
            };

            // d1 × d10 × d100 × d1000 × d10000 × d100000 × d1000000
            // should be obvious d1 is 1 and d10 is first digit of 10 is 1 so we can ignore those 2
            var ns = new[] { 100, 1000, 10_000, 100_000, 1_000_000 };
            var answer = 1;
            foreach (var n in ns)
            {
                answer *= FindNthDigit(champTable, n);
            }

            return answer;
        }

        private int FindNthDigit(IEnumerable<ChampRow> champTable, int n)
        {
            var row = champTable.Single(r => r.DigitStart <= n && r.DigitEnd >= n);
            var howManyDigitsInRow = n - row.DigitStart + 1;
            var offsetNumber = howManyDigitsInRow / row.Digits;
            var digitOffset = (howManyDigitsInRow % row.Digits) - 1;
            var number = row.StartRange + offsetNumber;
            var digit = number.GetDigits()[digitOffset];
            return digit;
        }

        public int ExpectedSolution()
        {
            return 210;
        }

        private record ChampRow(int Digits, int StartRange, int EndRange, int DigitCount, int DigitStart, int DigitEnd);
    }
}
