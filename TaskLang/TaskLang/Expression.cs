using TaskLang.Tokens;

namespace TaskLang.Context
{
    public enum ExpressionType
    {
        VarDeclaration, //var =
        VarAssignment, //=
        FunctionDefinition, //->
        FunctionCall, //a b c
        Return, //return
        Control //if, while, for
    }
    public class Expression
    {
        public ExpressionType Type { get; private set; }
        public Token[] Tokens { get; private set; }
        public int LineNumber { get; private set; }

        protected Expression(int LineNumber)
        {
            this.LineNumber = LineNumber;
        }
        public Expression(ExpressionType Type, Token[] Tokens, int LineNumber) : this(LineNumber)
        {
            this.Type = Type;
            this.Tokens = Tokens;
        }
    }
    public class ExpressionError : Expression
    {
        public string Error { get; private set; }

        public ExpressionError(string Error, int LineNumber) : base(LineNumber)
        {
            this.Error = Error;
        }
    }
}