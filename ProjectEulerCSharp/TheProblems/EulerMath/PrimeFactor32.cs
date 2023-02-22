using System;
using System.Collections.Generic;

namespace TheProblems.EulerMath;

public record PrimeFactor32(int Factor, int Power)
{
    public static IList<PrimeFactor32> Factors(int n)
    {
        if (n < 2)
        {
            throw new ArgumentOutOfRangeException(nameof(n), "must be at least 2 to find factors");
        }
        var factors = new List<PrimeFactor32>();
        var factor = 2;
        var power = 0;
        while (n % factor == 0)
        {
            power++;
            n /= factor;
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

        return factors;
    }
}