using System;
using System.Text;
using System.Text.RegularExpressions;
using ProjectEulerCSharp.EulerMath;

namespace ProjectEulerCSharp.PrettyPrint
{
    public class TextContainer
    {
        public TextContainer(string text)
        {
            Text = text ?? string.Empty;
        }

        public string Text { get; }

        public static TextContainer BuildContainer(string rawText, int maxCharWidth = 80)
        {
            if (rawText.Length < maxCharWidth)
            {
                return new TextContainer(rawText);
            }
            var sb = new StringBuilder();
            var lines = rawText.SplitOnNewLines(StringSplitOptions.TrimEntries);
            var containsWhitespace = new Regex(@"\s");
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
                if (!containsWhitespace.IsMatch(line))
                {
                    sb.AppendLine(line);
                    continue;
                }

                var currIdx = 0;
                while (currIdx < line.Length - 1)
                {
                    var endIndex = currIdx + maxCharWidth - 1;
                    if (endIndex >= line.Length - 1)
                    {
                        sb.AppendLine(line.Substring(currIdx));
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