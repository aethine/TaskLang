using System;
using System.Collections.Generic;
using System.Text;

namespace TaskLang
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
}
