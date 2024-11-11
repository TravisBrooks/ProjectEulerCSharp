using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ProjectEulerCSharp.EulerMath
{
    internal static class ExtensionMethods
    {
        private static readonly int AsciiToIntegralDelta = 48;

        /// <summary>
        /// This is to convert '1', '2',... to 1,2, ... 
        /// </summary>
        /// <param name="intChar"></param>
        /// <returns></returns>
        public static int ToIntFast(this char intChar)
        {
            return intChar - AsciiToIntegralDelta;
        }

        public static int ToIntChecked(this char intChar)
        {
            if (intChar < 48 || intChar > 57)
            {
                throw new ArgumentOutOfRangeException(nameof(intChar), $"must be one of 0123456789 characters but was {intChar}");
            }

            return intChar.ToIntFast();
        }

        /// <summary>
        /// 'A'=1, 'b'=2, etc
        /// </summary>
        /// <param name="someChar"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static int CharOrdinalPosition(this char someChar)
        {
            // uppercase
            if (someChar > 64 && someChar < 91)
            {
                return someChar - 64;
            }
            // lowercase
            if (someChar > 96 && someChar < 123)
            {
                return someChar - 96;
            }
            // not a letter
            throw new ArgumentOutOfRangeException(nameof(someChar), "Can only return the ordinal position for a char that is an uppercase or lowercase of the English alphabet.");
        }

        public static char ToCharFast(this int singleDigit)
        {
            return (char)(singleDigit + AsciiToIntegralDelta);
        }

        public static char ToCharChecked(this int singleDigit)
        {
            if (singleDigit < 0 || singleDigit > 9)
            {
                throw new ArgumentOutOfRangeException(nameof(singleDigit), $"must be a number between 0 and 9 inclusive but was {singleDigit}");
            }

            return singleDigit.ToCharFast();
        }

        /// <summary>
        /// </summary>
        /// <param name="str"></param>
        /// <param name="splitOptions">Defaults to TrimEntries|RemoveEmptyEntries</param>
        /// <returns></returns>
        public static string[] SplitOnNewLines(
            this string str,
            StringSplitOptions splitOptions = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
        {
            return str.Split(new[] { "\r\n", "\r", "\n" }, splitOptions);
        }

        public static int[][] TabularDataToJaggedIntArray(this string str)
        {
            var rows = str.SplitOnNewLines();
            var arr = new int[rows.Length][];
            for (var i = 0; i < rows.Length; i++)
            {
                var rowData = rows[i].Split(null).Select(int.Parse).ToArray();
                arr[i] = rowData;
            }

            return arr;
        }

        /// <summary>
        /// An experiment to see how hard/easy it is to use the new generic math. Took a little getting used to but wasn't that
        /// bad, but the performance was quite bad. Will need to do further experiments before attempting to use generic math more.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="n"></param>
        /// <returns>digits in n (currently in reverse order)</returns>
		public static IEnumerable<T> GetDigitsGen<T>(this T n) where T : IBinaryInteger<T>
		{
	        if (n < T.Zero)
	        {
		        n = -n;
	        }

	        if (n == T.Zero)
	        {
		        yield return n;
		        yield break;
	        }

			var ten = T.CreateChecked(10);
	        while (n > T.Zero)
	        {
		        if (n < ten)
		        {
			        yield return n;
                    yield break;
		        }

		        var d = n % ten;
		        n /= ten;
		        yield return d;
	        }
		}

        public static int[] GetDigits(this int n)
        {
            if (n < 0)
            {
                n = -n;
            }

            if (n < 10)
            {
                return new[] { n };
            }
            var digitCount = n.GetCountOfDigits();
            var digits = new int[digitCount];
            for (var i = digitCount - 1; i >= 0; i--)
            {
                if (n < 10)
                {
                    digits[i] = n;
                }
                else
                {
                    var d = n % 10;
                    digits[i] = d;
                    n /= 10;
                }
            }
            return digits;
        }

        public static int[] GetDigits(this long n)
        {
            if (n < 0)
            {
                n = -n;
            }

            if (n < 10)
            {
                return new[] { (int)n };
            }
            var digitCount = n.GetCountOfDigits();
            var digits = new int[digitCount];
            for (var i = digitCount - 1; i >= 0; i--)
            {
                if (n < 10)
                {
                    digits[i] = (int)n;
                }
                else
                {
                    var d = n % 10;
                    digits[i] = (int)d;
                    n /= 10;
                }
            }
            return digits;
        }

        public static long Reverse(this long n)
        {
	        var digits = n.GetDigits();
	        Array.Reverse(digits);
	        return digits.ToLong();
        }

		public static long ToLong(this int[] digits)
        {
            return new ReadOnlySpan<int>(digits).ToLong();
        }

        public static long ToLong(this ReadOnlySpan<int> digits)
        {
            long total = 0;
            long mult = 1;
            for (var i = digits.Length - 1; i >= 0; i--)
            {
                total += digits[i] * mult;
                mult *= 10;
            }
            return total;
        }

        public static int Reverse(this int n)
        {
	        var digits = n.GetDigits();
	        Array.Reverse(digits);
	        return digits.ToInt();
        }

		public static int ToInt(this int[] digits)
        {
            return new ReadOnlySpan<int>(digits).ToInt();
        }

        public static int ToInt(this ReadOnlySpan<int> digits)
        {
            var total = 0;
            var mult = 1;
            for (var i = digits.Length - 1; i >= 0; i--)
            {
                total += digits[i] * mult;
                mult *= 10;
            }
            return total;
        }

        public static int GetCountOfDigits(this int n)
        {
            // ye olde log trick
            return (int)(Math.Floor(Math.Log10(n)) + 1);
        }

        public static int GetCountOfDigits(this long n)
        {
            // ye olde log trick
            return (int)(Math.Floor(Math.Log10(n)) + 1);
        }

        /// <summary>
        /// Greatest common divisor
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int GCD(this int a, int b)
        {
            // TODO: if I need a gcd for more than just int convert this to use IBinaryInteger<T>
            // this is just Euclidean Algorithm for GCD
            while (true)
            {
                if (b == 0)
                {
                    return a;
                }

                var a1 = a;
                a = b;
                b = a1 % b;
            }
        }

        public static bool IsInteger(this double d)
        {
            const double precision = 0.00001;
            return Math.Abs(d - Math.Floor(d)) < precision;
        }
    }
}