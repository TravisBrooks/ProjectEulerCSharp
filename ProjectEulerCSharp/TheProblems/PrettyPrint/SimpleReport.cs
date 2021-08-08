using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheProblems.PrettyPrint
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

        public SimpleReport(int? minTextWidthInChars = null)
        {
            _containers = new List<TextContainer>();
            MinTextWidthInChars = minTextWidthInChars ?? 80;
        }

        public int MinTextWidthInChars { get; }

        public IList<TextContainer> ChildContainers => _containers.AsReadOnly();

        public void AddContainer(string text)
        {
            _containers.Add(new TextContainer(text ?? string.Empty));
        }

        public string PrettyPrintString()
        {
            var maxMessageLineChars = _containers.Select(c => c.Text)
                .SelectMany(str => str.Split(new[] {"\r\n", "\r", "\n"}, StringSplitOptions.None))
                .Max(str => str.Length);

            // the char for either the left or right border + white space
            var charsForBorder = 2;
            var totalCharsPerLine = Math.Max(maxMessageLineChars, MinTextWidthInChars) + charsForBorder + charsForBorder;

            var builder = new StringBuilder();
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
                var lines = container.Text.Split(new[] {"\r\n", "\r", "\n"}, StringSplitOptions.None);
                foreach (var line in lines)
                {
                    var lineCharCnt = 2;
                    builder.Append(BorderLeftChar);
                    builder.Append(" ");

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

            return builder.ToString();
        }
    }
}