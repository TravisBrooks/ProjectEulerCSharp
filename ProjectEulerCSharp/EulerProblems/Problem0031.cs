using System;
using System.Collections.Generic;

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

        private record CoinCountsRec(int ACount, int BCount = 0, int CCount = 0, int DCount = 0, int ECount = 0, int FCount = 0, int GCount = 0, int HCount = 0)
        {
            //                                      a  b  c   d   e   f    g    h
            private static readonly int[] Coins = { 1, 2, 5, 10, 20, 50, 100, 200 };

            public int TotalChange()
            {
                return Coins[0] * ACount +
                       Coins[1] * BCount +
                       Coins[2] * CCount +
                       Coins[3] * DCount +
                       Coins[4] * ECount +
                       Coins[5] * FCount +
                       Coins[6] * GCount +
                       Coins[7] * HCount;
            }
        }

        public int BruteForceSolution()
        {
            const int target = 200;
            var exactChangeSet = new HashSet<CoinCountsRec>();
            for (var aCount = 0; aCount <= 200; aCount++)
            {
                var coinCounts = new CoinCountsRec(aCount);
                if (coinCounts.TotalChange() == target)
                {
                    exactChangeSet.Add(coinCounts);
                    continue;
                }
                for (var bCount = 0; bCount <= 100; bCount++)
                {
                    coinCounts = new CoinCountsRec(aCount, bCount);
                    var totalChange = coinCounts.TotalChange();
                    if (totalChange > target)
                    {
                        continue;
                    }
                    if (totalChange == target)
                    {
                        exactChangeSet.Add(coinCounts);
                        continue;
                    }

                    for (var cCount = 0; cCount <= 40; cCount++)
                    {
                        coinCounts = new CoinCountsRec(aCount, bCount, cCount);
                        totalChange = coinCounts.TotalChange();
                        if (totalChange > target)
                        {
                            continue;
                        }
                        if (totalChange == target)
                        {
                            exactChangeSet.Add(coinCounts);
                            continue;
                        }
                        for (var dCount = 0; dCount <= 20; dCount++)
                        {
                            coinCounts = new CoinCountsRec(aCount, bCount, cCount, dCount);
                            totalChange = coinCounts.TotalChange();
                            if (totalChange > target)
                            {
                                continue;
                            }
                            if (totalChange == target)
                            {
                                exactChangeSet.Add(coinCounts);
                                continue;
                            }
                            for (var eCount = 0; eCount <= 10; eCount++)
                            {
                                coinCounts = new CoinCountsRec(aCount, bCount, cCount, dCount, eCount);
                                totalChange = coinCounts.TotalChange();
                                if (totalChange > target)
                                {
                                    continue;
                                }
                                if (totalChange == target)
                                {
                                    exactChangeSet.Add(coinCounts);
                                    continue;
                                }
                                for (var fCount = 0; fCount <= 4; fCount++)
                                {
                                    coinCounts = new CoinCountsRec(aCount, bCount, cCount, dCount, eCount, fCount);
                                    totalChange = coinCounts.TotalChange();
                                    if (totalChange > target)
                                    {
                                        continue;
                                    }
                                    if (totalChange == target)
                                    {
                                        exactChangeSet.Add(coinCounts);
                                        continue;
                                    }
                                    for (var gCount = 0; gCount <= 2; gCount++)
                                    {
                                        coinCounts = new CoinCountsRec(aCount, bCount, cCount, dCount, eCount, fCount, gCount);
                                        totalChange = coinCounts.TotalChange();
                                        if (totalChange > target)
                                        {
                                            continue;
                                        }
                                        if (totalChange == target)
                                        {
                                            exactChangeSet.Add(coinCounts);
                                            continue;
                                        }
                                        for (var hCount = 0; hCount <= 1; hCount++)
                                        {
                                            coinCounts = new CoinCountsRec(aCount, bCount, cCount, dCount, eCount, fCount, gCount, hCount);
                                            totalChange = coinCounts.TotalChange();
                                            if (totalChange > target)
                                            {
                                                continue;
                                            }
                                            if (totalChange == target)
                                            {
                                                exactChangeSet.Add(coinCounts);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return exactChangeSet.Count;
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
