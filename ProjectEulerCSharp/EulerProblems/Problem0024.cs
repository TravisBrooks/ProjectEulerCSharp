using System.Collections.Generic;
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
            var answerArr = Permutations.Of(digits, PermutationAlgorithm.Lexicographic).Take(1_000_000).Last();
            var answerStr = string.Join(string.Empty, answerArr);
            var answer = long.Parse(answerStr);
            return answer;
        }

        public long AnalyticSolution()
        {
            // see https://en.wikipedia.org/wiki/Factorial_number_system
            // The factorial number has a bit of a tedious writeup on wikipedia but the concept is head slappingly
            // simple. When you pick the first digit from the digit pool there are 9! choices, the next digit there
            // are 8! etc. Its just converting a number from one base to another, but in this case the base is in 
            // factorials.
            var digits = new [] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var n = digits.Length;
            long kth = 1_000_000 - 1;
            var answerDigits = new List<int>();
            // converting the kth number to its factoradic representation (Lehmer code) gives the kth permutation
            for (var i = n - 1; i > 0; i--)
            {
                var fact = long.Parse(Factorial.Of(i).ToString());
                var index = kth / fact;
                var remainder = kth - (fact * index);
                var answerDigit = digits[index];
                answerDigits.Add(answerDigit);
                digits = digits.Where(d => d != answerDigit).ToArray();
                kth = remainder;
            }

            if (digits.Any())
            {
                answerDigits.AddRange(digits);
            }
            var answerStr = string.Join(string.Empty, answerDigits);
            return long.Parse(answerStr);
        }

        public long ExpectedSolution()
        {
            return 2_783_915_460;
        }
    }
}