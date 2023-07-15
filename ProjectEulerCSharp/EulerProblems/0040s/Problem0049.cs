using System;
using System.Collections.Generic;
using System.Linq;
using ProjectEulerCSharp.EulerMath;

namespace ProjectEulerCSharp.EulerProblems._0040s
{
    [Euler(
        title: "Problem 49: Prime Permutations",
        description: @"The arithmetic sequence, 1487, 4817, 8147, in which each of the terms increases by 3330, is unusual in two ways: (i) each of the three terms are prime, and, (ii) each of the 4-digit numbers are permutations of one another.

There are no arithmetic sequences made up of three 1-, 2-, or 3-digit primes, exhibiting this property, but there is one other 4-digit increasing sequence.

What 12-digit number do you form by concatenating the three terms in this sequence?")
    ]
    // ReSharper disable once UnusedMember.Global
    public class Problem0049 : ISolution<long>
    {
        public bool HaveImplementedAnalyticSolution => false;

        public long BruteForceSolution()
        {
            var primes = Primes.Calculate(9999).SkipWhile(p => p < 1000).ToList();
            var primeSet = new HashSet<int>(primes);
            const int offset = 3330;
            foreach (var p1 in primes)
            {
                if (p1 == 1487)
                {
                    continue;
                }
                var p2 = p1 + offset;
                var p3 = p2 + offset;
                if (primeSet.Contains(p2) && primeSet.Contains(p3))
                {
                    // If we got this far we know all 3 numbers satisfy the 3330 offset rule while all being prime, but we still
                    // need to verify they are all permutations of each other.
                    var p1Digits = p1.GetDigits();
                    Array.Sort(p1Digits);
                    var p2Digits = p2.GetDigits();
                    Array.Sort(p2Digits);
                    if (p1Digits.SequenceEqual(p2Digits))
                    {
                        var p3Digits = p3.GetDigits();
                        Array.Sort(p3Digits);
                        if (p1Digits.SequenceEqual(p3Digits))
                        {
                            long answer = p1;
                            answer *= 1_0000_0000;
                            answer += (p2 * 10000) + p3;
                            return answer;
                        }
                    }
                }
            }
            return -1;
        }

        public long AnalyticSolution()
        {
            throw new NotImplementedException();
        }

        public long ExpectedSolution()
        {
            return 2969_6299_9629;
        }
    }
}
