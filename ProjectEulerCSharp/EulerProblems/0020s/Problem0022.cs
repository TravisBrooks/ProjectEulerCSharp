using System;
using System.Linq;
using ProjectEulerCSharp.EulerMath;

namespace ProjectEulerCSharp.EulerProblems._0020s
{
    [Euler(
        title: "Problem 22: Names scores",
        description: @"Using names.txt (file embedded as /EulerData/p022_names.txt), a 46K text file containing over five-thousand first names, begin by sorting it into alphabetical order. Then working out the alphabetical value for each name, multiply this value by its alphabetical position in the list to obtain a name score.

For example, when the list is sorted into alphabetical order, COLIN, which is worth 3 + 15 + 12 + 9 + 14 = 53, is the 938th name in the list. So, COLIN would obtain a score of 938 × 53 = 49714.

What is the total of all the name scores in the file?")
    ]
    // ReSharper disable once UnusedType.Global
    public class Problem0022 : ISolution<int>
    {
        private readonly string[] _names;

        public Problem0022()
        {
            _names = EulerData.Get.Resource(fileName: "p022_names.txt");
        }

        public bool HaveImplementedAnalyticSolution => false;

        public int BruteForceSolution()
        {
            var answer = _names
                .OrderBy(s => s)
                .Select((s, idx) => s.Select(c => c.CharOrdinalPosition()).Sum() * (idx + 1)) // idx from Select is 0-based so have to add 1
                .Sum();
            return answer;
        }

        public int AnalyticSolution()
        {
            throw new NotImplementedException();
        }

        public int ExpectedSolution()
        {
            return 871_198_282;
        }
    }
}