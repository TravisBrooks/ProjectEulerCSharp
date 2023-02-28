using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace ProjectEulerCSharp.CodeGen
{
    public class BruteForceSolutionGen
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly StringBuilder _sb;
        private readonly Stack<string> _stack;

        public BruteForceSolutionGen(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _sb = new StringBuilder();
            _stack = new Stack<string>();
        }

        private void Push(string expr)
        {
            _sb.Append(expr);
            _sb.AppendLine("{");
            _stack.Push("}");
        }

        private void Pop()
        {
            while (_stack.TryPop(out var result))
            {
                _sb.AppendLine(result);
            }
        }

        [Fact]
        public void CodeGenProblem18()
        {
            // yup, more unit test abuse! The side effect of running this test is it outputs the text of the solution to #18,
            // minus the array declaration. Problem 18 has 15 rows but apparently 67 has 100 so if I want to try the same brute
            // force on 67 its faster (and more likely to be error free) to code gen it then to hand roll all those loops. The
            // output can be auto formatted in VS after its gen'd.
            var builder = new BruteForceSolutionGen(_testOutputHelper);
            var str = builder.CodeGenProblem18Impl(15);
            _testOutputHelper.WriteLine(str);
        }

        [Fact]
        public void CodeGenProblem24()
        {
            var builder = new BruteForceSolutionGen(_testOutputHelper);
            var str = builder.CodeGenProblem24Impl(new[]{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9}, 1_000_000);
            _testOutputHelper.WriteLine(str);
        }

        public string CodeGenProblem18Impl(int triangleRows)
        {
            _sb.AppendLine("var maxLen = 0;");
            _sb.AppendLine();
            Push("for (var idx0=0; idx0 < triangle[0].Length; idx0++)");
            for (var i = 1; i < triangleRows; i++)
            {
                Push($"for (var idx{i} = idx{i - 1}; idx{i} <= idx{i - 1} + 1; idx{i}++)");
            }
            _sb.AppendLine("var len = ");
            for (var i = 0; i < triangleRows; i++)
            {
                _sb.Append("triangle[" + i + "][idx" + i + "]");
                if (i == triangleRows - 1)
                {
                    _sb.AppendLine(";");
                }
                else
                {
                    _sb.AppendLine(" +");
                }
            }

            _sb.AppendLine(@"if (len > maxLen)
                             {
                                maxLen = len;
                             }");
            Pop();

            _sb.Append("return maxLen;");
            return _sb.ToString();
        }

        public string CodeGenProblem24Impl(int[] digits, int haltAfterThisManyDigits)
        {
            digits = digits.OrderBy(i => i).ToArray();
            _sb.AppendLine($"var digits = new []{{{string.Join(", ", digits)}}};");
            _sb.AppendLine("var numberCount = 0;");
            _sb.AppendLine("long answer = 0;");
            Push("foreach (var d0 in digits)");
            for (var i = 1; i < digits.Length; i++)
            {
                var exceptList = new List<string>();
                for (var x = i - 1; x>=0; x--)
                {
                    exceptList.Insert(0, $"d{x}");
                }
                var exceptStr = $"new[]{{{string.Join(", ", exceptList)}}}";
                Push($"foreach(var d{i} in digits.Except({exceptStr}))");
            }

            _sb.AppendLine("numberCount++;");
            _sb.AppendLine("if(numberCount>=" + haltAfterThisManyDigits + "){");
            _sb.AppendLine("string numStr = string.Empty + " + string.Join("+", digits.Select(i => $"d{i}")) + ";");
            _sb.AppendLine("answer = long.Parse(numStr);");
            // a good old goto seemed like the safest way to get out of the loops, particularly in generated code
            _sb.AppendLine("goto FoundIt;");
            _sb.AppendLine("}");

            Pop();

            _sb.AppendLine("FoundIt:");
            _sb.AppendLine("return answer;");
            return _sb.ToString();
        }
    }
}