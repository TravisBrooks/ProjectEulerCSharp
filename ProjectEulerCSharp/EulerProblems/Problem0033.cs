using System;
using ProjectEulerCSharp.EulerMath;

namespace ProjectEulerCSharp.EulerProblems
{
    [Euler(
        title: "Problem 33: Digit cancelling fractions",
        description: @"The fraction 49/98 is a curious fraction, as an inexperienced mathematician in attempting to simplify it may incorrectly believe that 49/98 = 4/8, which is correct, is obtained by cancelling the 9s.

We shall consider fractions like, 30/50 = 3/5, to be trivial examples.

There are exactly four non-trivial examples of this type of fraction, less than one in value, and containing two digits in the numerator and denominator.

If the product of these four fractions is given in its lowest common terms, find the value of the denominator.")
    ]
    // ReSharper disable once UnusedMember.Global
    public class Problem0033 : ISolution<int>
    {
        public bool HaveImplementedAnalyticSolution => false;

        public int BruteForceSolution()
        {
            Fraction fractionProduct = null;
            for (var n = 10; n <= 98; n++)
            {
                for (var d = n+1; d<=99; d++)
                {
                    // these would be the "trivial examples"
                    if (n % 10 == 0 || d % 10 == 0)
                    {
                        continue;
                    }
                    var nDigits = n.GetDigits();
                    var dDigits = d.GetDigits();
                    if (nDigits[0] == dDigits[0])
                    {
                        fractionProduct = _FractionProduct(n, d, nDigits[1], dDigits[1], fractionProduct);
                    }
                    else if (nDigits[0] == dDigits[1])
                    {
                        fractionProduct = _FractionProduct(n, d, nDigits[1], dDigits[0], fractionProduct);
                    }
                    else if (nDigits[1] == dDigits[0])
                    {
                        fractionProduct = _FractionProduct(n, d, nDigits[0], dDigits[1], fractionProduct);
                    }
                    else if (nDigits[1] == dDigits[1])
                    {
                        fractionProduct = _FractionProduct(n, d, nDigits[0], dDigits[0], fractionProduct);
                    }
                }
            }

            return fractionProduct?.Denominator ?? 0;
        }

        private static Fraction _FractionProduct(int n, int d, int canceledN, int canceledD, Fraction productAccumulator)
        {
            var f1 = new Fraction(n, d);
            var f2 = new Fraction(canceledN, canceledD);
            if (f1.Equivalent(f2))
            {
                if (productAccumulator == null)
                {
                    productAccumulator = f1;
                }
                else
                {
                    productAccumulator *= f1;
                }
            }

            return productAccumulator;
        }

        public int AnalyticSolution()
        {
            throw new NotImplementedException();
        }

        public int ExpectedSolution()
        {
            return 100;
        }
    }
}
