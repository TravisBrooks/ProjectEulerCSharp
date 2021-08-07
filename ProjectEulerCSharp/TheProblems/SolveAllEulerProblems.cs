using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace TheProblems
{
    [TestFixture]
    public class SolveAllEulerProblems
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
            var bruteForceMethod = objectSolution.GetType().GetMethod(nameof(ISolution<object>.BruteForceSolution));
            var analyticMethod = objectSolution.GetType().GetMethod(nameof(ISolution<object>.AnalyticSolution));

            Assert.NotNull(expectedMethod);
            Assert.NotNull(bruteForceMethod);
            Assert.NotNull(analyticMethod);

            TimeSpan bruteForceElapsed;
            TimeSpan analyticElapsed = default(TimeSpan);
            dynamic bruteForceSolution;
            dynamic analyticSolution = null;

            var expected = expectedMethod.Invoke(objectSolution, null);
            _stopwatch.Restart();

            bruteForceSolution = bruteForceMethod.Invoke(objectSolution, null);
            _stopwatch.Stop();
            bruteForceElapsed = _stopwatch.Elapsed;

            if (objectSolution.HaveImplementedAnalyticSolution)
            {
                _stopwatch.Restart();
                analyticSolution = analyticMethod.Invoke(objectSolution, null);
                _stopwatch.Stop();
                analyticElapsed = _stopwatch.Elapsed;
            }

            var eulerAttribute = (EulerAttribute) Attribute.GetCustomAttribute(objectSolution.GetType(), typeof(EulerAttribute));
            Assert.That(eulerAttribute, Is.Not.Null, $"Did not find an {nameof(EulerAttribute)} for solution {objectSolution.GetType().Name}");

            Console.WriteLine(eulerAttribute.Title);
            Console.WriteLine(eulerAttribute.Description);
            // TODO: come with some pretty print to make a border around the Title and Description in the output
            //Console.WriteLine("‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾");
            Console.WriteLine();
            Console.WriteLine($"Brute force solution: {bruteForceSolution}");
            Console.WriteLine($"Time spent calculating brute force solution: {bruteForceElapsed}");

            if (objectSolution.HaveImplementedAnalyticSolution)
            {
                Console.WriteLine();
                Console.WriteLine($"Analytic solution: {bruteForceSolution}");
                Console.WriteLine($"Time spent calculating analytic solution: {analyticElapsed}");
                Console.WriteLine();

                if (bruteForceElapsed < analyticElapsed)
                {
                    Console.WriteLine("Brute force wins! This time anyway...");
                }
                else if (analyticElapsed < bruteForceElapsed)
                {
                    Console.WriteLine("The analytical solution wins! This time anyway...");
                }
                else
                {
                    Console.WriteLine("An exact tie between brute force and analytic solutions! Inconceivable!");
                }
            }

            Assert.That(bruteForceSolution, Is.EqualTo(expected), "brute force solution was incorrect");
            if (objectSolution.HaveImplementedAnalyticSolution)
            {
                Assert.That(analyticSolution, Is.EqualTo(expected), "analytic solution was incorrect");
            }
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