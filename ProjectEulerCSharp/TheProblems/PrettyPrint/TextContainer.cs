namespace TheProblems.PrettyPrint
{
    public class TextContainer
    {
        public TextContainer(string? text = null, BorderStyle? borderStyle = null, BorderDisplay? borderDisplay = null)
        {
            Text = text ?? string.Empty;
            BorderStyle = borderStyle ?? BorderStyle.None;
            BorderDisplay = borderDisplay ?? BorderDisplay.None;
        }

        public string Text { get; set; }
        public BorderStyle BorderStyle { get; set; }
        public BorderDisplay BorderDisplay { get; set; }

        public char? BorderTopChar => _BorderTopOrBottom(top: true);
        public char? BorderBottomChar => _BorderTopOrBottom(top: false);
        public char? BorderRightChar => _BorderRightOrLeft(right: true);
        public char? BorderLeftChar => _BorderRightOrLeft(right: false);

        private char? _BorderTopOrBottom(bool top)
        {
            var showBorder = top ? BorderDisplay.HasFlag(BorderDisplay.Top) : BorderDisplay.HasFlag(BorderDisplay.Bottom);
            if(showBorder)
            {
                switch (BorderStyle)
                {
                    case var bs when bs.HasFlag(BorderStyle.Solid):
                    {
                        return '─';
                    }
                    case var bs when bs.HasFlag(BorderStyle.Dashed):
                    {
                        return '┄';
                    }
                    case var bs when bs.HasFlag(BorderStyle.Dotted):
                    {
                        return '┈';
                    }
                    case var bs when bs.HasFlag(BorderStyle.Double):
                    {
                        return '═';
                    }
                    default:
                        return null;
                }
            }

            return null;
        }

        private char? _BorderRightOrLeft(bool right)
        {
            var showBorder = right ? BorderDisplay.HasFlag(BorderDisplay.Right) : BorderDisplay.HasFlag(BorderDisplay.Left);
            if (showBorder)
            {
                switch (BorderStyle)
                {
                    case var bs when bs.HasFlag(BorderStyle.Solid):
                    {
                        return '│';
                    }
                    case var bs when bs.HasFlag(BorderStyle.Dashed):
                    {
                        return '┆';
                    }
                    case var bs when bs.HasFlag(BorderStyle.Dotted):
                    {
                        return '┊';
                    }
                    case var bs when bs.HasFlag(BorderStyle.Double):
                    {
                        return '║';
                    }
                    default:
                        return null;
                }
            }

            return null;
        }

    }

}