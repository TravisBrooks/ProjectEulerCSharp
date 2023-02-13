using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using TheProblems.PrettyPrint;
using Xunit;
using Xunit.Abstractions;
using Assert = XunitAssertMessages.AssertM;

namespace TheProblems
{
    public class SolveAllEulerProblems
    {
        private readonly Stopwatch _stopwatch = new();
        private readonly ITestOutputHelper _testOutputHelper;

        public SolveAllEulerProblems(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Theory]
        [MemberData(nameof(AllSolutionInstances))]
        public void EulerSolution<T>(ISolution<T> solutionInstance) where T : INumber<T>
        {
            var analyticElapsed = default(TimeSpan);
            T analyticSolution = default(T);

            var expected = solutionInstance.ExpectedSolution();
            _stopwatch.Restart();

            var bruteForceSolution = solutionInstance.BruteForceSolution();
            _stopwatch.Stop();
            var bruteForceElapsed = _stopwatch.Elapsed;

            if (solutionInstance.HaveImplementedAnalyticSolution)
            {
                _stopwatch.Restart();
                analyticSolution = solutionInstance.AnalyticSolution();
                _stopwatch.Stop();
                analyticElapsed = _stopwatch.Elapsed;
            }

            var eulerReport = new SimpleReport(minTextWidthInChars: 80);

            var eulerAttribute = (EulerAttribute) Attribute.GetCustomAttribute(solutionInstance.GetType(), typeof(EulerAttribute));
            Debug.Assert(eulerAttribute is not null, $"Did not find an {nameof(EulerAttribute)} for solution {solutionInstance.GetType().Name}");

            eulerReport.AddContainer(eulerAttribute.Title);
            eulerReport.AddContainer(eulerAttribute.Description);

            var builder = new StringBuilder();
            builder.AppendLine($"Brute force solution: {bruteForceSolution}");
            builder.Append($"Time spent calculating brute force solution: {bruteForceElapsed}");

            if (solutionInstance.HaveImplementedAnalyticSolution)
            {
                eulerReport.AddContainer(builder.ToString());
            }
            else
            {
                eulerReport.AddContainer(builder.ToString());
            }

            // reset builder
            builder = new StringBuilder();

            if (solutionInstance.HaveImplementedAnalyticSolution)
            {
                builder.AppendLine($"Analytic solution: {analyticSolution}");
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

            _testOutputHelper.WriteLine(eulerReport.PrettyPrintString());
            
            Assert.Equal(expected, bruteForceSolution, "brute force solution was incorrect");

            if (solutionInstance.HaveImplementedAnalyticSolution)
            {
                Assert.Equal(expected, analyticSolution, "analytic solution was incorrect");
            }
        }

        public static IEnumerable<object[]> AllSolutionInstances()
        {
            var solutionTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISolution<>)))
                // reverse ordering by name is a hack to get the tests to display correctly in built in VS test explorer. Resharper test runner goes in parallel so the displayed test order is a bit random.
                .OrderByDescending(t => t.Name)
                .ToArray();

            var testCases = new List<object[]>();

            foreach (var solutionType in solutionTypes)
            {
                var instance = Activator.CreateInstance(solutionType);
                testCases.Add(new []{instance});
            }

            return testCases.ToArray();
        }

    }
}