namespace TaskLang
{
    public enum TokenType
    {
        Number,
        Word,
        String,
        Operator,
        Arrow,
        Group,
        Assign,
        Semicolon,
        LeftParen,
        RightParen,
        ReturnKey,
        WhileKey,
        ForKey,
        InKey,
        BreakKey,
        VarKey,
        IfKey,
        ThenKey,
        NewLine,
        Error
    }
    public struct Token
    {
        public TokenType Type { get; private set; }
        public string Info { get; private set; }

        public Token(TokenType Type, string Info = "")
        {
            this.Type = Type;
            this.Info = Info;
        }

        public override string ToString()
        {
            return Type.ToString() + (Info == "" ? "" : $":{Info}");
        }
    }
}
