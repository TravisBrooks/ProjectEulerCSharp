using System.Collections;
using System.Collections.Generic;

namespace ProjectEulerCSharp.EulerMath
{
    public static class Primes
    {
        public static IEnumerable<int> Calculate(int upperBounds)
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

            for (var i = 0; i < sieve.Length; i++)
            {
                if (sieve[i])
                {
                    yield return i;
                }
            }
        }
    }

}