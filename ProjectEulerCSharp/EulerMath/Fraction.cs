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
        private readonly Lazy<int> _gcd;

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
            Numerator = Math.Abs(numerator);
            Denominator = Math.Abs(denominator);
            Sign = sign;
            _approximateDecimalValue = new Lazy<decimal>(_ApproximateDecimalImpl);
            _gcd = new Lazy<int>(() => Numerator.GCD(Denominator));
        }

        public int Numerator { get; }
        public int Denominator { get; }
        public FractionSign Sign { get; }

        public decimal ApproximateDecimal => _approximateDecimalValue.Value;

        public bool IsReducedFraction => _gcd.Value != 1;

        public Fraction ReduceFraction()
        {
            return new Fraction(Numerator / _gcd.Value, Denominator / _gcd.Value, Sign);
        }

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

        /// <summary>
        /// This verifies that 2 fractions represent the equivalent ratio
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equivalent(Fraction other)
        {
            if (Equals(other))
            {
                return true;
            }

            return ReduceFraction().Equals(other.ReduceFraction());
        }

        protected bool Equals(Fraction other)
        {
            return Numerator == other.Numerator && Denominator == other.Denominator && Sign == other.Sign;
        }

        /// <summary>
        /// Note that this is testing that 2 fractions are exactly equal, not that 2 fractions are equivalent (ie they reduce to the same fraction)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((Fraction)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Numerator, Denominator, (int)Sign);
        }

        public override string ToString()
        {
            var str = Sign == FractionSign.Negative ? "-" : string.Empty;
            return str + Numerator + "/" + Denominator;
        }

        public static Fraction operator *(Fraction a, Fraction b)
        {
            var sign = FractionSign.Positive;
            if ((a.Sign == FractionSign.Positive && b.Sign == FractionSign.Negative)
                ||
                (a.Sign == FractionSign.Negative && b.Sign == FractionSign.Positive))
            {
                sign = FractionSign.Negative;
            }
            var frac = new Fraction(a.Numerator * b.Numerator, a.Denominator * b.Denominator, sign);
            return frac.ReduceFraction();
        }

    }
}
