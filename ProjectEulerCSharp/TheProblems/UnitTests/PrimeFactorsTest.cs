using System.Collections.Generic;
using NUnit.Framework;

namespace TheProblems.UnitTests
{
    [TestFixture]
    public class PrimeFactorsTest
    {
        [Test]
        [TestCaseSource(nameof(PrimeFactorData))]
        public void PrimeFactors(int n, IEnumerable<PrimeFactor32> factorsExpected)
        {
            var factorsActual = PrimeFactor32.Factors(n);
            Assert.That(factorsActual, Is.EquivalentTo(factorsExpected), "unexpected factors for " + n);
        }

        public static IEnumerable<TestCaseData> PrimeFactorData()
        {
            {
                yield return new TestCaseData(12, new[] { new PrimeFactor32(2, 2), new PrimeFactor32(3, 1) });
            }
            {
                yield return new TestCaseData(18, new[] { new PrimeFactor32(2, 1), new PrimeFactor32(3, 2) });
            }
            {
                yield return new TestCaseData(20, new[] { new PrimeFactor32(2, 2), new PrimeFactor32(5, 1) });
            }
            {
                yield return new TestCaseData(24, new[] { new PrimeFactor32(2, 3), new PrimeFactor32(3, 1) });
            }
        }
    }
}