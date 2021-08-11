using System.Collections.Generic;
using NUnit.Framework;

namespace TheProblems
{
    [TestFixture]
    public class PrimesTest
    {
        [Test]
        public void UpTo100()
        {
            var expectedPrimes = new[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97 };
            var actualPrimes = Primes.Calculate(100);
            Assert.That(actualPrimes, Is.EquivalentTo(expectedPrimes));
        }

        [Test]
        public void UpTo1000()
        {
            var expectedPrimes = new[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199, 211, 223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271, 277, 281, 283, 293, 307, 311, 313, 317, 331, 337, 347, 349, 353, 359, 367, 373, 379, 383, 389, 397, 401, 409, 419, 421, 431, 433, 439, 443, 449, 457, 461, 463, 467, 479, 487, 491, 499, 503, 509, 521, 523, 541, 547, 557, 563, 569, 571, 577, 587, 593, 599, 601, 607, 613, 617, 619, 631, 641, 643, 647, 653, 659, 661, 673, 677, 683, 691, 701, 709, 719, 727, 733, 739, 743, 751, 757, 761, 769, 773, 787, 797, 809, 811, 821, 823, 827, 829, 839, 853, 857, 859, 863, 877, 881, 883, 887, 907, 911, 919, 929, 937, 941, 947, 953, 967, 971, 977, 983, 991, 997 };
            var actualPrimes = Primes.Calculate(1000);
            Assert.That(actualPrimes, Is.EquivalentTo(expectedPrimes));
        }

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