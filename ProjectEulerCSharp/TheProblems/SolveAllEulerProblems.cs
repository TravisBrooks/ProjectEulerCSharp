using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using TheProblems.PrettyPrint;

namespace TheProblems
{
    [TestFixture]
    public class SolveAllEulerProblems
    {
        private readonly Stopwatch _stopwatch = new();

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

            var analyticElapsed = default(TimeSpan);
            dynamic analyticSolution = null;

            var expected = expectedMethod.Invoke(objectSolution, null);
            _stopwatch.Restart();

            dynamic bruteForceSolution = bruteForceMethod.Invoke(objectSolution, null);
            _stopwatch.Stop();
            var bruteForceElapsed = _stopwatch.Elapsed;

            if (objectSolution.HaveImplementedAnalyticSolution)
            {
                _stopwatch.Restart();
                analyticSolution = analyticMethod.Invoke(objectSolution, null);
                _stopwatch.Stop();
                analyticElapsed = _stopwatch.Elapsed;
            }

            var eulerReport = new SimpleReport(minTextWidthInChars: 100);

            var eulerAttribute = (EulerAttribute) Attribute.GetCustomAttribute(objectSolution.GetType(), typeof(EulerAttribute));
            Assert.That(eulerAttribute, Is.Not.Null, $"Did not find an {nameof(EulerAttribute)} for solution {objectSolution.GetType().Name}");

            eulerReport.AddContainer(eulerAttribute.Title);
            eulerReport.AddContainer(eulerAttribute.Description);

            var builder = new StringBuilder();
            builder.AppendLine($"Brute force solution: {bruteForceSolution}");
            builder.Append($"Time spent calculating brute force solution: {bruteForceElapsed}");

            if (objectSolution.HaveImplementedAnalyticSolution)
            {
                eulerReport.AddContainer(builder.ToString());
            }
            else
            {
                eulerReport.AddContainer(builder.ToString());
            }

            // reset builder
            builder = new StringBuilder();

            if (objectSolution.HaveImplementedAnalyticSolution)
            {
                builder.AppendLine($"Analytic solution: {bruteForceSolution}");
                builder.Append($"Time spent calculating analytic solution: {analyticElapsed}");
                eulerReport.AddContainer(builder.ToString());

                if (bruteForceElapsed < analyticElapsed)
                {
                    eulerReport.AddContainer("Brute force wins! This time anyway...");
                }
                else if (analyticElapsed < bruteForceElapsed)
                {
                    eulerReport.AddContainer("The analytical solution wins! This time anyway...");
                }
                else
                {
                    eulerReport.AddContainer("An exact tie between brute force and analytic solutions! Inconceivable!");
                }

            }

            Console.Write(eulerReport.PrettyPrintString());

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