using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEulerCSharp.EulerMath
{
    public static class Permutations
    {
        /// <summary>
        ///     All permutations of the given array, the order the permutations appear varies by algorithm.
        /// </summary>
        /// <typeparam name="T">Must be IComparable to support LexicographicPermutationAlgorithm</typeparam>
        /// <param name="anArray"></param>
        /// <param name="algChoice"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static IEnumerable<T[]> Of<T>(T[] anArray, PermutationAlgorithm algChoice)
            where T : IComparable<T>
        {
            switch (algChoice)
            {
                case PermutationAlgorithm.Heap:
                    return HeapPermutationAlgorithm(anArray);
                case PermutationAlgorithm.QuickPerm03:
                    return QuickPerm03Algorithm(anArray);
                case PermutationAlgorithm.Lexicographic:
                    return LexicographicPermutationAlgorithm(anArray);
                default:
                    throw new ArgumentException($"The {nameof(PermutationAlgorithm)} option \"{algChoice}\" is not supported.");
            }
        }

        /// <summary>
        ///     All permutations of a given array. The IEnumerable returned is lazy evaluated, so all permutations
        ///     do not need to be iterated for the IEnumerable to be reasoned over. Implementation based on the 3rd
        ///     algorithm example from https://www.quickperm.org/03example.php
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="anArray"></param>
        /// <returns></returns>
        private static IEnumerable<T[]> QuickPerm03Algorithm<T>(T[] anArray)
        {
            var n = anArray.Length;
            var a = new T[n];
            Array.Copy(anArray, a, n); // make a copy of the array so the original does not get changed
            var p = new int[n + 1];

            for (var ix = 0; ix < n; ix++)
            {
                p[ix] = ix;
            }

            p[n] = n; // p[N] > 0 controls iteration and the index boundary for i

            // have to make a copy of the array otherwise the subsequent array swaps will change the returned value
            yield return a.ToArray();

            var i = 1; // setup first swap points to be 1 and 0 respectively (i & j)
            while (i < n)
            {
                var j = 0;
                p[i]--; // decrease index "weight" for i by one
                do // reverse target array from j to i
                {
                    (a[j], a[i]) = (a[i], a[j]);
                    j++; // increment j by 1
                    i--; // decrement i by 1
                } while (j < i);

                yield return a.ToArray();

                i = 1; // reset index i to 1 (assumed)
                while (p[i] == 0) // while (p[i] == 0)
                {
                    p[i] = i; // reset p[i] zero value
                    i++; // set new index value for i (increase by one)
                }
            }
        }

        /// <summary>
        ///     All permutations of a given array. The IEnumerable returned is lazy evaluated, so all permutations
        ///     do not need to be iterated for the IEnumerable to be reasoned over. Implementation based on Heap's
        ///     algorithm https://en.wikipedia.org/wiki/Heap%27s_algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="anArray"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private static IEnumerable<T[]> HeapPermutationAlgorithm<T>(T[] anArray)
        {
            var arrayOfT = new T[anArray.Length];
            Array.Copy(anArray, arrayOfT, arrayOfT.Length);
            var n = arrayOfT.Length;
            var c = new int[n];

            yield return arrayOfT.ToArray();

            var i = 1;
            while (i < n)
            {
                if (c[i] < i)
                {
                    if (i % 2 == 0)
                    {
                        (arrayOfT[0], arrayOfT[i]) = (arrayOfT[i], arrayOfT[0]);
                    }
                    else
                    {
                        (arrayOfT[c[i]], arrayOfT[i]) = (arrayOfT[i], arrayOfT[c[i]]);
                    }

                    yield return arrayOfT.ToArray();

                    c[i] += 1;
                    i = 1;
                }
                else
                {
                    c[i] = 0;
                    i++;
                }
            }
        }

        /// <summary>
        ///     All permutations of a given array. The IEnumerable returned is lazy evaluated, so all permutations
        ///     do not need to be iterated for the IEnumerable to be reasoned over. Implementation based on Knuth's
        ///     Algorithm L
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="anArray"></param>
        /// <returns></returns>
        private static IEnumerable<T[]> LexicographicPermutationAlgorithm<T>(T[] anArray)
            where T : IComparable<T>
        {
            var n = anArray.Length;
            var arrayOfT = anArray.OrderBy(t => t).ToArray();
            yield return arrayOfT.ToArray();
            while (true)
            {
                var j = n - 2;
                while (j >= 0 && arrayOfT[j].CompareTo(arrayOfT[j + 1]) >= 0)
                {
                    j--;
                }

                if (j < 0)
                {
                    break;
                }

                var l = n - 1;
                while (arrayOfT[j].CompareTo(arrayOfT[l]) >= 0)
                {
                    l--;
                }

                (arrayOfT[j], arrayOfT[l]) = (arrayOfT[l], arrayOfT[j]);
                var k = j + 1;
                l = n - 1;
                while (k < l)
                {
                    (arrayOfT[k], arrayOfT[l]) = (arrayOfT[l], arrayOfT[k]);
                    k++;
                    l--;
                }

                yield return arrayOfT.ToArray();
            }
        }
    }
}