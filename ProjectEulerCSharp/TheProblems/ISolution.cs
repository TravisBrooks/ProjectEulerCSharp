namespace TheProblems
{
    /// <summary>
    /// Marker interface, only meant to be implemented by ISolution&lt;T&gt;
    /// </summary>
    public interface ISolution
    {
    }

    public interface ISolution<out T> : ISolution
    {
        T TheSolution();
        T ExpectedSolution();
    }
}