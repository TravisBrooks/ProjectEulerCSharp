using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Xunit;

namespace ProjectEulerCSharp.EulerMath.UnitTests
{
    public class PermutationsTest
    {
        [Theory]
        [MemberData(nameof(PermutationData))]
        public void PermutationTest<T>(PermutationTestData<T> testData) where T : IComparable<T>
        {
            var (anArray, permutationAlgorithm, permutationsExpected) = testData;
            var copyOfOriginalArray = new T[anArray.Length];
            Array.Copy(anArray, copyOfOriginalArray, anArray.Length);
            var permutationsActual = Permutations.Of(anArray, permutationAlgorithm).ToArray();
            permutationsActual.Should().BeEquivalentTo(permutationsExpected);
            anArray.Should().BeEquivalentTo(copyOfOriginalArray);
        }

        public static IEnumerable<object[]> PermutationData()
        {
            return PermutationDataHeapAlg()
                .Concat(PermutationDataQuickPerm03Alg())
                .Concat(PermutationDataLexicographicAlg());
        }

        private static IEnumerable<object[]> PermutationDataHeapAlg()
        {
            yield return new object[]
            {
                new PermutationTestData<int>(new[] { 5 }, PermutationAlgorithm.Heap, new[] { new[] { 5 } })
            };
            yield return new object[]
            {
                new PermutationTestData<int>(
                    new[] { 1, 2 },
                    PermutationAlgorithm.Heap,
                    new[]
                    {
                        new[] { 1, 2 },
                        new[] { 2, 1 }
                    })
            };
            yield return new object[]
            {
                new PermutationTestData<int>(
                    new[] { 0, 1, 2 },
                    PermutationAlgorithm.Heap,
                    new[]
                    {
                        new[] { 0, 1, 2 },
                        new[] { 1, 0, 2 },
                        new[] { 2, 0, 1 },
                        new[] { 0, 2, 1 },
                        new[] { 1, 2, 0 },
                        new[] { 2, 1, 0 }
                    })
            };
            yield return new object[]
            {
                new PermutationTestData<string>(
                    new[] { "foo", "bar" }, // note: array is reverse sorted
                    PermutationAlgorithm.Heap,
                    new[]
                    {
                        new[] { "foo", "bar" },
                        new[] { "bar", "foo" }
                    })
            };
        }

        private static IEnumerable<object[]> PermutationDataQuickPerm03Alg()
        {
            yield return new object[]
            {
                new PermutationTestData<int>(new[] { 5 }, PermutationAlgorithm.Heap, new[] { new[] { 5 } })
            };
            yield return new object[]
            {
                new PermutationTestData<int>(
                    new[] { 1, 2 },
                    PermutationAlgorithm.QuickPerm03,
                    new[]
                    {
                        new[] { 1, 2 },
                        new[] { 2, 1 }
                    })
            };
            yield return new object[]
            {
                new PermutationTestData<int>(
                    new[] { 0, 1, 2 },
                    PermutationAlgorithm.QuickPerm03,
                    new[]
                    {
                        new[] { 0, 1, 2 },
                        new[] { 1, 0, 2 },
                        new[] { 2, 0, 1 },
                        new[] { 0, 2, 1 },
                        new[] { 1, 2, 0 },
                        new[] { 2, 1, 0 }
                    })
            };
            yield return new object[]
            {
                new PermutationTestData<string>(
                    new[] { "foo", "bar" }, // note: array is reverse sorted
                    PermutationAlgorithm.QuickPerm03,
                    new[]
                    {
                        new[] { "foo", "bar" },
                        new[] { "bar", "foo" }
                    })
            };
        }

        private static IEnumerable<object[]> PermutationDataLexicographicAlg()
        {
            yield return new object[]
            {
                new PermutationTestData<int>(new[] { 5 }, PermutationAlgorithm.Lexicographic, new[] { new[] { 5 } })
            };
            yield return new object[]
            {
                new PermutationTestData<int>(
                    new[] { 1, 2 },
                    PermutationAlgorithm.Lexicographic,
                    new[]
                    {
                        new[] { 1, 2 },
                        new[] { 2, 1 }
                    })
            };
            yield return new object[]
            {
                new PermutationTestData<int>(
                    new[] { 0, 1, 2 },
                    PermutationAlgorithm.Lexicographic,
                    new[]
                    {
                        new[] { 0, 1, 2 },
                        new[] { 0, 2, 1 },
                        new[] { 1, 0, 2 },
                        new[] { 1, 2, 0 },
                        new[] { 2, 0, 1 },
                        new[] { 2, 1, 0 }
                    })
            };
            yield return new object[]
            {
                new PermutationTestData<string>(
                    new[] { "foo", "bar" }, // note: array is reverse sorted
                    PermutationAlgorithm.Lexicographic,
                    new[]
                    {
                        new[] { "bar", "foo" },
                        new[] { "foo", "bar" }
                    })
            };
        }

        private static string ArrayOfArrayToString<T>(IEnumerable<T[]> allPermutations)
        {
            var sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine("{");
            foreach (var permutations in allPermutations)
            {
                sb.AppendLine("     new []{" + string.Join(", ", permutations) + "},");
            }

            sb.AppendLine("}");
            return sb.ToString();
        }

        public record PermutationTestData<T>(
            T[] AnArray,
            PermutationAlgorithm PermutationAlgorithm,
            IEnumerable<T[]> ExpectedPermutations
        );
    }
}