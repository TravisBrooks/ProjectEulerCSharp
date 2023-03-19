using System.Collections.Generic;
using Xunit;
using Assert = XunitAssertMessages.AssertM;

namespace ProjectEulerCSharp.EulerMath.UnitTests
{
    public class ExtensionMethodsGetDigitsTest
    {
        [Theory]
        [MemberData(nameof(GetDigitsData))]
        public void GetDigitsTest(GetDigitsTestData testData)
        {
            var (n, expectedDigits) = testData;
            var actualDigits = n.GetDigits();
            Assert.Equal(expectedDigits, actualDigits, "unexpected digits for " + n);
        }

        public static IEnumerable<object[]> GetDigitsData()
        {
            yield return new object[]
            {
                new GetDigitsTestData(0, new[] { 0 })
            };
            yield return new object[]
            {
                new GetDigitsTestData(1, new[] { 1 })
            };
            yield return new object[]
            {
                new GetDigitsTestData(12, new[] { 1, 2 })
            };
            yield return new object[]
            {
                new GetDigitsTestData(12345, new[] { 1, 2, 3, 4, 5})
            };
            yield return new object[]
            {
                new GetDigitsTestData(10, new[] { 1, 0})
            };
            yield return new object[]
            {
                new GetDigitsTestData(102, new[] { 1, 0, 2})
            };
            // negative numbers should return the digits ignoring the sign
            yield return new object[]
            {
                new GetDigitsTestData(-1, new[] { 1 })
            };
            yield return new object[]
            {
                new GetDigitsTestData(-12, new[] { 1, 2})
            };
        }

        public record GetDigitsTestData(int N, IEnumerable<int> ExpectedDigits);
    }
}
