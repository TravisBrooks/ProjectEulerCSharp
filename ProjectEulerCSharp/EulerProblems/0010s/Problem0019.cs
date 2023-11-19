using System;

namespace ProjectEulerCSharp.EulerProblems._0010s
{
    [Euler(
        title: "Problem 19: Counting Sundays",
        description: @"You are given the following information, but you may prefer to do some research for yourself.

• 1 Jan 1900 was a Monday.
• Thirty days has September,
April, June and November.
All the rest have thirty-one,
Saving February alone,
Which has twenty-eight, rain or shine.
And on leap years, twenty-nine.
• A leap year occurs on any year evenly divisible by 4, but not on a century unless it is divisible by 400.

How many Sundays fell on the first of the month during the twentieth century (1 Jan 1901 to 31 Dec 2000)?")
    ]
    // ReSharper disable once UnusedType.Global
    public class Problem0019 : ISolution<int>
    {
        public bool HaveImplementedAnalyticSolution => false;

        public int BruteForceSolution()
        {
            var currentDate = DateTime.Parse("1 Jan 1901");
            var endDate = DateTime.Parse("31 Dec 2000");
            var count = 0;
            while (currentDate < endDate)
            {
                if (currentDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    count++;
                }

                currentDate = currentDate.AddMonths(1);
            }

            return count;
        }

        public int AnalyticSolution()
        {
            throw new NotImplementedException();
        }

        public int ExpectedSolution()
        {
            return 171;
        }
    }
}