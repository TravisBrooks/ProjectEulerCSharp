using System;
using System.Linq;

namespace TheProblems
{
    [Euler(
title: "Number letter counts",
description: @"If the numbers 1 to 5 are written out in words: one, two, three, four, five,
then there are 3 + 3 + 5 + 4 + 4 = 19 letters used in total.

If all the numbers from 1 to 1000 (one thousand) inclusive were written out in
words, how many letters would be used?

NOTE: Do not count spaces or hyphens. For example, 342 (three hundred and
forty-two) contains 23 letters and 115 (one hundred and fifteen) contains 20
letters. The use of ""and"" when writing out numbers is in compliance with
British usage."
        )]
    // ReSharper disable once UnusedMember.Global
    public class Problem0017 : ISolution<int>
    {
        public bool HaveImplementedAnalyticSolution => false;

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
            switch (number)
            {
                case 1000:
                    return one + "thousand";
                case > 900:
                    return nine + hundred + and + SubHundredToEnglish(DropHundred(number));
                case 900:
                    return nine + hundred;
                case > 800:
                    return eight + hundred + and + SubHundredToEnglish(DropHundred(number));
                case 800:
                    return eight + hundred;
                case > 700:
                    return seven + hundred + and + SubHundredToEnglish(DropHundred(number));
                case 700:
                    return seven + hundred;
                case > 600:
                    return six + hundred + and + SubHundredToEnglish(DropHundred(number));
                case 600:
                    return six + hundred;
                case > 500:
                    return five + hundred + and + SubHundredToEnglish(DropHundred(number));
                case 500:
                    return five + hundred;
                case > 400:
                    return four + hundred + and + SubHundredToEnglish(DropHundred(number));
                case 400:
                    return four + hundred;
                case > 300:
                    return three + hundred + and + SubHundredToEnglish(DropHundred(number));
                case 300:
                    return three + hundred;
                case > 200:
                    return two + hundred + and + SubHundredToEnglish(DropHundred(number));
                case 200:
                    return two + hundred;
                case > 100:
                    return one + hundred + and + SubHundredToEnglish(DropHundred(number));
                case 100:
                    return one + hundred;
                default:
                    return SubHundredToEnglish(number);
            }

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
                    > 90 => ninety + DigitToEnglish(DropTen(n)),
                    90 => ninety,
                    > 80 => eighty + DigitToEnglish(DropTen(n)),
                    80 => eighty,
                    > 70 => seventy + DigitToEnglish(DropTen(n)),
                    70 => seventy,
                    > 60 => sixty + DigitToEnglish(DropTen(n)),
                    60 => sixty,
                    > 50 => fifty + DigitToEnglish(DropTen(n)),
                    50 => fifty,
                    > 40 => forty + DigitToEnglish(DropTen(n)),
                    40 => forty,
                    > 30 => thirty + DigitToEnglish(DropTen(n)),
                    30 => thirty,
                    > 20 => twenty + DigitToEnglish(DropTen(n)),
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

            static int DropHundred(int n)
            {
                return n switch
                {
                    > 900 => n - 900,
                    > 800 => n - 800,
                    > 700 => n - 700,
                    > 600 => n - 600,
                    > 500 => n - 500,
                    > 400 => n - 400,
                    > 300 => n - 300,
                    > 200 => n - 200,
                    > 100 => n - 100,
                    _ => throw new ArgumentOutOfRangeException(nameof(n), $"Parameter must be > 100, < 1000, and cannot be evenly divided by 10 but was {n}")
                };
            }

            static int DropTen(int n)
            {
                return n switch
                {
                    > 90 => n - 90,
                    > 80 => n - 80,
                    > 70 => n - 70,
                    > 60 => n - 60,
                    > 50 => n - 50,
                    > 40 => n - 40,
                    > 30 => n - 30,
                    > 20 => n - 20,
                    > 10 => n - 10,
                    _ => throw new ArgumentOutOfRangeException(nameof(n), $"Parameter must be > 10, < 100, and cannot be evenly divided by 10 but was {n}")
                };
            }
        }

        public int AnalyticSolution()
        {
            throw new NotImplementedException();
        }

        public int ExpectedSolution()
        {
            return 21124;
        }
    }
}
