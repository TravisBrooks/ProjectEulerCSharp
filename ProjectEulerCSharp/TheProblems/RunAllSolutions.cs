using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace TheProblems
{
    [TestFixture]
    public class RunAllSolutions
    {
        [OneTimeSetUp]
        public void FixtureSetup()
        {
            _stopwatch = new Stopwatch();
        }

        private Stopwatch _stopwatch;

        [TestCaseSource(nameof(_AllSolutions))]
        public void EulerSolution(ISolution objectSolution)
        {
            // NUnit has real problems dealing with TestCaseSource and generic types, so I introduced marker interface ISolution to get around that
            // then use reflection to get to the methods defined for ISolution<T> for whatever T is bound to for the given problem class.

            var expectedMethod = objectSolution.GetType().GetMethod(nameof(ISolution<object>.ExpectedSolution));
            var actualMethod = objectSolution.GetType().GetMethod(nameof(ISolution<object>.TheSolution));

            Assert.NotNull(expectedMethod);
            Assert.NotNull(actualMethod);

            var expected = expectedMethod.Invoke(objectSolution, null);
            _stopwatch.Restart();
            var actual = actualMethod.Invoke(objectSolution, null);
            _stopwatch.Stop();

            var eulerAttribute = (EulerAttribute) Attribute.GetCustomAttribute(objectSolution.GetType(), typeof(EulerAttribute));
            Assert.That(eulerAttribute, Is.Not.Null, $"Did not find an {nameof(EulerAttribute)} for solution {objectSolution.GetType().Name}");

            Console.WriteLine(eulerAttribute.Title);
            Console.WriteLine();
            Console.WriteLine(eulerAttribute.Description);
            Console.WriteLine();
            Console.WriteLine($"Calculated solution: {actual}");
            Console.WriteLine();
            Console.WriteLine($"Time spent calculating solution: {_stopwatch.Elapsed}");
            Assert.That(actual, Is.EqualTo(expected));
        }

        private static IEnumerable<TestCaseData> _AllSolutions()
        {
            var solutionTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && typeof(ISolution).IsAssignableFrom(t))
                .ToArray();

            var testCases = new List<TestCaseData>();

            foreach (var solutionType in solutionTypes.OrderBy(st => st.Name))
            {
                var instance = (ISolution) Activator.CreateInstance(solutionType);
                testCases.Add(new TestCaseData(instance));
            }

            return testCases.ToArray();
        }
    }
}