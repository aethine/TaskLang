using System;

namespace TaskLang
{
    //TODO: Convert variables to this type
    public struct Var
    {
        public string Value { get; private set; }
        public bool IsConst { get; private set; }

        public Var(string Value, bool IsConst)
        {
            this.Value = Value;
            this.IsConst = IsConst;
        }

        public bool Set(string value)
        {
            if (IsConst) return false;
            Value = value;
            return true;
        }

        public static explicit operator string(Var v) { return v.Value; }
        public static explicit operator Var(string s) { return new Var(s, false); }
    }
}