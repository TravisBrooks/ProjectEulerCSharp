using System;

namespace TheProblems
{
    [Euler(
title: "Maximum path sum I",
description: @"
By starting at the top of the triangle below and moving to adjacent numbers on
the row below, the maximum total from top to bottom is 23.

3
7 4
2 4 6
8 5 9 3

That is, 3 + 7 + 4 + 9 = 23.

Find the maximum total from top to bottom of the triangle below:

75
95 64
17 47 82
18 35 87 10
20 04 82 47 65
19 01 23 75 03 34
88 02 77 73 07 63 67
99 65 04 28 06 16 70 92
41 41 26 56 83 40 80 70 33
41 48 72 33 47 32 37 16 94 29
53 71 44 65 25 43 91 52 97 51 14
70 11 33 28 77 73 17 78 39 68 17 57
91 71 52 38 17 14 91 43 58 50 27 29 48
63 66 04 68 89 53 67 30 73 16 69 87 40 31
04 62 98 27 23 09 70 98 73 93 38 53 60 04 23

NOTE: As there are only 16384 routes, it is possible to solve this problem by
trying every route. However, Problem 67, is the same challenge with a triangle
containing one-hundred rows; it cannot be solved by brute force, and requires a
clever method! ;o)"
    )]
    // ReSharper disable once UnusedMember.Global
    public class Problem0018 : ISolution<int>
    {
        public bool HaveImplementedAnalyticSolution => false;

        public int BruteForceSolution()
        {
            // the most brute force way I can think to do this is to have a crap ton of loops to find every possible path

            int[][] triangle =
            {
                new[]{75}, // 0 a
                new[]{95, 64}, // 1 b
                new[]{17, 47, 82}, // 2 c
                new[]{18, 35, 87, 10}, //3 d
                new[]{20, 04, 82, 47, 65}, // 4 e
                new[]{19, 01, 23, 75, 03, 34}, // 5 f
                new[]{88, 02, 77, 73, 07, 63, 67}, // 6 g
                new[]{99, 65, 04, 28, 06, 16, 70, 92}, // 7 h
                new[]{41, 41, 26, 56, 83, 40, 80, 70, 33}, // 8 i
                new[]{41, 48, 72, 33, 47, 32, 37, 16, 94, 29}, // 9 j
                new[]{53, 71, 44, 65, 25, 43, 91, 52, 97, 51, 14}, // 10 k
                new[]{70, 11, 33, 28, 77, 73, 17, 78, 39, 68, 17, 57}, // 11 l
                new[]{91, 71, 52, 38, 17, 14, 91, 43, 58, 50, 27, 29, 48}, // 12 m
                new[]{63, 66, 04, 68, 89, 53, 67, 30, 73, 16, 69, 87, 40, 31}, // 13 n
                new[]{04, 62, 98, 27, 23, 09, 70, 98, 73, 93, 38, 53, 60, 04, 23}, // 14 o
            };

            var maxLen = 0;

            // One of the dumbest nested loops I've ever written. Basic idea is the 2 branches that can be traveled in the
            // triangle from a given row [a, b] can go to [a+1, b] or [a+1, b+1]. This big ugly loop goes down both paths
            // and figures out the distance for both, keeping the larger. It looks dumb as hell but is surprisingly fast.
            for (var a=0; a < triangle[0].Length; a++)
            {
                for (var b = a; b <= a + 1; b++)
                {
                    for (var c = b; c <= b + 1; c++)
                    {
                        for (var d = c; d <= c + 1; d++)
                        {
                            for (var e = d; e <= d + 1; e++)
                            {
                                for (var f = e; f <= e + 1; f++)
                                {
                                    for (var g = f; g <= f + 1; g++)
                                    {
                                        for (var h = g; h <= g + 1; h++)
                                        {
                                            for (var i = h; i <= h + 1; i++)
                                            {
                                                for (var j = i; j <= i + 1; j++)
                                                {
                                                    for (var k = j; k <= j + 1; k++)
                                                    {
                                                        for (var l = k; l <= k + 1; l++)
                                                        {
                                                            for (var m = l; m <= l + 1; m++)
                                                            {
                                                                for (var n = m; n <= m + 1; n++)
                                                                {
                                                                    for (var o = n; o <= n + 1; o++)
                                                                    {
                                                                        var len = triangle[00][a] +
                                                                                  triangle[01][b] +
                                                                                  triangle[02][c] +
                                                                                  triangle[03][d] +
                                                                                  triangle[04][e] +
                                                                                  triangle[05][f] +
                                                                                  triangle[06][g] +
                                                                                  triangle[07][h] +
                                                                                  triangle[08][i] +
                                                                                  triangle[09][j] +
                                                                                  triangle[10][k] +
                                                                                  triangle[11][l] +
                                                                                  triangle[12][m] +
                                                                                  triangle[13][n] +
                                                                                  triangle[14][o];
                                                                        if (len > maxLen)
                                                                        {
                                                                            maxLen = len;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return maxLen;
        }

        public int AnalyticSolution()
        {
            throw new NotImplementedException();
        }

        public int ExpectedSolution()
        {
            return 1074;
        }
    }
}
