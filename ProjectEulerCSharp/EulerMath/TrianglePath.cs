using System;

namespace ProjectEulerCSharp.EulerMath
{
    /// <summary>
    ///     A class to solve problems #18 and #67
    /// </summary>
    public class TrianglePath
    {
        private readonly int[][] _pathLengths;
        private readonly int[][] _triangle;

        public TrianglePath(int[][] triangleData)
        {
            _triangle = triangleData;
            _pathLengths = new int[_triangle.Length][];
            for (var i = 0; i < _triangle.Length; i++)
            {
                _pathLengths[i] = new int[_triangle[i].Length];
            }

            // set the last row of the length data to the node values of the triangle's last row
            _pathLengths[^1] = _triangle[^1];
        }

        public int LongestPath()
        {
            // fill in triangleLengths from the bottom up
            for (var i = _pathLengths.Length - 2; i >= 0; i--)
            {
                for (var j = 0; j < _pathLengths[i].Length; j++)
                {
                    var nodeValue = _triangle[i][j];
                    var leftPath = nodeValue + _pathLengths[i + 1][j];
                    var rightPath = nodeValue + _pathLengths[i + 1][j + 1];
                    var maxPath = Math.Max(leftPath, rightPath);
                    _pathLengths[i][j] = maxPath;
                }
            }

            return _pathLengths[0][0];
        }
    }
}