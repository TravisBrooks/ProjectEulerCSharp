using System;
using System.Collections.Generic;

namespace ProjectEulerCSharp.EulerProblems._0030s
{
    [Euler(
        title: "Problem 39: Integer right triangles", 
        description: @"If p is the perimeter of a right angle triangle with integral length sides, {a,b,c}, there are exactly three solutions for p = 120.

{20,48,52}, {24,45,51}, {30,40,50}

For which value of p ≤ 1000, is the number of solutions maximized?")
    ]
    // ReSharper disable once UnusedType.Global
    public class Problem0039 : ISolution<int>
    {
        public bool HaveImplementedAnalyticSolution => false;

        public int BruteForceSolution()
        {
            var allRightTriangles = new Dictionary<int, int>();

            for (var a = 1; a < 997; a++)
            {
                // c must be greater than a+b, and a+b+c is at most 1000 so a+b must be less than 500
                for (var b = 1; a + b < 500; b++)
                {
                    var abSquared = a * a + b * b;
                    var c = (int)Math.Sqrt(abSquared);
                    if (c * c == abSquared)
                    {
                        var perimeter = a + b + c;
                        allRightTriangles.TryAdd(perimeter, 0);
                        allRightTriangles[perimeter]++;
                    }
                }
            }

            var maxSolutions = 0;
            var perimeterWithMaxSolutions = 0;
            foreach (var perimeter in allRightTriangles.Keys)
            {
                var solutionCount = allRightTriangles[perimeter];
                if (solutionCount > maxSolutions)
                {
                    maxSolutions = solutionCount;
                    perimeterWithMaxSolutions = perimeter;
                }
            }

            return perimeterWithMaxSolutions;
        }

        public int AnalyticSolution()
        {
            throw new NotImplementedException();
        }

        public int ExpectedSolution()
        {
            return 840;
        }
    }
}
