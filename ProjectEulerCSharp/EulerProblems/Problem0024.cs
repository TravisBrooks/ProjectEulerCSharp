using System.Linq;
using ProjectEulerCSharp.EulerMath;

namespace ProjectEulerCSharp.EulerProblems
{
    [Euler(
        title: "Problem 24: Lexicographic permutations",
        description: @"A permutation is an ordered arrangement of objects. For example, 3124 is one possible permutation of the digits 1, 2, 3 and 4. If all of the permutations are listed numerically or alphabetically, we call it lexicographic order. The lexicographic permutations of 0, 1 and 2 are:

012   021   102   120   201   210

What is the millionth lexicographic permutation of the digits 0, 1, 2, 3, 4, 5, 6, 7, 8 and 9?")
    ]
    // ReSharper disable once UnusedMember.Global
    public class Problem0024 : ISolution<long>
    {
        public bool HaveImplementedAnalyticSolution => true;

        public long BruteForceSolution()
        {
            var digits = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var numberCount = 0;
            long answer = 0;
            foreach (var d0 in digits)
            {
                foreach (var d1 in digits.Except(new[] { d0 }))
                {
                    foreach (var d2 in digits.Except(new[] { d0, d1 }))
                    {
                        foreach (var d3 in digits.Except(new[] { d0, d1, d2 }))
                        {
                            foreach (var d4 in digits.Except(new[] { d0, d1, d2, d3 }))
                            {
                                foreach (var d5 in digits.Except(new[] { d0, d1, d2, d3, d4 }))
                                {
                                    foreach (var d6 in digits.Except(new[] { d0, d1, d2, d3, d4, d5 }))
                                    {
                                        foreach (var d7 in digits.Except(new[] { d0, d1, d2, d3, d4, d5, d6 }))
                                        {
                                            foreach (var d8 in digits.Except(new[] { d0, d1, d2, d3, d4, d5, d6, d7 }))
                                            {
                                                foreach (var d9 in digits.Except(new[] { d0, d1, d2, d3, d4, d5, d6, d7, d8 }))
                                                {
                                                    numberCount++;
                                                    if (numberCount >= 1000000)
                                                    {
                                                        string numStr = string.Empty + d0 + d1 + d2 + d3 + d4 + d5 + d6 + d7 + d8 + d9;
                                                        answer = long.Parse(numStr);
                                                        goto FoundIt;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            FoundIt:
            return answer;
        }

        public long AnalyticSolution()
        {
            var digits = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var answerArr = Permutations.Of(digits, PermutationAlgorithm.Lexicographic).Take(1_000_000).Last();
            var answerStr = string.Join(string.Empty, answerArr);
            var answer = long.Parse(answerStr);
            return answer;
        }

        public long ExpectedSolution()
        {
            return 2_783_915_460;
        }
    }
}