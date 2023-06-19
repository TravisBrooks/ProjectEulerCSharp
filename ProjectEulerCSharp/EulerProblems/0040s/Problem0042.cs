using System;
using System.Collections.Generic;
using System.Linq;
using ProjectEulerCSharp.EulerData;
using ProjectEulerCSharp.EulerMath;

namespace ProjectEulerCSharp.EulerProblems._0040s
{
    [Euler(
        title: "Problem 42: Coded Triangle Numbers",
        description: @"The nth term of the sequence of triangle numbers is given by, tn=1/2n(n+1); so the first ten triangle numbers are:
        1, 3, 6, 5, 10, 15, 21, 28, 36, 45, 55
By converting each letter in a word to a number corresponding to its alphabetical position and adding these values we form a word value. For example, the word value for SKY is 19+11+25=55=t10. If the word value is a triangle number then we shall call the word a triangle word.

Using p042_words.txt , a 16K text file containing nearly two-thousand common English words, how many are triangle words?")
    ]
    // ReSharper disable once UnusedMember.Global
    public class Problem0042 : ISolution<int>
    {
        private readonly string[] _words;

        public Problem0042()
        {
            _words = Get.Resource("p042_words.txt");
        }

        public bool HaveImplementedAnalyticSolution => false;

        public int BruteForceSolution()
        {
            var maybeTriangles = new int[_words.Length];
            for (var i = 0; i < _words.Length; i++)
            {
                var word = _words[i];
                var sum = 0;
                foreach (var c in word)
                {
                    sum += c.CharOrdinalPosition();
                }
                maybeTriangles[i] = sum;
            }

            var biggestMaybe = maybeTriangles.Max();
            var triangles = new HashSet<int>();
            var maxTriangle = 0;
            var n = 1;
            while (maxTriangle < biggestMaybe)
            {
                var triangle = MakeTriangleNumber(n);
                triangles.Add(triangle);
                maxTriangle = triangle;
                n++;
            }

            var answer = 0;
            foreach (var maybeTriangle in maybeTriangles)
            {
                if (triangles.Contains(maybeTriangle))
                {
                    answer++;
                }
            }
            return answer;
        }

        private int MakeTriangleNumber(int n)
        {
            return (n * (n + 1)) / 2;
        }

        public int AnalyticSolution()
        {
            throw new NotImplementedException();
        }

        public int ExpectedSolution()
        {
            return 162;
        }
    }
}
