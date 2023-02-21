using System;
using System.IO;
using System.Reflection;

namespace TheProblems
{
    [Euler(
        title: "Maximum path sum Part II",
        description: @"
By starting at the top of the triangle below and moving to adjacent numbers on
the row below, the maximum total from top to bottom is 23.

3
7 4
2 4 6
8 5 9 3

That is, 3 + 7 + 4 + 9 = 23.

Find the maximum total from top to bottom in triangle.txt (file embedded as
/ProblemData/p067_triangle.txt), a 15K text file containing a triangle with
one-hundred rows.

NOTE: This is a much more difficult version of Problem 18. It is not possible
to try every route to solve this problem, as there are 299 altogether! If you
could check one trillion (1012) routes every second it would take over twenty
billion years to check them all. There is an efficient algorithm to solve it.
;o)"
    )]
    // ReSharper disable once UnusedMember.Global
    public class Problem0067 : ISolution<int>
    {
        private readonly int[][] _triangleData;

        public Problem0067()
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream("TheProblems.ProblemData.p067_triangle.txt"))
            {
                if (stream == null)
                {
                    throw new Exception("Could not find resource for triangle data!");
                }

                using (StreamReader reader = new(stream))
                {
                    var result = reader.ReadToEnd();
                    _triangleData = result.TabularDataToJaggedIntArray();
                }
            }
        }

        public bool HaveImplementedAnalyticSolution => false;

        public int BruteForceSolution()
        {
            // This is really the analytic solution, I attempted my previous brute force solution for #18 and
            // just like the Euler website claims it looked like it was going to run forever.
            var solver = new TrianglePathProblem(_triangleData);
            var answer = solver.LongestPath();
            return answer;
        }

        public int AnalyticSolution()
        {
            throw new NotImplementedException();
        }

        public int ExpectedSolution()
        {
            return 7273;
        }
    }
}
