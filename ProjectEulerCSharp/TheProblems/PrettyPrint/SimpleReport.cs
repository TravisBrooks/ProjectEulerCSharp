using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheProblems.PrettyPrint
{
    public class SimpleReport
    {
        private readonly List<TextContainer> _containers;

        public SimpleReport(BorderStyle? defaultBorderStyle = null, BorderDisplay? defaultBorderDisplay = null)
        {
            _containers = new List<TextContainer>();
            DefaultBorderStyle = defaultBorderStyle ?? BorderStyle.None;
            DefaultBorderDisplay = defaultBorderDisplay ?? BorderDisplay.None;
        }

        public BorderStyle DefaultBorderStyle { get; set; }
        public BorderDisplay DefaultBorderDisplay { get; set; }

        public IList<TextContainer> ChildContainers => _containers.AsReadOnly();

        public void AddContainer(string text)
        {
            _containers.Add(new TextContainer(text ?? string.Empty, DefaultBorderStyle, DefaultBorderDisplay));
        }

        public void AddContainer(TextContainer container)
        {
            _containers.Add(container);
        }

        public string PrettyPrintString()
        {
            var maxMessageLineChars = _containers.Select(c => c.Text)
                .SelectMany(str => str.Split(new[] {"\r\n", "\r", "\n"}, StringSplitOptions.None))
                .Max(str => str.Length);
            var haveBorderLeft = _containers.Any(tc => tc.BorderDisplay.HasFlag(BorderDisplay.Left));
            var haveBorderRight = _containers.Any(tc => tc.BorderDisplay.HasFlag(BorderDisplay.Right));
            var totalCharsPerLine = maxMessageLineChars;
            if (haveBorderLeft)
            {
                totalCharsPerLine += 2;
            }

            if (haveBorderRight)
            {
                totalCharsPerLine += 2;
            }

            var builder = new StringBuilder();
            foreach (var container in _containers)
            {
                var topChar = container.BorderTopChar;
                if (topChar.HasValue)
                {
                    builder.AppendLine(new string(topChar.Value, totalCharsPerLine));
                }

                var lines = container.Text.Split(new[] {"\r\n", "\r", "\n"}, StringSplitOptions.None);
                var leftChar = container.BorderLeftChar;
                var rightChar = container.BorderRightChar;
                foreach (var line in lines)
                {
                    var lineCharCnt = 0;
                    if (leftChar.HasValue)
                    {
                        builder.Append(leftChar.Value);
                        builder.Append(" ");
                        lineCharCnt += 2;
                    }

                    builder.Append(line);
                    lineCharCnt += line.Length;

                    if (rightChar.HasValue)
                    {
                        var padding = totalCharsPerLine - lineCharCnt - 1;
                        builder.Append(new string(' ', padding));
                        builder.Append(rightChar);
                    }

                    builder.AppendLine();
                }

                var bottomChar = container.BorderBottomChar;
                if (bottomChar.HasValue)
                {
                    builder.AppendLine(new string(bottomChar.Value, totalCharsPerLine));
                }
            }

            return builder.ToString();
        }
    }
}