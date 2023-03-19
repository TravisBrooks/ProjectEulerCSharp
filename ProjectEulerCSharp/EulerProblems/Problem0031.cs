using System;

namespace ProjectEulerCSharp.EulerProblems
{
    [Euler(
        title: "Problem 31: Coin sums",
        description: @"In the United Kingdom the currency is made up of pound (£) and pence (p). There are eight coins in general circulation:

1p, 2p, 5p, 10p, 20p, 50p, £1 (100p), and £2 (200p).
It is possible to make £2 in the following way:

1×£1 + 1×50p + 2×20p + 1×5p + 1×2p + 3×1p
How many different ways can £2 be made using any number of coins?")]
    // ReSharper disable once UnusedMember.Global
    public class Problem0031 : ISolution<int>
    {
        public bool HaveImplementedAnalyticSolution => false;

        //                                      a  b  c   d   e   f    g    h
        private static readonly int[] Coins = { 1, 2, 5, 10, 20, 50, 100, 200 };
        private static int TotalChange(
            int aCount, 
            int bCount = 0,
            int cCount = 0,
            int dCount = 0,
            int eCount = 0,
            int fCount = 0,
            int gCount = 0,
            int hCount = 0)
        {
            
            return Coins[0] * aCount +
                   Coins[1] * bCount +
                   Coins[2] * cCount +
                   Coins[3] * dCount +
                   Coins[4] * eCount +
                   Coins[5] * fCount +
                   Coins[6] * gCount +
                   Coins[7] * hCount;
        }

        public int BruteForceSolution()
        {
            const int target = 200;
            var changeCount = 0;
            for (var aCount = 0; aCount <= 200; aCount++)
            {
                if (TotalChange(aCount) == target)
                {
                    changeCount++;
                    break;
                }
                for (var bCount = 0; bCount <= 100; bCount++)
                {
                    var totalChange = TotalChange(aCount, bCount);
                    if (totalChange > target)
                    {
                        break;
                    }
                    if (totalChange == target)
                    {
                        changeCount++;
                        break;
                    }

                    for (var cCount = 0; cCount <= 40; cCount++)
                    {
                        totalChange = TotalChange(aCount, bCount, cCount);
                        if (totalChange > target)
                        {
                            break;
                        }
                        if (totalChange == target)
                        {
                            changeCount++;
                            break;
                        }
                        for (var dCount = 0; dCount <= 20; dCount++)
                        {
                            totalChange = TotalChange(aCount, bCount, cCount, dCount);
                            if (totalChange > target)
                            {
                                break;
                            }
                            if (totalChange == target)
                            {
                                changeCount++;
                                break;
                            }
                            for (var eCount = 0; eCount <= 10; eCount++)
                            {
                                totalChange = TotalChange(aCount, bCount, cCount, dCount, eCount);
                                if (totalChange > target)
                                {
                                    break;
                                }
                                if (totalChange == target)
                                {
                                    changeCount++;
                                    break;
                                }
                                for (var fCount = 0; fCount <= 4; fCount++)
                                {
                                    totalChange = TotalChange(aCount, bCount, cCount, dCount, eCount, fCount);
                                    if (totalChange > target)
                                    {
                                        break;
                                    }
                                    if (totalChange == target)
                                    {
                                        changeCount++;
                                        break;
                                    }
                                    for (var gCount = 0; gCount <= 2; gCount++)
                                    {
                                        totalChange = TotalChange(aCount, bCount, cCount, dCount, eCount, fCount, gCount);
                                        if (totalChange > target)
                                        {
                                            break;
                                        }
                                        if (totalChange == target)
                                        {
                                            changeCount++;
                                            break;
                                        }
                                        for (var hCount = 0; hCount <= 1; hCount++)
                                        {
                                            totalChange = TotalChange(aCount, bCount, cCount, dCount, eCount, fCount, gCount, hCount);
                                            if (totalChange > target)
                                            {
                                                break;
                                            }
                                            if (totalChange == target)
                                            {
                                                changeCount++;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return changeCount;
        }

        public int AnalyticSolution()
        {
            throw new NotImplementedException();
        }

        public int ExpectedSolution()
        {
            return 73_682;
        }

    }
}
