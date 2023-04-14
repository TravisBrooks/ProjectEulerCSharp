using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using ProjectEulerCSharp.EulerMath;
using ProjectEulerCSharp.PrettyPrint;
using Xunit.Abstractions;
using Assert = XunitAssertMessages.AssertM;

namespace ProjectEulerCSharp
{
    public class BaseTestRunner
    {
        private readonly Stopwatch _stopwatch;
        private readonly ITestOutputHelper _testOutputHelper;

        public BaseTestRunner(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _stopwatch = new Stopwatch();
            // eagerly the load factorials, otherwise the Factorial static ctor makes the first ISolution that uses Factorial look way slower than it really is
            _ = Factorial.Of(10);
        }

        protected void SolutionImpl<T>(ISolution<T> solutionInstance) where T : INumber<T>
        {
            var analyticElapsed = default(TimeSpan);
            var analyticSolution = default(T);

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

            var eulerReport = new SimpleReport(textMaxCharsPerLine: 80);

            var eulerAttribute = (EulerAttribute)Attribute.GetCustomAttribute(solutionInstance.GetType(), typeof(EulerAttribute));
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

        public static IEnumerable<object[]> DiscoverSolutionInstances(Type testRunnerType)
        {
            var solutionTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => 
                    t.GetInterfaces().Any(i => 
                        i.IsGenericType &&
                        i.GetGenericTypeDefinition() == typeof(ISolution<>))
                    &&
                    t.Namespace == testRunnerType.Namespace
                    )
                .OrderBy(t => t.Name)
                .ToArray();

            var testCases = new List<object[]>();

            foreach (var solutionType in solutionTypes)
            {
                var instance = Activator.CreateInstance(solutionType);
                testCases.Add(new[] { instance });
            }

            return testCases.ToArray();
        }
    }
}