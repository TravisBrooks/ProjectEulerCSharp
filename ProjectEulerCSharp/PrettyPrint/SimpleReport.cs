using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectEulerCSharp.EulerMath;

namespace ProjectEulerCSharp.PrettyPrint
{
    public class SimpleReport
    {
        /*
         * Box drawing unicode chars, see:
         * https://en.wikipedia.org/wiki/Box_Drawing_(Unicode_block)
         */

        private static readonly char BorderTopChar = '─';
        private static readonly char BorderBottomChar = '─';
        private static readonly char BorderRightChar = '│';
        private static readonly char BorderLeftChar = '│';

        private static readonly char CornerUpperLeft = '┌';
        private static readonly char CornerUpperRight = '┐';
        private static readonly char CornerLowerLeft = '└';
        private static readonly char CornerLowerRight = '┘';
        private static readonly char CornerInnerLeft = '├';
        private static readonly char CornerInnerRight = '┤';

        private readonly List<TextContainer> _containers;

        public SimpleReport(int? textMaxCharsPerLine = null)
        {
            _containers = new List<TextContainer>();
            TextMaxCharsPerLine = textMaxCharsPerLine ?? 80;
        }

        public int TextMaxCharsPerLine { get; }

        public IList<TextContainer> ChildContainers => _containers.AsReadOnly();

        public void AddContainer(string text)
        {
            _containers.Add(TextContainer.Build(text));
        }

        public string PrettyPrintString()
        {
            var maxMessageLineChars = _containers.Select(c => c.Text)
                .SelectMany(str => str.SplitOnNewLines(StringSplitOptions.TrimEntries))
                .Max(str => str.Length);

            // the char for either the left or right border + white space
            var charsForBorder = 2;
            var totalCharsPerLine = Math.Max(maxMessageLineChars, TextMaxCharsPerLine) + charsForBorder + charsForBorder;

            var builder = new StringBuilder();
            builder.AppendLine();
            var containerCount = _containers.Count;
            var currentContainer = 0;

            if (containerCount > 0)
            {
                builder.Append(CornerUpperLeft);
                builder.Append(new string(BorderTopChar, totalCharsPerLine - charsForBorder));
                builder.Append(CornerUpperRight);
                builder.Append(Environment.NewLine);
            }

            foreach (var container in _containers)
            {
                currentContainer++;
                var lines = container.Text.SplitOnNewLines(StringSplitOptions.TrimEntries);
                foreach (var line in lines)
                {
                    var lineCharCnt = 2;
                    builder.Append(BorderLeftChar);
                    builder.Append(' ');

                    builder.Append(line);
                    lineCharCnt += line.Length;

                    var padding = totalCharsPerLine - lineCharCnt - 1;
                    builder.Append(new string(' ', padding));
                    builder.Append(BorderRightChar);
                    builder.Append(Environment.NewLine);
                }

                if (currentContainer < containerCount)
                {
                    builder.Append(CornerInnerLeft);
                    builder.Append(new string(BorderBottomChar, totalCharsPerLine - charsForBorder));
                    builder.Append(CornerInnerRight);
                    builder.Append(Environment.NewLine);
                }
            }

            if (containerCount > 0)
            {
                builder.Append(CornerLowerLeft);
                builder.Append(new string(BorderBottomChar, totalCharsPerLine - charsForBorder));
                builder.Append(CornerLowerRight);
                builder.Append(Environment.NewLine);
            }

            var prettyPrintString = builder.ToString();
            return prettyPrintString;
        }
    }
}