﻿using System;
using System.Collections.Generic;

namespace ProjectEulerCSharp.EulerProblems._0030s
{
    [Euler(
        title: "Problem 39: Integer right triangles", 
        description: @"If p is the perimeter of a right angle triangle with integral length sides, {a,b,c}, there are exactly three solutions for p = 120.

{20,48,52}, {24,45,51}, {30,40,50}

For which value of p ≤ 1000, is the number of solutions maximized?")
    ]
    // ReSharper disable once UnusedMember.Global
    public class Problem0039 : ISolution<int>
    {
        public bool HaveImplementedAnalyticSolution => false;

        public int BruteForceSolution()
        {
            //var allRightTriangles = new Dictionary<int, HashSet<RightTriangle>>();
            var allRightTriangles = new Dictionary<int, int>();

            for (var a = 1; a < 997; a++)
            {
                for (var b = 1; a + b < 500; b++)
                {
                    var maxSideLen = Math.Max(a, b);
                    for (var c = maxSideLen + 1; a + b + c <= 1000; c++)
                    {
                        if (IsRightTriangle(a, b, c))
                        {
                            var perimeter = a + b + c;
                            allRightTriangles.TryAdd(perimeter, 0);
                            allRightTriangles[perimeter]++;
                        }
                    }
                }
            }

            var maxSolutions = 0;
            var perimeterWithMaxSolutions = 0;
            foreach (var perimeter in allRightTriangles.Keys)
            {
                var solutions = allRightTriangles[perimeter];
                if (solutions > maxSolutions)
                {
                    maxSolutions = solutions;
                    perimeterWithMaxSolutions = perimeter;
                }
            }

            return perimeterWithMaxSolutions;
        }

        private static bool IsRightTriangle(int a, int b, int c)
        {
            return a * a + b * b == c * c;
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
