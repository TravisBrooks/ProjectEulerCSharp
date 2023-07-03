using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEulerCSharp.EulerMath
{
    public record PrimeFactor64(long Factor, long Power)
    {
        private static readonly Dictionary<long, List<PrimeFactor64>> FactorCache = new();

        public static IList<PrimeFactor64> Factors(long n)
        {
            if (n < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(n), "must be at least 2 to find factors");
            }

            var originalN = n;
            var factors = new List<PrimeFactor64>();
            long factor = 2;
            long power = 0;
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
                factors.Add(new PrimeFactor64(factor, power));
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
                        factors.Add(new PrimeFactor64(factor, power));
                    }

                    factor += 2;
                    power = 0;
                }
            }

            if (power > 0)
            {
                factors.Add(new PrimeFactor64(factor, power));
            }
            FactorCache[originalN] = factors;

            return factors;
        }

        private static bool LookForAnswerInCache(long n, long factor, long power, List<PrimeFactor64> factors, out IList<PrimeFactor64> list)
        {
            if (FactorCache.TryGetValue(n, out var history))
            {
                var dict = new Dictionary<long, long> { { factor, power } };
                MergeFactors(factors, dict);
                MergeFactors(history, dict);
                factors.Clear();
                foreach (var f in dict.Keys.OrderBy(i => i))
                {
                    factors.Add(new PrimeFactor64(f, dict[f]));
                }

                list = factors;
                return true;
            }

            list = new List<PrimeFactor64>();
            return false;
        }

        private static void MergeFactors(List<PrimeFactor64> factors, IDictionary<long, long> dict)
        {
            foreach (var factor64 in factors)
            {
                if (dict.ContainsKey(factor64.Factor))
                {
                    dict[factor64.Factor] += factor64.Power;
                }
                else
                {
                    dict.Add(factor64.Factor, factor64.Power);
                }
            }
        }
    }
}