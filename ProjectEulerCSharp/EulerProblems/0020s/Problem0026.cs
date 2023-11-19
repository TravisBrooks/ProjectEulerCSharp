using System;
using System.Linq;
using ProjectEulerCSharp.EulerMath;

namespace ProjectEulerCSharp.EulerProblems._0020s
{
    [Euler(
        title: "Problem 26: Reciprocal cycles",
        description: @"A unit fraction contains 1 in the numerator. The decimal representation of the unit fractions with denominators 2 to 10 are given:

    1/2 = 0.5
    1/3 = 0.(3)
    1/4 = 0.25
    1/5 = 0.2
    1/6 = 0.1(6)
    1/7 = 0.(142857)
    1/8 = 0.125
    1/9 = 0.(1)
    1/10= 0.1
Where 0.1(6) means 0.166666..., and has a 1-digit recurring cycle. It can be seen that 1/7 has a 6-digit recurring cycle.

Find the value of d less-than 1000 for which 1/d contains the longest recurring cycle in its decimal fraction part.")
    ]
    // ReSharper disable once UnusedType.Global
    public class Problem0026 : ISolution<int>
    {
        public bool HaveImplementedAnalyticSolution => false;

        public int BruteForceSolution()
        {
            var fractions = Enumerable.Range(2, 998).Select(d => new Fraction(1, d));
            var answer = fractions.Select(f => (f.Denominator, f.RecurringCycle()))
                .MaxBy(tpl => tpl.Item2.Length)
                .Denominator;
            return answer;
        }

        public int AnalyticSolution()
        {
            throw new NotImplementedException();
        }

        public int ExpectedSolution()
        {
            return 983;
        }
    }
}
