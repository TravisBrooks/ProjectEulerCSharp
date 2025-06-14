using System.Collections.Generic;
using System.Numerics;
using Xunit;
using Xunit.Abstractions;

namespace ProjectEulerCSharp.EulerProblems._0030s
{
    public class TestRunner : BaseTestRunner
    {
        public TestRunner(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        [Theory(DisplayName = "Solutions 0030s")]
        [Trait("Category", "EulerProblems")]
        [MemberData(nameof(SolutionInstances))]
        public void Solution<T>(ISolution<T> solutionInstance) where T : INumber<T>
        {
            SolutionImpl(solutionInstance);
        }

        public static IEnumerable<object[]> SolutionInstances()
        {
            return BaseTestRunner.DiscoverSolutionInstances(typeof(TestRunner));
        }
    }
}
