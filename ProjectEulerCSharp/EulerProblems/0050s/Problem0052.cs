using System;
using System.Linq;

namespace ProjectEulerCSharp.EulerProblems._0050s
{
    [Euler(
            title: "Problem 52: Permuted Multiples",
            description: @"It can be seen that the number, 125874, and its double, 251748, contain exactly the same digits, but in a different order.

Find the smallest positive integer, x, such that 2x, 3x, 4x, 5x, and 6x, contain the same digits."
        )
    ]
    // ReSharper disable once UnusedType.Global
    public class Problem0052 : ISolution<int>
    {
        public bool HaveImplementedAnalyticSolution => false;

        public int BruteForceSolution()
        {
            for (var i = 100_000; i < 1_000_000; i++)
            {
                var digits1 = DigitCount(i);
                if (!digits1.SequenceEqual(DigitCount(i * 2)) ||
                    !digits1.SequenceEqual(DigitCount(i * 3)) ||
                    !digits1.SequenceEqual(DigitCount(i * 4)) ||
                    !digits1.SequenceEqual(DigitCount(i * 5)) ||
                    !digits1.SequenceEqual(DigitCount(i * 6)))
                {
                    continue;
                }
                return i;
            }
            return -1;
        }

        private int[] DigitCount(int number)
        {
            var counts = new int[10];
            while (number > 0)
            {
                counts[number % 10]++;
                number /= 10;
            }
            return counts;
        }

        public int AnalyticSolution()
        {
            throw new NotImplementedException();
        }

        public int ExpectedSolution()
        {
            return 142_857;
        }
    }
}
