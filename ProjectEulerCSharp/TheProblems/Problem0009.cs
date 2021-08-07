using System;

namespace TheProblems
{
    [Euler("Special Pythagorean triplet", @"
A Pythagorean triplet is a set of three natural numbers, a < b < c, for which,
     a^2 + b^2 = c^2
For example, 3^2 + 4^2 = 9 + 16 = 25 = 5^2.
There exists exactly one Pythagorean triplet for which a + b + c = 1000.
Find the product abc.
")]
    // ReSharper disable once UnusedMember.Global
    public class Problem0009 : ISolution<int>
    {
        /****************************************************************************************************
         * Special Pythagorean triplet
         *
         * A Pythagorean triplet is a set of three natural numbers, a < b < c, for which,
         *      a^2 + b^2 = c^2
         * For example, 3^2 + 4^2 = 9 + 16 = 25 = 5^2.
         * There exists exactly one Pythagorean triplet for which a + b + c = 1000.
         * Find the product abc.
         ****************************************************************************************************/
        public int BruteForceSolution()
        {
            var a = 1;
            var b = 0;
            var c = 0;
            var foundIt = false;
            for (; a <= 998; a++)
            {
                var aa = a * a;
                for (b = a + 1; a + b <= 999; b++)
                {
                    var bb = b * b;
                    var ab = a + b;
                    for (c = b + 1; ab + c <= 1000; c++)
                    {
                        if (ab + c == 1000)
                        {
                            if (aa + bb == c * c)
                            {
                                foundIt = true;
                                break;
                            }
                        }
                    }

                    if (foundIt)
                    {
                        break;
                    }
                }

                if (foundIt)
                {
                    break;
                }
            }

            if (foundIt == false)
            {
                throw new Exception($"D'oh! never found the answer, last values were {a}, {b}, {c}");
            }

            return a * b * c;
        }

        public bool HaveImplementedAnalyticSolution => false;

        public int AnalyticSolution()
        {
            throw new NotImplementedException();
        }

        public int ExpectedSolution()
        {
            return 31875000;
        }
    }
}