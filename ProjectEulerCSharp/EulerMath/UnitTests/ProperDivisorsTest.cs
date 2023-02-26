using System.Collections.Generic;
using System.Linq;
using Xunit;
using Assert = XunitAssertMessages.AssertM;

namespace ProjectEulerCSharp.EulerMath.UnitTests
{
    public class ProperDivisorsTest
    {
        [Theory]
        [MemberData(nameof(ProperDivisorsData))]
        public void Divisors(ProperDivisorsTestData testData)
        {
            var (n, expectedDivisors) = testData;
            var actualDivisors = ProperDivisors.Of(n);
            Assert.Equivalent(expectedDivisors, actualDivisors, userMessage: $"unexpected divisors for {n}");
        }

        public record ProperDivisorsTestData(int N, IList<int> Divisors);

        public static IEnumerable<object[]> ProperDivisorsData()
        {
            yield return new object[]
            {
                new ProperDivisorsTestData(220, new[]{1, 2, 4, 5, 10, 11, 20, 22, 44, 55, 110})
            };
            yield return new object[]
            {
                new ProperDivisorsTestData(284, new[]{1, 2, 4, 71, 142})
            };
            yield return new object[]
            {
                new ProperDivisorsTestData(36, new[]{1, 2, 18, 3, 12, 4, 9, 6})
            };
        }
    }
}
