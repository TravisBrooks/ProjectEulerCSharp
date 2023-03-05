using System;
using System.Text;
using System.Text.RegularExpressions;
using ProjectEulerCSharp.EulerMath;

namespace ProjectEulerCSharp.PrettyPrint
{
    public class TextContainer
    {
        private static readonly Regex ContainsWhitespace = new(@"\s", RegexOptions.Compiled);

        public TextContainer(string text)
        {
            Text = text ?? string.Empty;
        }

        public string Text { get; }

        /// <summary>
        ///     Will format the given text to add line breaks to fit lines into the maxCharWidth. Will add breaks on whitespace
        ///     only, if no whitespace is present the line(s) will exceed maxCharWidth.
        /// </summary>
        /// <param name="rawText"></param>
        /// <param name="maxCharWidth"></param>
        /// <returns></returns>
        public static TextContainer Build(string rawText, int maxCharWidth)
        {
            if (rawText.Length < maxCharWidth)
            {
                return new TextContainer(rawText);
            }

            var sb = new StringBuilder();
            var lines = rawText.SplitOnNewLines(StringSplitOptions.TrimEntries);
            foreach (var line in lines)
            {
                if (string.Equals(line, string.Empty))
                {
                    sb.AppendLine();
                    continue;
                }

                if (line.Length <= maxCharWidth)
                {
                    sb.AppendLine(line);
                    continue;
                }

                // if we have a big line of text with no whitespace we'll assume that its important and will let it exceed the maxCharWidth
                if (!ContainsWhitespace.IsMatch(line))
                {
                    sb.AppendLine(line);
                    continue;
                }

                var currIdx = 0;
                while (currIdx < line.Length - 1)
                {
                    var endIndex = currIdx + maxCharWidth - 1;
                    var restOfLine = line.Substring(currIdx);
                    // if it all fits in 1 line or there is no whitespace in the rest of the line append the rest of the line
                    if (restOfLine.Length < maxCharWidth || !ContainsWhitespace.IsMatch(restOfLine))
                    {
                        sb.AppendLine(restOfLine);
                        currIdx = line.Length;
                    }
                    else if (char.IsWhiteSpace(line[endIndex]))
                    {
                        sb.AppendLine(line.Substring(currIdx, maxCharWidth));
                        currIdx = endIndex + 1;
                    }
                    else
                    {
                        // go back to find first whitespace char
                        for (var i = endIndex; i >= currIdx; i--)
                        {
                            if (char.IsWhiteSpace(line[i]))
                            {
                                if (maxCharWidth <= i - currIdx + 1)
                                {
                                    sb.AppendLine(line.Substring(currIdx));
                                    currIdx = line.Length;
                                }
                                else
                                {
                                    var substrLineLength = i - currIdx + 1;
                                    sb.AppendLine(line.Substring(currIdx, substrLineLength));
                                    currIdx += substrLineLength;
                                }

                                break;
                            }
                        }
                    }
                }
            }

            return new TextContainer(sb.ToString().Trim());
        }
    }
}