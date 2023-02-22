using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace ProjectEulerCSharp.CodeGen
{
    public class TriangleLongestPath
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly StringBuilder _sb;
        private readonly Stack<string> _stack;

        public TriangleLongestPath(ITestOutputHelper testOutputHelper)
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

        public string BuildIt(int triangleRows)
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
            while (_stack.TryPop(out var result))
            {
                _sb.AppendLine(result);
            }

            _sb.Append("return maxLen;");
            return _sb.ToString();
        }

        [Fact]
        public void CodeGenProblem18()
        {
            // yup, more unit test abuse! The side effect of running this test is it outputs the text of the solution to #18,
            // minus the array declaration. Problem 18 has 15 rows but apparently 67 has 100 so if I want to try the same brute
            // force on 67 its faster (and more likely to be error free) to code gen it then to hand roll all those loops. The
            // output can be auto formatted in VS after its gen'd.
            var builder = new TriangleLongestPath(_testOutputHelper);
            var str = builder.BuildIt(15);
            _testOutputHelper.WriteLine(str);
        }

    }
}