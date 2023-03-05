namespace ProjectEulerCSharp.EulerMath
{
    public enum PermutationAlgorithm
    {
        /// <summary>
        /// See https://en.wikipedia.org/wiki/Heap%27s_algorithm
        /// </summary>
        Heap,
        /// <summary>
        /// See https://www.quickperm.org/03example.php
        /// </summary>
        QuickPerm03,
        /// <summary>
        /// Permutations in sorted order, based on Knuth's Algorithm L
        /// </summary>
        Lexicographic
    }
}