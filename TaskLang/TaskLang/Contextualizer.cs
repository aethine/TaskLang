using System;
using System.Collections.Generic;
using System.Text;
using TaskLang.Tokens;

namespace TaskLang.Context
{
    public static class Contextualizer
    {

    }
    public class Context
    {
        public Context Parent { get; protected set; }
        public List<Context> Children { get; protected set; }
        public Dictionary<string, string> Variables { get; protected set; }

        public Context()
        {
            Parent = null;
            Children = new List<Context>();
            Variables = new Dictionary<string, string>();
        }
        protected Context(Context Parent) : this()
        {
            this.Parent = Parent;
        }

        public Context AddContext()
        {
            Context c = new Context(this);
            Children.Add(c);
            return c;
        }

        public void Destroy()
        {
            Parent.Children.Remove(this);
        }
        public bool DeclareVariable(string name, string value)
        {
            if (Variables.ContainsKey(name)) return false;
            Variables.Add(name, value);
            return true;
        }
        public bool AssignVariable(string name, string newValue)
        {
            if (!Variables.ContainsKey(name)) return false;
            Variables[name] = newValue;
            return true;
        }
    }
    public enum ExpressionType
    {
        VarDeclaration, //var =
        VarAssignment, //=
        FunctionDefinition, //->
        FunctionCall, //a b c
        Return, //return
        Control //if, while, for
    }
    public struct Expression
    {
        public ExpressionType Type { get; private set; }
        public Token[] Tokens { get; private set; }

        public Expression(ExpressionType Type, Token[] Tokens)
        {
            this.Type = Type;
            this.Tokens = Tokens;
        }
    }
    public struct ExpressionContext
    {
        public Expression Expression { get; private set; }
        public Context Context { get; private set; }

        public ExpressionContext(Expression Expression, Context Context)
        {
            this.Expression = Expression;
            this.Context = Context;
        }
    }
}
