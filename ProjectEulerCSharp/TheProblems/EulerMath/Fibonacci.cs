using System.Collections.Generic;

namespace TheProblems.EulerMath
{
    public static class Fibonacci
    {
        public static IEnumerable<long> Sequence()
        {
            var first = 1L;
            var second = 2L;

            yield return first;
            yield return second;

            while (true)
            {
                var newNumber = first + second;
                first = second;
                second = newNumber;

                if (second < first)
                {
                    // if we overflowed past the max long and wrapped around to a negative number we've gone too far
                    yield break;
                }

                yield return newNumber;
            }
        }
    }
}