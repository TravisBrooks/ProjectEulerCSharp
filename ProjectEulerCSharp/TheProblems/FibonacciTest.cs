using System.Linq;
using NUnit.Framework;

namespace TheProblems
{
    [TestFixture]
    public class FibonacciTest
    {
        [Test]
        public void ExpectedFibonacciNumbers()
        {
            // this is the expected initial sequence from Problem 2, not necessarily the universally expected sequence of Fibonacci numbers
            var expected = new long[] {1, 2, 3, 5, 8, 13, 21, 34, 55, 89};
            var actual = Fibonacci.Sequence().Take(expected.Length);
            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [Test]
        public void TheSequenceHaltsOnItsOwn()
        {
            var fibs = Fibonacci.Sequence().ToList();
            Assert.That(fibs.Count, Is.EqualTo(91),
                "We should stop calculating fibonacci numbers when adding the last 2 would exceed the size of Int64.MaxValue");
            foreach (var fib in fibs)
            {
                Assert.That(fib, Is.GreaterThan(0),
                    "All fibonacci numbers in sequence are positive numbers greater than 0");
            }

            var last = fibs[^1];
            var penultimate = fibs[^2];
            var theFibNotReturned = penultimate + last;
            Assert.That(theFibNotReturned, Is.LessThanOrEqualTo(0),
                "The fibonacci number not returned would have overflowed the Int64");
        }
    }
}