using System;
using System.Collections;
using System.Collections.Generic;

namespace ProjectEulerCSharp.EulerMath
{
    public static class Primes
    {
        public static IEnumerable<int> Calculate(int upperBounds)
        {
            var sieve = _BuildSieve(upperBounds);
            for (var i = 0; i < sieve.Length; i++)
            {
                if (sieve[i])
                {
                    yield return i;
                }
            }
        }

        /// <summary>
        /// Builds a function that checks if a given integer is prime
        /// </summary>
        /// <param name="upperBounds">The maximum integer that can be tested </param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Func<int, bool> IsPrime(int upperBounds)
        {
            var sieve = _BuildSieve(upperBounds);

            bool IsPrimeImpl(int n)
            {
                if (n > upperBounds)
                {
                    throw new ArgumentException($"The maximum integer that can be checked if it is prime is the {nameof(upperBounds)} value {upperBounds}");
                }

                if (n < 2)
                {
                    return false;
                }

                return sieve[n];
            }

            return IsPrimeImpl;
        }

        private static BitArray _BuildSieve(int upperBounds)
        {
            // The sieve is being treated as having a 1 based index
            var sieve = new BitArray(upperBounds + 1, true)
            {
                [0] = false,
                [1] = false
            };

            var index = 2;
            var innerIndex = 2;

            while (index * innerIndex <= upperBounds)
            {
                while (index * innerIndex <= upperBounds)
                {
                    // Should be pretty obvious, for every innerIndex in loops mark as not prime innerIndex*2, innerIndex*3, etc
                    // We only need to consider doing this if sieve[innerIndex] is true (not a factor of a prior innerIndex).
                    var factor = innerIndex;
                    while (sieve[innerIndex] && (factor += innerIndex) <= upperBounds)
                    {
                        sieve[factor] = false;
                    }

                    innerIndex++;
                }

                index++;
            }

            return sieve;
        }
    }
}