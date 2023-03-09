using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEulerCSharp.EulerMath
{
    /// <summary>
    /// A class to represent a rational number. Will slowly fill this class in as needs arise in Euler problems
    /// </summary>
    public class Fraction
    {
        private readonly Lazy<decimal> _approximateDecimalValue;

        public Fraction(int numerator, int denominator, FractionSign sign = FractionSign.Positive)
        {
            if (denominator == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(denominator), "The denominator cannot be 0");
            }
            if (sign != FractionSign.Negative && sign != FractionSign.Positive)
            {
                throw new ArgumentOutOfRangeException(nameof(sign), $"The sign value was not allowed: {sign}");
            }
            Numerator = numerator;
            Denominator = denominator;
            Sign = sign;
            _approximateDecimalValue = new Lazy<decimal>(_ApproximateDecimalImpl);
        }

        public int Numerator { get; }
        public int Denominator { get; }
        public FractionSign Sign { get; }

        public decimal ApproximateDecimal => _approximateDecimalValue.Value;

        /// <summary>
        /// </summary>
        /// <returns>Either the digits in the recurring cycle or empty if there isn't one</returns>
        public int[] RecurringCycle()
        {
            // Solves #26, no guarantees this method is correct or useful for anything except solving #26
            // For #26 numerator is always smaller than denominator so that is the only case getting solved
            var recurring = new List<(int Numerator, int Quotient)>();
            var n = Numerator;
            var d = Denominator;
            var quotient = 0;
            var foundCycle = false;
            var keepGoing = true;
            while (keepGoing)
            {
                while (n < d)
                {
                    n *= 10;
                }
                quotient = n / d;
                var remainder = n % d;
                if (remainder == 0)
                {
                    keepGoing = false;
                }
                else
                {
                    if (recurring.Any(tpl => tpl.Numerator == n && tpl.Quotient == quotient))
                    {
                        foundCycle = true;
                        keepGoing = false;
                        continue;
                    }
                    recurring.Add((n, quotient));
                    n = remainder;
                }
            }

            return foundCycle ? 
                recurring.SkipWhile(tpl => tpl.Numerator != n && tpl.Quotient != quotient)
                    .Select(tpl => tpl.Quotient)
                    .ToArray() 
                : new int[]{};
        }

        private decimal _ApproximateDecimalImpl()
        {
            decimal n = Numerator;
            decimal d = Denominator;
            return Sign == FractionSign.Negative ? -(n / d) : n / d;
        }
    }
}
