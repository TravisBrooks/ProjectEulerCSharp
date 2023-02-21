namespace TheProblems
{
    [Euler(
title: "Maximum path sum 1",
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
        private readonly int[][] _triangleData;

        public Problem0018()
        {
            _triangleData = @"
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
04 62 98 27 23 09 70 98 73 93 38 53 60 04 23".TabularDataToJaggedIntArray();
        }

        public bool HaveImplementedAnalyticSolution => true;

        public int BruteForceSolution()
        {
            // Hey, its called a brute force solution...
            // One of the dumbest nested loops I've ever written. Basic idea is the 2 branches that can be traveled in the
            // triangle from a given row [a, b] can go to [a+1, b] or [a+1, b+1]. This big ugly loop goes down both paths
            // and figures out the distance for both, keeping the larger. It looks dumb as hell but is surprisingly fast.

            var triangle = _triangleData;

            var maxLen = 0;

            for (var idx0 = 0; idx0 < triangle[0].Length; idx0++)
            {
                for (var idx1 = idx0; idx1 <= idx0 + 1; idx1++)
                {
                    for (var idx2 = idx1; idx2 <= idx1 + 1; idx2++)
                    {
                        for (var idx3 = idx2; idx3 <= idx2 + 1; idx3++)
                        {
                            for (var idx4 = idx3; idx4 <= idx3 + 1; idx4++)
                            {
                                for (var idx5 = idx4; idx5 <= idx4 + 1; idx5++)
                                {
                                    for (var idx6 = idx5; idx6 <= idx5 + 1; idx6++)
                                    {
                                        for (var idx7 = idx6; idx7 <= idx6 + 1; idx7++)
                                        {
                                            for (var idx8 = idx7; idx8 <= idx7 + 1; idx8++)
                                            {
                                                for (var idx9 = idx8; idx9 <= idx8 + 1; idx9++)
                                                {
                                                    for (var idx10 = idx9; idx10 <= idx9 + 1; idx10++)
                                                    {
                                                        for (var idx11 = idx10; idx11 <= idx10 + 1; idx11++)
                                                        {
                                                            for (var idx12 = idx11; idx12 <= idx11 + 1; idx12++)
                                                            {
                                                                for (var idx13 = idx12; idx13 <= idx12 + 1; idx13++)
                                                                {
                                                                    for (var idx14 = idx13; idx14 <= idx13 + 1; idx14++)
                                                                    {
                                                                        var len =
                                                                            triangle[0][idx0] +
                                                                            triangle[1][idx1] +
                                                                            triangle[2][idx2] +
                                                                            triangle[3][idx3] +
                                                                            triangle[4][idx4] +
                                                                            triangle[5][idx5] +
                                                                            triangle[6][idx6] +
                                                                            triangle[7][idx7] +
                                                                            triangle[8][idx8] +
                                                                            triangle[9][idx9] +
                                                                            triangle[10][idx10] +
                                                                            triangle[11][idx11] +
                                                                            triangle[12][idx12] +
                                                                            triangle[13][idx13] +
                                                                            triangle[14][idx14];
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
            var solver = new TrianglePathProblem(_triangleData);
            return solver.LongestPath();
        }

        public int ExpectedSolution()
        {
            return 1074;
        }
        
    }
}
