using System.Collections.Generic;
using System.Numerics;

namespace ProjectEulerCSharp.EulerMath
{
    public static class Fibonacci
    {
        public static IEnumerable<BigInteger> Sequence()
        {
            var first = BigInteger.One;
            var second = new BigInteger(2);

            yield return first;
            yield return second;

            while (true)
            {
                var newNumber = first + second;
                first = second;
                second = newNumber;

                yield return newNumber;
            }
        }
    }
}