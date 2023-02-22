using System.Collections.Generic;
using Xunit;
using Assert = XunitAssertMessages.AssertM;

namespace ProjectEulerCSharp.EulerMath.UnitTests
{
    public class PrimeFactorsTest
    {
        [Theory]
        [MemberData(nameof(PrimeFactorData))]
        public void PrimeFactors(PrimeFactorsTestData testData)
        {
            var (n, factorsExpected) = testData;
            var factorsActual = PrimeFactor32.Factors(n);
            Assert.Equivalent(factorsExpected, factorsActual, userMessage: "unexpected factors for " + n);
        }

        public record PrimeFactorsTestData(int N, IEnumerable<PrimeFactor32> FactorsExpected);

        public static IEnumerable<object[]> PrimeFactorData()
        {
            yield return new object[]
            {
                new PrimeFactorsTestData(12, new[] { new PrimeFactor32(2, 2), new PrimeFactor32(3, 1) })
            };
            yield return new object[]
            {
                new PrimeFactorsTestData(18, new[] { new PrimeFactor32(2, 1), new PrimeFactor32(3, 2) })
            };
            yield return new object[]
            {
                new PrimeFactorsTestData(20, new[] { new PrimeFactor32(2, 2), new PrimeFactor32(5, 1) })
            };
            yield return new object[]
            {
                new PrimeFactorsTestData(24, new[] { new PrimeFactor32(2, 3), new PrimeFactor32(3, 1) })
            };
        }
    }
}