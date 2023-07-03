using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEulerCSharp.EulerMath
{
    public record PrimeFactor32(int Factor, int Power)
    {
        private static readonly Dictionary<int, List<PrimeFactor32>> FactorCache = new();

        public static IList<PrimeFactor32> Factors(int n)
        {
            if (n < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(n), "must be at least 2 to find factors");
            }

            var originalN = n;
            var factors = new List<PrimeFactor32>();
            var factor = 2;
            var power = 0;
            while (n % factor == 0)
            {
                power++;
                n /= factor;
                if (LookForAnswerInCache(n, factor, power, factors, out var list))
                {
                    return list;
                }
            }

            if (power > 0)
            {
                factors.Add(new PrimeFactor32(factor, power));
            }

            factor = 3;
            power = 0;
            while (n >= factor)
            {
                if (n % factor == 0)
                {
                    power++;
                    n /= factor;
                    if (LookForAnswerInCache(n, factor, power, factors, out var list))
                    {
                        return list;
                    }
                }
                else
                {
                    if (power > 0)
                    {
                        factors.Add(new PrimeFactor32(factor, power));
                    }

                    factor += 2;
                    power = 0;
                }
            }

            if (power > 0)
            {
                factors.Add(new PrimeFactor32(factor, power));
            }
            FactorCache[originalN] = factors;

            return factors;
        }

        private static bool LookForAnswerInCache(int n, int factor, int power, List<PrimeFactor32> factors, out IList<PrimeFactor32> list)
        {
            if (FactorCache.TryGetValue(n, out var history))
            {
                var dict = new Dictionary<int, int> { { factor, power } };
                MergeFactors(factors, dict);
                MergeFactors(history, dict);
                factors.Clear();
                foreach (var f in dict.Keys.OrderBy(i => i))
                {
                    factors.Add(new PrimeFactor32(f, dict[f]));
                }

                list = factors;
                return true;
            }

            list = new List<PrimeFactor32>();
            return false;
        }

        private static void MergeFactors(List<PrimeFactor32> factors, IDictionary<int, int> dict)
        {
            foreach (var factor32 in factors)
            {
                if (dict.ContainsKey(factor32.Factor))
                {
                    dict[factor32.Factor] += factor32.Power;
                }
                else
                {
                    dict.Add(factor32.Factor, factor32.Power);
                }
            }
        }
    }
}