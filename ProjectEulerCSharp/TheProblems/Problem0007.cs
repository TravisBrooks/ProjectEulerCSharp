using System.Linq;
using TheProblems.EulerMath;

namespace TheProblems
{
    [Euler("10001st prime",
        @"By listing the first six prime numbers: 2, 3, 5, 7, 11, and 13, we can see that
the 6th prime is 13.

What is the 10_001st prime number?")]
    // ReSharper disable once UnusedMember.Global
    public class Problem0007 : ISolution<int>
    {
        /****************************************************************************************************
         * 10001st prime
         *
         * By listing the first six prime numbers: 2, 3, 5, 7, 11, and 13, we can see that the 6th prime is 13.
         * What is the 10 001st prime number?
         ****************************************************************************************************/
        public int BruteForceSolution()
        {
            // took a few tries to narrow in on a tight match for the upper bounds to get at least 10,001 primes but not too many extra
            //var bunchOfPrimes = Primes.Calculate(5000000).ToList();
            //var bunchOfPrimes = Primes.Calculate(2500000).ToList();
            //var bunchOfPrimes = Primes.Calculate(150000).ToList();
            //var bunchOfPrimes = Primes.Calculate(110000).ToList();
            var bunchOfPrimes = Primes.Calculate(105000).ToList();
            return bunchOfPrimes[10000];
        }

        public bool HaveImplementedAnalyticSolution => false;

        public int AnalyticSolution()
        {
            throw new System.NotImplementedException();
        }

        public int ExpectedSolution()
        {
            return 104743;
        }
    }
}