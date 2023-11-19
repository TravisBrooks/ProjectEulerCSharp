using System;
using System.Collections.Generic;
using System.Linq;
using ProjectEulerCSharp.EulerMath;

namespace ProjectEulerCSharp.EulerProblems._0030s
{
    [Euler(
        title: "Problem 38: Pandigital multiples",
        description: @"Take the number 192 and multiply it by each of 1, 2, and 3:

    192 × 1 = 192
    192 × 2 = 384
    192 × 3 = 576
By concatenating each product we get the 1 to 9 pandigital, 192384576. We will call 192384576 the concatenated product of 192 and (1,2,3)

The same can be achieved by starting with 9 and multiplying by 1, 2, 3, 4, and 5, giving the pandigital, 918273645, which is the concatenated product of 9 and (1,2,3,4,5).

What is the largest 1 to 9 pandigital 9-digit number that can be formed as the concatenated product of an integer with (1,2, ... , n) where n greater than 1?")
    ]
    // ReSharper disable once UnusedType.Global
    public class Problem0038 : ISolution<long>
    {
        public bool HaveImplementedAnalyticSolution => false;

        public long BruteForceSolution()
        {
            long max = 0;
            // picked 10k as upper bound because the smallest valid concat product is 1_000_020_000 which is over 9 digits
            for (var n = 1; n < 10_000; n++)
            {
                // took a guess that there would be at most 5 multipliers
                for (var m=2; m <= 5; m++)
                {
                    var multipliers = Enumerable.Range(1, m);
                    var candidate = ConcatenatedProduct(n, multipliers);
                    if (candidate.GetCountOfDigits() > 9)
                    {
                        // if candidate is already too large then don't test even larger multipliers on n
                        break;
                    }

                    if (IsPandigital(candidate) && candidate > max)
                    {
                        max = candidate;
                    }
                }
            }
            return max;
        }

        private static long ConcatenatedProduct(long someNumber, IEnumerable<int> multipliers)
        {
            // This could be a linq one liner, but or speed purposes its done by hand
            var someNumbers = new long[multipliers.Count()];
            var i = 0;
            foreach (var m in multipliers)
            {
                someNumbers[i] = someNumber * m;
                i++;
            }
            return Concatenate(someNumbers);
        }

        private static long Concatenate(IEnumerable<long> someNumbers)
        {
            var power = 0;
            long concatenatedNumber = 0;
            foreach (var number in someNumbers.Reverse())
            {
                concatenatedNumber += (number * (long)Math.Pow(10, power));
                power += number.GetCountOfDigits();
            }

            return concatenatedNumber;
        }

        private static bool IsPandigital(long someNumber)
        {
            var digits = someNumber.GetDigits();
            if (digits.Length != 9)
            {
                return false;
            }

            var panDigits = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Array.Sort(digits);
            for (var i = 0; i < 9; i++) 
            {
                if (panDigits[i] != digits[i])
                {
                    return false;
                }
            }
            return true;
        }

        public long AnalyticSolution()
        {
            throw new NotImplementedException();
        }

        public long ExpectedSolution()
        {
            return 932_718_654;
        }
    }
}
