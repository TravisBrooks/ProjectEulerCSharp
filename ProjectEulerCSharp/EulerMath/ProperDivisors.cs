﻿using System;
using System.Collections.Generic;

namespace ProjectEulerCSharp.EulerMath
{
    public static class ProperDivisors
    {
        public static IEnumerable<int> Of(int n)
        {
            if (n <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n), $"Can only find divisors for a number greater than 0, was passed in {n}.");
            }

            var divisors = new HashSet<int> { 1 };
            for (var i = 2; i <= (int)Math.Ceiling(Math.Sqrt(n)); i++)
            {
                if (n % i == 0)
                {
                    divisors.Add(i);
                    var maybe = n / i;
                    if (i != maybe)
                    {
                        divisors.Add(maybe);
                    }
                }
            }

            return divisors;
        }
    }
}