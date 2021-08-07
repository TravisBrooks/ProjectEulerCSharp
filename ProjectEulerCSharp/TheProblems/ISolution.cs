namespace TheProblems
{
    /// <summary>
    /// Marker interface, only meant to be implemented by ISolution&lt;T&gt;
    /// </summary>
    public interface ISolution
    {
        bool HaveImplementedAnalyticSolution { get; }
    }

    public interface ISolution<out T> : ISolution
    {
        T BruteForceSolution();
        T AnalyticSolution();
        T ExpectedSolution();
    }
}