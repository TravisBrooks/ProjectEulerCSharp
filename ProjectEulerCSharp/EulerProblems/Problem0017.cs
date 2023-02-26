using System;
using System.Linq;

namespace ProjectEulerCSharp.EulerProblems
{
    [Euler(
title: "Number letter counts",
description: @"If the numbers 1 to 5 are written out in words: one, two, three, four, five, then there are 3 + 3 + 5 + 4 + 4 = 19 letters used in total.

If all the numbers from 1 to 1000 (one thousand) inclusive were written out in words, how many letters would be used?

NOTE: Do not count spaces or hyphens. For example, 342 (three hundred and forty-two) contains 23 letters and 115 (one hundred and fifteen) contains 20 letters. The use of ""and"" when writing out numbers is in compliance with British usage."
        )]
    // ReSharper disable once UnusedMember.Global
    public class Problem0017 : ISolution<int>
    {
        public bool HaveImplementedAnalyticSolution => true;

        public int BruteForceSolution()
        {
            var sum = 0;
            foreach (var number in Enumerable.Range(1, 1000))
            {
                var word = NumberToEnglish(number);
                sum += word.Length;
            }
            return sum;
        }

        /// <summary>
        /// Could probably come up with something more clever but this was the first idea I had and it seemed to work
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private static string NumberToEnglish(int number)
        {
            const string one = "one";
            const string two = "two";
            const string three = "three";
            const string four = "four";
            const string five = "five";
            const string six = "six";
            const string seven = "seven";
            const string eight = "eight";
            const string nine = "nine";
            const string hundred = "hundred";
            const string and = "and";
            return number switch
            {
                1000 => one + "thousand",
                > 900 => nine + hundred + and + SubHundredToEnglish(number % 100),
                900 => nine + hundred,
                > 800 => eight + hundred + and + SubHundredToEnglish(number % 100),
                800 => eight + hundred,
                > 700 => seven + hundred + and + SubHundredToEnglish(number % 100),
                700 => seven + hundred,
                > 600 => six + hundred + and + SubHundredToEnglish(number % 100),
                600 => six + hundred,
                > 500 => five + hundred + and + SubHundredToEnglish(number % 100),
                500 => five + hundred,
                > 400 => four + hundred + and + SubHundredToEnglish(number % 100),
                400 => four + hundred,
                > 300 => three + hundred + and + SubHundredToEnglish(number % 100),
                300 => three + hundred,
                > 200 => two + hundred + and + SubHundredToEnglish(number % 100),
                200 => two + hundred,
                > 100 => one + hundred + and + SubHundredToEnglish(number % 100),
                100 => one + hundred,
                _ => SubHundredToEnglish(number),
            };

            static string SubHundredToEnglish(int n)
            {
                const string twenty = "twenty";
                const string thirty = "thirty";
                const string forty = "forty";
                const string fifty = "fifty";
                const string sixty = "sixty";
                const string seventy = "seventy";
                const string eighty = "eighty";
                const string ninety = "ninety";

                return n switch
                {
                    > 90 => ninety + DigitToEnglish(n % 10),
                    90 => ninety,
                    > 80 => eighty + DigitToEnglish(n % 10),
                    80 => eighty,
                    > 70 => seventy + DigitToEnglish(n % 10),
                    70 => seventy,
                    > 60 => sixty + DigitToEnglish(n % 10),
                    60 => sixty,
                    > 50 => fifty + DigitToEnglish(n % 10),
                    50 => fifty,
                    > 40 => forty + DigitToEnglish(n % 10),
                    40 => forty,
                    > 30 => thirty + DigitToEnglish(n % 10),
                    30 => thirty,
                    > 20 => twenty + DigitToEnglish(n % 10),
                    20 => twenty,
                    > 10 => TeenToEnglish(n),
                    _ => DigitToEnglish(n)
                };
            }

            static string TeenToEnglish(int n)
            {
                return n switch
                {
                    11 => "eleven",
                    12 => "twelve",
                    13 => "thirteen",
                    14 => "fourteen",
                    15 => "fifteen",
                    16 => "sixteen",
                    17 => "seventeen",
                    18 => "eighteen",
                    19 => "nineteen",
                    _ => throw new ArgumentOutOfRangeException(nameof(n), $"Parameter must be > 10 and < 20 but was {n}")
                };
            }

            static string DigitToEnglish(int n)
            {
                return n switch
                {
                    1 => "one",
                    2 => "two",
                    3 => "three",
                    4 => "four",
                    5 => "five",
                    6 => "six",
                    7 => "seven",
                    8 => "eight",
                    9 => "nine",
                    10 => "ten",
                    _ => throw new ArgumentOutOfRangeException(nameof(n), $"Parameter must be > 0 and <= 10 but was {n}")
                };
            }
        }

        public int AnalyticSolution()
        {
            // this is really more of a succinct brute force method than analytic. Relies on some stupid array indexing tricks.
            var sum = 0;
            foreach (var number in Enumerable.Range(1, 1000))
            {
                sum += NumberToEnglishWordLength(number);
            }
            return sum;
        }

        private static readonly int[] TensAndTeens = { 0, "one".Length, "two".Length, "three".Length, "four".Length, "five".Length, "six".Length, "seven".Length, "eight".Length, "nine".Length, "ten".Length, "eleven".Length, "twelve".Length, "thirteen".Length, "fourteen".Length, "fifteen".Length, "sixteen".Length, "seventeen".Length, "eighteen".Length, "nineteen".Length };
        private static readonly int[] SubHundred = { 0, 0, "twenty".Length, "thirty".Length, "forty".Length, "fifty".Length, "sixty".Length, "seventy".Length, "eighty".Length, "ninety".Length };
        private static int NumberToEnglishWordLength(int number)
        {
            if (number == 1000)
            {
                return "OneThousand".Length;
            }

            var len = 0;
            if (number >= 100)
            {
                var remainder = number % 100;
                len = TensAndTeens[(number - remainder) / 100] + "hundred".Length;
                if (remainder != 0)
                {
                    len += "and".Length;
                }
                number = remainder;
            }

            if (number >= 20)
            {
                var remainder = number % 10;
                len += SubHundred[(number - remainder) / 10];
                number = remainder;
            }

            if (number > 0)
            {
                len += TensAndTeens[number];
            }
            return len;
        }

        public int ExpectedSolution()
        {
            return 21124;
        }
    }
}
