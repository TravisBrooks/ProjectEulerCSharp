using System.Numerics;

namespace TheProblems
{
    public interface ISolution<out T> where T : INumber<T>
    {
        bool HaveImplementedAnalyticSolution { get; }

        T BruteForceSolution();
        T AnalyticSolution();
        T ExpectedSolution();
    }
}