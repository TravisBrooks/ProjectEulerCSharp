using System.Linq;
using System.Numerics;
using Xunit;

namespace ProjectEulerCSharp.EulerMath.UnitTests
{
    public class FibonacciTest
    {
        [Fact]
        public void ExpectedFibonacciNumbers()
        {
            // this is the expected initial sequence from Problem 2, not necessarily the universally expected sequence of Fibonacci numbers
            var expected = new [] { 1, 2, 3, 5, 8, 13, 21, 34, 55, 89 }
                .Select(i => new BigInteger(i))
                .ToList();
            var actual = Fibonacci.Sequence().Take(expected.Count());
            Assert.Equal(expected, actual);
        }
    }
}