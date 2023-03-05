using System;
using System.Linq;

namespace ProjectEulerCSharp.EulerMath
{
    internal static class ExtensionMethods
    {
        private static readonly int AsciiToIntegralDelta = 48;

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
    }
}