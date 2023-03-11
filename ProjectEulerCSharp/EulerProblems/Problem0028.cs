namespace ProjectEulerCSharp.EulerProblems
{
    [Euler(
        title: "Problem 28: Number spiral diagonals",
        description: @"Starting with the number 1 and moving to the right in a clockwise direction a 5 by 5 spiral is formed as follows:

21 22 23 24 25
20  7  8  9 10
19  6  1  2 11
18  5  4  3 12
17 16 15 14 13

It can be verified that the sum of the numbers on the diagonals is 101.

What is the sum of the numbers on the diagonals in a 1001 by 1001 spiral formed in the same way?")
    ]
    // ReSharper disable once UnusedMember.Global
    public class Problem0028 : ISolution<int>
    {
        public bool HaveImplementedAnalyticSolution => true;

        public int BruteForceSolution()
        {
            // I took some notes on paper and figured out a pattern when I continued the spiral to a 7x7 square
            // 43 44 45 46 47 48 49
            // 42 21 22 23 24 25 26
            // 41 20  7  8  9 10 27
            // 40 19  6  1  2 11 28
            // 39 18  5  4  3 12 29
            // 38 17 16 15 14 13 30
            // 37 36 35 34 33 32 31


            // diagonal sums
            // 1          =   1 ... 1x1
            // 3  5  7  9 =  24 ... 3x3
            //13 17 21 25 =  76 ... 5x5
            //31 37 43 49 = 160 ... 7x7

            // The pattern is summing the last values by increasing even numbers
            //( 3,  5,  7,  9) ~ ( 1 +  2,  1 +  4,  1 +  6,  1 +  8)
            //(13, 17, 21, 25) ~ ( 3 + 10,  5 + 12,  7 + 14,  9 + 16)
            //(31, 37, 43, 49) ~ (13 + 18, 17 + 20, 21 + 22, 25 + 24)
            
            int a = 1, b = 1, c = 1, d = 1;
            var diagonalDelta = 0;
            var diagonalSum = 1;
            for (var dimensions = 1; dimensions < 1_001; dimensions+=2)
            {
                a += diagonalDelta += 2;
                b += diagonalDelta += 2;
                c += diagonalDelta += 2;
                d += diagonalDelta += 2;
                diagonalSum += a + b + c + d;
            }
            return diagonalSum;
        }

        public int AnalyticSolution()
        {
            // Try this other solution to see if its any faster. What makes it more analytical is noticing that the upper right
            // values are squares of the nxn dimension. The 3 other diagonals can be determined by offsets of this n^2 value
            // 43 44 45 46 47 48 49
            // 42 21 22 23 24 25 26
            // 41 20  7  8  9 10 27
            // 40 19  6  1  2 11 28
            // 39 18  5  4  3 12 29
            // 38 17 16 15 14 13 30
            // 37 36 35 34 33 32 31
            //
            //  n^2
            //  n^2 - 1n + 1
            //  n^2 - 2n + 2
            //  n^2 - 3n + 3
            // added together:
            // 4n^2 - 6n + 6
            var diagonalSum = 1;
            for (var dimensions = 3; dimensions <= 1_001; dimensions+=2)
            {
                diagonalSum += 4 * dimensions * dimensions - 6 * dimensions + 6;
            }
            return diagonalSum;
        }

        public int ExpectedSolution()
        {
            return 669_171_001;
        }
    }
}
