using System;

namespace ProjectEulerCSharp.EulerProblems
{
    [Euler(
        title: "Problem 31: Coin sums",
        description: @"In the United Kingdom the currency is made up of pound (£) and pence (p). There are eight coins in general circulation:

1p, 2p, 5p, 10p, 20p, 50p, £1 (100p), and £2 (200p).
It is possible to make £2 in the following way:

1×£1 + 1×50p + 2×20p + 1×5p + 1×2p + 3×1p
How many different ways can £2 be made using any number of coins?")
    ]
    // ReSharper disable once UnusedMember.Global
    public class Problem0031 : ISolution<int>
    {
        public bool HaveImplementedAnalyticSolution => false;

        private const int aValue = 200;
        private const int bValue = 100;
        private const int cValue = 50;
        private const int dValue = 20;
        private const int eValue = 10;
        private const int fValue = 5;
        private const int gValue = 2;
        private const int hValue = 1;

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
            
            return aValue * aCount +
                   bValue * bCount +
                   cValue * cCount +
                   dValue * dCount +
                   eValue * eCount +
                   fValue * fCount +
                   gValue * gCount +
                   hValue * hCount;
        }

        public int BruteForceSolution()
        {
            const int target = 200;
            var changeCount = 0;
            for (var aCount = 0; aCount * aValue <= target; aCount++)
            {
                if (TotalChange(aCount) == target)
                {
                    changeCount++;
                    break;
                }
                for (var bCount = 0; bCount * bValue <= target; bCount++)
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

                    for (var cCount = 0; cCount * cValue <= target; cCount++)
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
                        for (var dCount = 0; dCount * dValue <= target; dCount++)
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
                            for (var eCount = 0; eCount * eValue <= target; eCount++)
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
                                for (var fCount = 0; fCount * fValue <= target; fCount++)
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
                                    for (var gCount = 0; gCount * gValue <= target; gCount++)
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
                                        for (var hCount = 0; hCount * hValue <= target; hCount++)
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
