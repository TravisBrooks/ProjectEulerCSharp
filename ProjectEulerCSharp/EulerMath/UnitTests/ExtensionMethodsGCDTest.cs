﻿using System.Collections.Generic;
using Xunit;
using Assert = XunitAssertMessages.AssertM;

namespace ProjectEulerCSharp.EulerMath.UnitTests
{
    public class ExtensionMethodsGCDTest
    {
        [Theory]
        [MemberData(nameof(GetGcdTestData))]
        public void GCDTest(GcdTestData testData)
        {
            var (a, b, expectedGcd) = testData;
            var actualGcd = a.GCD(b);
            Assert.Equal(expectedGcd, actualGcd, $"unexpected gcd for {a}, {b} actual gcd = {actualGcd}");
        }

        public static IEnumerable<object[]> GetGcdTestData()
        {
            yield return new object[]
            {
                new GcdTestData(4, 16, 4)
            };
            yield return new object[]
            {
                new GcdTestData(16, 4, 4)
            };
            yield return new object[]
            {
                new GcdTestData(270, 192, 6)
            };
            yield return new object[]
            {
                new GcdTestData(1, 4, 1)
            };
            // for 2 primes the gcd is 1
            yield return new object[]
            {
                new GcdTestData(3, 7, 1)
            };
        }

        public record GcdTestData(int A, int B, int ExpectedGcd);
    }
}
