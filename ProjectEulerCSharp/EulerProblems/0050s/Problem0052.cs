using System;
using System.Linq;
using ProjectEulerCSharp.EulerMath;

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
            for (var i = 1; i <= 1_000_000; i++)
            {
                var digits1 = i.GetDigits();
                var digits2 = (i * 2).GetDigits();
                if (digits1.Length != digits2.Length)
                {
                    continue;
                }
                var digits3 = (i * 3).GetDigits();
                if (digits1.Length != digits3.Length)
                {
                    continue;
                }
                var digits4 = (i * 4).GetDigits();
                if (digits1.Length != digits4.Length)
                {
                    continue;
                }
                var digits5 = (i * 5).GetDigits();
                if (digits1.Length != digits5.Length)
                {
                    continue;
                }
                var digits6 = (i * 6).GetDigits();
                if (digits1.Length != digits6.Length)
                {
                    continue;
                }
                Array.Sort(digits1);
                Array.Sort(digits2);
                if (!digits1.SequenceEqual(digits2))
                {
                    continue;
                }
                Array.Sort(digits3);
                if (!digits1.SequenceEqual(digits3))
                {
                    continue;
                }
                Array.Sort(digits4);
                if (!digits1.SequenceEqual(digits4))
                {
                    continue;
                }
                Array.Sort(digits5);
                if (!digits1.SequenceEqual(digits5))
                {
                    continue;
                }
                Array.Sort(digits6);
                if (!digits1.SequenceEqual(digits6))
                {
                    continue;
                }

                return i;
            }

            return -1;
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
