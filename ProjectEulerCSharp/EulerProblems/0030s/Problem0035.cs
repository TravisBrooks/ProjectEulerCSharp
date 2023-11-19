using ProjectEulerCSharp.EulerMath;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEulerCSharp.EulerProblems._0030s
{
    [Euler(
        title: "Problem 35: Circular primes",
        description: @"The number, 197, is called a circular prime because all rotations of the digits: 197, 971, and 719, are themselves prime.

There are thirteen such primes below 100: 2, 3, 5, 7, 11, 13, 17, 31, 37, 71, 73, 79, and 97.

How many circular primes are there below one million?")
    ]
    // ReSharper disable once UnusedType.Global
    public class Problem0035 : ISolution<int>
    {
        public bool HaveImplementedAnalyticSolution => false;

        public int BruteForceSolution()
        {
            var count = 13;
            var upperBounds = 1_000_000;
            var primes = Primes.PreCalculated(upperBounds);
            
            for (var n = 100; n < upperBounds; n++)
            {
                if(primes.Contains(n))
                {
                    var digits = n.GetDigits();
                    var rotations = RotateDigits(digits);
                    var allPrime = true;
                    foreach (var rotation in rotations)
                    {
                        var fromDigits = FromDigits(rotation);
                        if (!primes.Contains(fromDigits))
                        {
                            allPrime = false;
                            break;
                        }
                    }

                    if (allPrime)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private static IEnumerable<int[]> RotateDigits(int[] digits)
        {
            if (digits.Length <= 1)
            {
                yield break;
            }
            var queue = new Queue<int>(digits);
            var stack = new Stack<int>();
            // we don't return the input as 1 of the rotations, for this problem its redundant
            for (var i = 1; i < digits.Length; i++)
            {
                stack.Push(queue.Dequeue());
                queue.Enqueue(stack.Pop());
                yield return queue.ToArray();
            }
        }

        private static readonly int[] PowersOf10 = { 1, 10, 100, 1000, 10_000, 100_000, 1_000_000 };
        private static int FromDigits(int[] digits)
        {
            var sum = 0;
            var i = 0;
            foreach (var d in digits.Reverse())
            {
                sum += PowersOf10[i] * d;
                i++;
            }

            return sum;
        }

        public int AnalyticSolution()
        {
            throw new NotImplementedException();
        }

        public int ExpectedSolution()
        {
            return 55;
        }
    }
}
