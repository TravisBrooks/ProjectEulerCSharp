using System.Collections.Generic;

namespace TheProblems
{
    public record PrimeFactor64(long Factor, long Power)
    {
        public static IList<PrimeFactor64> Factors(long n)
        {
            var factors = new List<PrimeFactor64>();
            long factor = 2;
            long power = 0;
            while (n % factor == 0)
            {
                power++;
                n /= factor;
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

            return factors;
        }
    }
}