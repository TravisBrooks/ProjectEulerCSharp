﻿using System.Linq;
using ProjectEulerCSharp.EulerMath;

namespace ProjectEulerCSharp.EulerProblems
{
    [Euler(
        title: "Problem 2: Even Fibonacci numbers",
        description: @"Each new term in the Fibonacci sequence is generated by adding the previous two terms. By starting with 1 and 2, the first 10 terms will be:

1, 2, 3, 5, 8, 13, 21, 34, 55, 89, ...

By considering the terms in the Fibonacci sequence whose values do not exceed four million, find the sum of the even-valued terms.")
    ]
    // ReSharper disable once UnusedMember.Global
    public class Problem0002 : ISolution<long>
    {
        /****************************************************************************************************
         * Even Fibonacci numbers
         *
         * Each new term in the Fibonacci sequence is generated by adding the previous two terms. By starting with 1 and 2, the first 10 terms will be:
         *      1, 2, 3, 5, 8, 13, 21, 34, 55, 89, ...
         * By considering the terms in the Fibonacci sequence whose values do not exceed four million, find the sum of the even-valued terms.
         ****************************************************************************************************/
        public long BruteForceSolution()
        {
            return Fibonacci.Sequence()
                .TakeWhile(n => n < 4_000_000)
                .Where(n => n % 2 == 0)
                .Sum(bi => long.Parse(bi.ToString()));
        }

        public bool HaveImplementedAnalyticSolution => true;

        public long AnalyticSolution()
        {
            /****************************************************************************************************
             * The way that the Fibonacci sequence is presented for this problem doesn't lend itself to this
             * analytic solution. If I lay out the sequence more traditionally the pattern is more obvious between
             * the evens vs odds:
             *      sequence:       0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, ...
             *      even or odd:    E  O  O  E  O  O  E   O   O   E   O   O    E  ...
             * The pattern emerging is every 3rd fib is even. This makes total sense as the sequence is made of the
             * sum of its 2 prior elements, and that when you add an Odd and Odd you always get an Even. The
             * recursive formula for Fib(n) is given as:
             *  Fib(n) = Fib(n-1) + Fib(n-2)
             * if Fib(n) is even then we know that Fib(n-1) and Fib(n-2) are odd and Fib(n-3) is even. So the 2
             * evens prior to and even Fib(n) are Fib(n-3) and Fib(n-6). The trick is to express the original
             * definition of Fib(n) in terms of Fib(n-3) and Fib(n-6):
             *      Fib(n) = Fib(n-1) + Fib(n-2)
             *             = (Fib(n-2) + Fib(n-3)) + (Fib(n-3) + Fib(n-4)) // note Fib(n-3) is listed twice
             *             = 2Fib(n-3) + Fib(n-2) + Fib(n-4)
             *             = 2Fib(n-3) + (Fib(n-3) + Fib(n-4)) + (Fib(n-5) + Fib(n-6))
             *             = 3Fib(n-3) + Fib(n-4) + Fib(n-5) + Fib(n-6)
             *               ... note that Fib(n-4) + Fib(n-5) is Fib(n-3), so:
             *             = 4Fib(n-3) + Fib(n-6)
             * So now we have a definition of the Fibonacci sequence defined only in terms of the even members of
             * the sequence, so given a seed of the first 2 even members of the sequence we can compute all even
             * members of the sequence.
             ****************************************************************************************************/

            long fibNMinus3 = 2;
            long fibNMinus6 = 0;
            var total = fibNMinus3 + fibNMinus6;

            while (fibNMinus3 < 4_000_000)
            {
                var fibN = 4L * fibNMinus3 + fibNMinus6;
                if (fibN > 4_000_000)
                {
                    break;
                }

                fibNMinus6 = fibNMinus3;
                fibNMinus3 = fibN;
                total += fibN;
            }

            return total;
        }

        public long ExpectedSolution()
        {
            return 4613732;
        }
    }
}