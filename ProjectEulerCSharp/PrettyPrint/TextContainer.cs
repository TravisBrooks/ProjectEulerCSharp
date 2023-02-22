namespace ProjectEulerCSharp.PrettyPrint
{
    public class TextContainer
    {
        public TextContainer(string text)
        {
            Text = text ?? string.Empty;
        }

        public string Text { get; }
    }
}