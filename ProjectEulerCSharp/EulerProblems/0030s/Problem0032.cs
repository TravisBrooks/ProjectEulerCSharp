using System;
using System.Collections.Generic;
using System.Linq;
using ProjectEulerCSharp.EulerMath;

namespace ProjectEulerCSharp.EulerProblems._0030s
{
    [Euler(
        title: "Problem 32: Pandigital products",
        description: @"We shall say that an n-digit number is pandigital if it makes use of all the digits 1 to n exactly once; for example, the 5-digit number, 15234, is 1 through 5 pandigital.

The product 7254 is unusual, as the identity, 39 × 186 = 7254, containing multiplicand, multiplier, and product is 1 through 9 pandigital.

Find the sum of all products whose multiplicand/multiplier/product identity can be written as a 1 through 9 pandigital.

HINT: Some products can be obtained in more than one way so be sure to only include it once in your sum.")
    ]
    // ReSharper disable once UnusedMember.Global
    public class Problem0032 : ISolution<int>
    {
        public bool HaveImplementedAnalyticSolution => false;

        public int BruteForceSolution()
        {
            var pandigitalProducts = new HashSet<int>();
            // the real trick to getting this solution to run quickly was figuring out good ranges for these loops, I just brute force figured out these numbers
            for (var lhs = 4; lhs <= 48; lhs++)
            {
                for (var rhs = 156; rhs <= 1965; rhs++)
                {
                    if (IsPandigitalProduct(lhs, rhs, out var product))
                    {
                        pandigitalProducts.Add(product);
                    }
                }
            }
            return pandigitalProducts.Sum();
        }

        public int AnalyticSolution()
        {
            throw new NotImplementedException();
        }

        private static bool IsPandigitalProduct(int lhs, int rhs, out int product)
        {
            product = lhs * rhs;
            // if we got an overflow then its too big
            if (product < 0)
            {
                return false;
            }

            var a = lhs.GetCountOfDigits();
            var b = rhs.GetCountOfDigits();
            var c = product.GetCountOfDigits();
            if (a + b + c != 9)
            {
                return false;
            }

            var lhsDigits = lhs.GetDigits();
            var rhsDigits = rhs.GetDigits();
            var productDigits = product.GetDigits();

            var allDigits = new int[9];
            lhsDigits.CopyTo(allDigits, 0);
            rhsDigits.CopyTo(allDigits, lhsDigits.Length);
            productDigits.CopyTo(allDigits, lhsDigits.Length + rhsDigits.Length);

            Array.Sort(allDigits);
            for (var i = 0; i < 9; i++)
            {
                if (allDigits[i] != i + 1)
                {
                    return false;
                }
            }

            return true;
        }

        public int ExpectedSolution()
        {
            return 45_228;
        }
    }
}
