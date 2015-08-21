namespace Chameleon.TokenPackage
{
    public class Token
    {
        public string Text { get { return text; } }
        private string text;

        public TokenType Type { get { return type; } }
        private TokenType type;

        public Token(string text, TokenType type)
        {
            this.text = text;
            this.type = type;
        }

        public override string ToString()
        {
            if (text == "\n")
                text = "";
            return "[" + type + "] " + text;
        }
    }
}
