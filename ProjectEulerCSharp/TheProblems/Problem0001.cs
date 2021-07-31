namespace TheProblems
{
    [Euler("Multiples of 3 or 5",
        @"If we list all the natural numbers below 10 that are multiples of 3 or 5, we get 3, 5, 6 and 9. The sum of these multiples is 23.
Find the sum of all the multiples of 3 or 5 below 1000.")
    ]
    // ReSharper disable once UnusedMember.Global
    public class Problem0001 : ISolution<int>
    {
        /****************************************************************************************************
         * Multiples of 3 or 5
         *
         * If we list all the natural numbers below 10 that are multiples of 3 or 5, we get 3, 5, 6 and 9. The sum of these multiples is 23.
         * Find the sum of all the multiples of 3 or 5 below 1000.
         ****************************************************************************************************/
        public int TheSolution()
        {
            var answer = 0;
            for (var i = 3; i < 1000; i++)
            {
                if (i % 3 == 0 || i % 5 == 0)
                {
                    answer += i;
                }
            }

            return answer;
        }

        public int ExpectedSolution()
        {
            return 233168;
        }
    }
}