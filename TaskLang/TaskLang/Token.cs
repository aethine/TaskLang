namespace TaskLang
{
    enum TokenType
    {
        Number,
        Word,
        String,
        Operator,
        Arrow,
        Group,
        ReturnKey,
        WhileKey,
        ForKey,
        InKey,
        BreakKey,
        VarKey,
        Semicolon,
        NewLine,
        Error
    }
    struct Token
    {
        public TokenType Type { get; private set; }
        public string Info { get; private set; }

        public Token(TokenType Type, string Info = "")
        {
            this.Type = Type;
            this.Info = Info;
        }
    }
}
